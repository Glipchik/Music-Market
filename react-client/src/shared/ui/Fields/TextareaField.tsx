import { Field, type FieldProps } from "formik";

interface TextareaFieldProps {
  minLength?: number;
  maxLength?: number;
  name: string;
  placeholder?: string;
}

export const TextareaField = ({
  minLength,
  maxLength,
  name,
  placeholder,
}: TextareaFieldProps) => {
  return (
    <Field name={name}>
      {({ field: formikField, form }: FieldProps) => {
        return (
          <textarea
            {...formikField}
            id={name}
            rows={6}
            placeholder={placeholder}
            minLength={minLength}
            maxLength={maxLength}
            className="form-input-field"
            onChange={(e) => {
              const value = e.target.value;
              if (maxLength === undefined || value.length <= maxLength) {
                form.setFieldValue(formikField.name, value);
              }
            }}
            onBlur={formikField.onBlur}
          />
        );
      }}
    </Field>
  );
};
