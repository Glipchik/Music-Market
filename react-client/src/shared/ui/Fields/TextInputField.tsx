import { Field, type FieldProps } from "formik";

interface TextInputFieldProps {
  name: string;
  placeholder?: string;
  minLength?: number;
  maxLength?: number;
}

export const TextInputField = ({
  name,
  placeholder,
  minLength,
  maxLength,
}: TextInputFieldProps) => {
  return (
    <Field name={name}>
      {({ field: formikField, form }: FieldProps) => {
        return (
          <input
            {...formikField}
            type="text"
            placeholder={placeholder}
            minLength={minLength}
            maxLength={maxLength}
            onChange={(e) => {
              const value = e.target.value;
              if (maxLength === undefined || value.length <= maxLength) {
                form.setFieldValue(formikField.name, value);
              }
            }}
            onBlur={formikField.onBlur}
            className="form-input-field"
          />
        );
      }}
    </Field>
  );
};
