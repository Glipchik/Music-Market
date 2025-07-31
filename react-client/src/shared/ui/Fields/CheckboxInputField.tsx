import { Field } from "formik";

interface CheckboxInputFieldProps {
  name: string;
}

export const CheckboxInputField = ({ name }: CheckboxInputFieldProps) => {
  return (
    <Field
      type="checkbox"
      id={name}
      name={name}
      className="h-4 w-4 text-indigo-600 border-gray-300 rounded"
    />
  );
};
