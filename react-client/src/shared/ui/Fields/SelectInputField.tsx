import { Field } from "formik";

interface SelectInputFieldProps {
  name: string;
  label: string;
  options?: string[];
  placeholder?: string;
}

export const SelectInputField = ({
  name,
  label,
  options,
  placeholder,
}: SelectInputFieldProps) => {
  return (
    <Field as="select" id={name} name={name} className="form-input-field">
      <option value="">{placeholder || `Select ${label}`}</option>
      {options?.map((opt) => (
        <option key={opt} value={opt}>
          {opt}
        </option>
      ))}
    </Field>
  );
};
