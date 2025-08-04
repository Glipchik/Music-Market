import React, { useState } from "react";
import { Formik, ErrorMessage, Form, type FormikHelpers } from "formik";
import FileUpload from "@/shared/ui/FileUpload";
import { useNavigate } from "react-router-dom";
import type { FormFieldDescriptorResponseModel } from "@/shared/types/form";
import type {
  InstrumentResponseModel,
  PhotoModel,
} from "@/shared/types/instrument";
import { toast } from "react-toastify";
import {
  createInstrument,
  deleteFiles,
  updateInstrument,
  uploadFiles,
} from "@/shared/api";
import { buildYupSchema } from "./validation";
import { DecimalInputField } from "@/shared/ui/Fields/DecimalInputField";
import { IntegerInputField } from "@/shared/ui/Fields/IntegerInputField";
import { TextInputField } from "@/shared/ui/Fields/TextInputField";
import { TextareaField } from "@/shared/ui/Fields/TextareaField";
import { SelectInputField } from "@/shared/ui/Fields/SelectInputField";
import { CheckboxInputField } from "@/shared/ui/Fields/CheckboxInputField";
import { useTranslation } from "react-i18next";

interface InstrumentFormProps {
  formSchema: FormFieldDescriptorResponseModel[];
  instrumentType: string;
  instrument?: InstrumentResponseModel;
}

const buildInitialValues = (
  formSchema: FormFieldDescriptorResponseModel[],
  instrument?: InstrumentResponseModel
): { [key: string]: unknown } => {
  if (!instrument) {
    return formSchema.reduce((values, field) => {
      if (field.type === "number" || field.type === "currency") {
        values[field.name] =
          field.defaultValue !== undefined && field.defaultValue !== null
            ? Number(field.defaultValue)
            : null;
      } else if (field.type === "checkbox") {
        values[field.name] = field.defaultValue ?? false;
      } else {
        values[field.name] = field.defaultValue ?? "";
      }
      return values;
    }, {} as { [key: string]: unknown });
  }

  const values: { [key: string]: unknown } = {
    name: instrument.name,
    price: instrument.price,
    manufacturer: instrument.manufacturer,
    description: instrument.description ?? "",
  };

  instrument.properties.forEach((prop) => {
    values[prop.name] = prop.value;
  });

  return values;
};

