import { Field, type FieldProps } from "formik";
import { useState } from "react";

interface DecimalInputFieldProps {
  name: string;
  placeholder?: string;
  minValue?: number;
  maxValue?: number;
}

const DECIMAL_REGEX = /^\d*([.,]\d{0,2})?$/;

export const DecimalInputField = ({
  name,
  placeholder,
  minValue,
  maxValue,
}: DecimalInputFieldProps) => {
  return (
    <Field name={name}>
      {({ form }: FieldProps) => {
        const [inputValue, setInputValue] = useState("");

        const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
          const raw = e.target.value;
          const normalized = raw.replace(",", ".");

          if (raw === "" || DECIMAL_REGEX.test(normalized)) {
            setInputValue(raw);

            const parsed = parseFloat(normalized);
            if (!isNaN(parsed)) {
              if (maxValue !== undefined && parsed > maxValue) {
                form.setFieldValue(name, maxValue);
                setInputValue(String(maxValue.toFixed(2)));
              } else {
                form.setFieldValue(name, parsed);
              }
            } else {
              form.setFieldValue(name, "");
            }
          }
        };

        const handleBlur = () => {
          const normalized = inputValue.replace(",", ".");
          const parsed = parseFloat(normalized);

          if (!isNaN(parsed)) {
            let final = parsed;
            if (maxValue !== undefined && parsed > maxValue) {
              final = maxValue;
            } else if (minValue !== undefined && parsed < minValue) {
              final = minValue;
            }
            const formatted = final.toFixed(2);
            setInputValue(formatted);
            form.setFieldValue(name, final);
          } else {
            setInputValue("");
            form.setFieldValue(name, "");
          }
        };

        return (
          <input
            type="text"
            inputMode="decimal"
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
