import type { FormFieldDescriptorResponseModel } from "@/shared/types/form";
import type { TFunction } from "i18next";
import * as Yup from "yup";

export function buildYupSchema(fields: FormFieldDescriptorResponseModel[], t: TFunction) {
  const shape: Record<string, Yup.AnySchema> = {};

  fields.forEach((field) => {
    let validator: Yup.AnySchema = [
      "text",
      "textarea",
      "decimal",
      "integer",
      "select",
    ].includes(field.type)
      ? Yup.string()
      : field.type === "checkbox"
      ? Yup.boolean()
      : Yup.mixed();

    if (field.isRequired) {
      const fieldName = t(field.labelKey).toLowerCase()
      validator = validator.required(t("isRequired", { fieldName }));
    }

    field.validationRules.forEach((rule) => {
      const errorMessage = rule.errorMessageKey ? t(rule.errorMessageKey) : undefined;

      switch (rule.ruleName) {
        case "minLength":
          validator = (validator as Yup.StringSchema).min(
            rule.ruleValue as number,
            errorMessage
          );
          break;
        case "maxLength":
          validator = (validator as Yup.StringSchema).max(
            rule.ruleValue as number,
            errorMessage
          );
          break;
        case "pattern":
          validator = (validator as Yup.StringSchema).matches(
            new RegExp(rule.ruleValue as string),
            errorMessage
          );
          break;
        case "minValue":
          validator = (validator as Yup.NumberSchema).min(
            rule.ruleValue as number,
            errorMessage
          );
          break;
        case "maxValue":
          validator = (validator as Yup.NumberSchema).max(
            rule.ruleValue as number,
            errorMessage
          );
          break;
      }
    });

    shape[field.name] = validator;
  });

  return Yup.object().shape(shape);
}