const InstrumentForm = ({
  formSchema,
  instrumentType,
  instrument,
}: InstrumentFormProps) => {
  const [selectedFiles, setSelectedFiles] = useState<File[]>([]);
  const [currentPhotoModels, setCurrentPhotoModels] = useState<PhotoModel[]>(
    instrument?.photoModels ?? []
  );
  const navigate = useNavigate();
  const { t } = useTranslation("instrumentForm");

  const initialValues = buildInitialValues(formSchema, instrument);
  const validationSchema = buildYupSchema(formSchema, t);

  type FormValues = typeof initialValues;

  const handleSubmit = async (
    values: FormValues,
    { setSubmitting, resetForm }: FormikHelpers<FormValues>
  ) => {
    try {
      let uploadedFileNames: string[] = [];

      if (selectedFiles.length > 0) {
        uploadedFileNames = await uploadFiles(selectedFiles);
      }

      const existingPhotoNames = currentPhotoModels.map((p) => p.photoName);

      const removedPhotos =
        instrument?.photoModels
          .map((p) => p.photoName)
          .filter((name) => !existingPhotoNames.includes(name)) || [];

      if (removedPhotos.length > 0) {
        await deleteFiles(removedPhotos);
      }

      const payload = {
        type: instrumentType,
        ...values,
        photoNames: [...existingPhotoNames, ...uploadedFileNames],
      };

      if (instrument) {
        await updateInstrument(instrument.id, payload);
        toast.success(t("successUpdate"));
        navigate(`/instruments/${instrument.id}`);
      } else {
        console.log(payload);
        const created = await createInstrument(payload);
        toast.success(t("successCreate"));
        resetForm();
        setSelectedFiles([]);
        navigate(`/instruments/${created.id}`);
      }
    } catch (error) {
      toast.error(t("errorOccurred"));
    } finally {
      setSubmitting(false);
    }
  };

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const newFiles = e.target.files ? Array.from(e.target.files) : [];
    setSelectedFiles((prev) => [...prev, ...newFiles]);
  };

  const handleRemoveFile = (fileToRemove: File) =>
    setSelectedFiles((prev) => prev.filter((file) => file !== fileToRemove));

  const handleRemoveInitialPhoto = (photoName: string) =>
    setCurrentPhotoModels((prev) =>
      prev.filter((photo) => photo.photoName !== photoName)
    );

  return (
    <Formik
      initialValues={initialValues}
      validationSchema={validationSchema}
      onSubmit={handleSubmit}
      enableReinitialize
    >
      {({ isSubmitting }) => (
        <Form className="bg-white p-8 rounded-lg shadow-md space-y-6">
          {formSchema.map((field) => {
            const minLengthRule = field.validationRules?.find(
              (r) => r.ruleName === "minLength"
            );
            const maxLengthRule = field.validationRules?.find(
              (r) => r.ruleName === "maxLength"
            );
            const minValueRule = field.validationRules.find(
              (r) => r.ruleName === "minValue"
            );
            const maxValueRule = field.validationRules.find(
              (r) => r.ruleName === "maxValue"
            );

            const minLength = minLengthRule
              ? Number(minLengthRule.ruleValue)
              : undefined;
            const maxLength = maxLengthRule
              ? Number(maxLengthRule.ruleValue)
              : undefined;
            const minValue = minValueRule
              ? Number(minValueRule.ruleValue)
              : undefined;
            const maxValue = maxValueRule
              ? Number(maxValueRule.ruleValue)
              : undefined;

            return (
              <div key={field.name} className="flex flex-col gap-1">
                <label htmlFor={field.name} className="form-label">
                  {t(field.labelKey)}
                  {field.isRequired && <span className="text-red-500">*</span>}
                </label>
                {field.type === "text" && (
                  <TextInputField
                    name={field.name}
                    placeholder={t(field.placeholderKey ?? "")}
                    minLength={minLength}
                    maxLength={maxLength}
                  />
                )}
                {field.type === "decimal" && (
                  <DecimalInputField
                    name={field.name}
                    placeholder={t(field.placeholderKey ?? "")}
                    minValue={minValue}
                    maxValue={maxValue}
                  />
                )}
                {field.type === "integer" && (
                  <IntegerInputField
                    name={field.name}
                    placeholder={t(field.placeholderKey ?? "")}
                    minValue={minValue}
                    maxValue={maxValue}
                  />
                )}
                {field.type === "textarea" && (
                  <TextareaField
                    name={field.name}
                    placeholder={t(field.placeholderKey ?? "")}
                    minLength={minLength}
                    maxLength={maxLength}
                  />
                )}
                {field.type === "select" && (
                  <SelectInputField
                    name={field.name}
                    label={t(field.labelKey) ?? ""}
                    placeholder={t(field.placeholderKey ?? "")}
                    options={
                      field.optionKeys && field.optionKeys.length > 0
                        ? field.optionKeys.map((key) => t(key))
                        : field.options ?? []
                    }
                  />
                )}
                {field.type === "checkbox" && (
                  <CheckboxInputField name={field.name} />
                )}
                <ErrorMessage
                  name={field.name}
                  component="div"
                  className="text-red-500 text-sm mt-1"
                />
              </div>
            );
          })}

          <FileUpload
            selectedFiles={selectedFiles}
            handleFileChange={handleFileChange}
            handleRemoveFile={handleRemoveFile}
            initialPhotos={currentPhotoModels}
            onRemoveInitialPhoto={handleRemoveInitialPhoto}
          />
          <button
            type="submit"
            disabled={isSubmitting}
            className="btn-base w-full bg-indigo-600 text-white hover:bg-indigo-700"
          >
            {isSubmitting
              ? instrument
                ? t("updating")
                : t("creating")
              : instrument
              ? t("updateListing")
              : t("createListing")}
          </button>
        </Form>
      )}
    </Formik>
  );
};

export default InstrumentForm;
