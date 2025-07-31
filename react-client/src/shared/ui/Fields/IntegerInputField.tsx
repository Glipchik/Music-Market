import { Field, type FieldProps } from "formik";
import { useState } from "react";

interface IntegerInputFieldProps {
  minValue?: number;
  maxValue?: number;
  placeholder?: string;
  name: string;
}

export const IntegerInputField = ({
  minValue,
  maxValue,
  placeholder,
  name,
}: IntegerInputFieldProps) => {
  return (
    <Field name={name}>
      {({ field: formikField, form }: FieldProps) => {
        const [inputValue, setInputValue] = useState(
          formikField.value !== undefined && formikField.value !== null
            ? String(formikField.value)
            : ""
        );

        const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
          const raw = e.target.value;

          if (raw === "" || /^\d*$/.test(raw)) {
            const num = Number(raw);

            setInputValue(raw);

            if (raw === "") {
              form.setFieldValue(name, null);
              return;
            }

            if (!isNaN(num)) {
              if (maxValue !== undefined && num > maxValue) {
                setInputValue(String(maxValue));
                form.setFieldValue(name, maxValue);
              } else if (minValue === undefined || num >= minValue) {
                form.setFieldValue(name, num);
              }
            }
          }
        };

        const handleBlur = () => {
          const num = Number(inputValue);
          if (!isNaN(num)) {
            let clamped = num;
            if (maxValue !== undefined && num > maxValue) {
              clamped = maxValue;
            } else if (minValue !== undefined && num < minValue) {
              clamped = minValue;
            }

            setInputValue(String(clamped));
            form.setFieldValue(name, clamped);
          } else {
            setInputValue("");
            form.setFieldValue(name, null);
          }
        };

        return (
          <input
            type="text"
            inputMode="numeric"
            value={inputValue}
            onChange={handleChange}
            onBlur={handleBlur}
            placeholder={placeholder}
            className="form-input-field"
          />
        );
      }}
    </Field>
  );
};
