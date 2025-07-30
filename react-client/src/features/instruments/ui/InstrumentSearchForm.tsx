import { Formik, Form, Field, ErrorMessage } from "formik";
import { useSubmit } from "react-router-dom";
import { MagnifyingGlassIcon } from "@heroicons/react/24/outline";
import { useEffect, useRef } from "react";
import { DecimalInputField } from "@/shared/ui/Fields/DecimalInputField";
import { TextInputField } from "@/shared/ui/Fields/TextInputField";
import { buildSearchValidationSchema } from "../lib/validation";

interface InstrumentSearchFormProps {
  searchParams: URLSearchParams;
}

const InstrumentSearchForm = ({ searchParams }: InstrumentSearchFormProps) => {
  const submit = useSubmit();
  const debounceRef = useRef<number | null>(null);

  const initialValues = {
    name: searchParams.get("name") || "",
    minPrice: searchParams.get("minPrice") || "",
    maxPrice: searchParams.get("maxPrice") || "",
    manufacturer: searchParams.get("manufacturer") || "",
  };

  const validationSchema = buildSearchValidationSchema();

  return (
    <Formik
      initialValues={initialValues}
      enableReinitialize
      validationSchema={validationSchema}
      onSubmit={(values, { setSubmitting }) => {
        const params = new URLSearchParams();
        Object.entries(values).forEach(([key, value]) => {
          if (value !== "") params.set(key, value);
        });

        submit(`?${params.toString()}`);
        setSubmitting(false);
      }}
    >
      {({ values, submitForm }) => {
        useEffect(() => {
          if (debounceRef.current !== null) {
            clearTimeout(debounceRef.current);
          }

          debounceRef.current = window.setTimeout(() => {
            submitForm();
          }, 300);

          return () => {
            if (debounceRef.current !== null) {
              clearTimeout(debounceRef.current);
            }
          };
        }, [values]);

        return (
          <Form
            role="search"
            className="bg-white p-6 rounded-lg shadow-md mb-8 sticky top-4 z-10"
          >
            <h2 className="text-2xl font-bold text-gray-800 mb-5">
              Filter Instruments
            </h2>
            <div className="mb-6">
              <label htmlFor="name" className="sr-only">
                Search by Instrument Name
              </label>
              <div className="relative">
                <Field
                  type="text"
                  id="name"
                  name="name"
                  placeholder="Search instruments..."
                  className="search-form-input w-full pl-10 pr-4"
                />
                <MagnifyingGlassIcon className="absolute left-3 top-1/2 transform -translate-y-1/2 h-5 w-5 text-gray-400" />
              </div>
            </div>
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
              <div>
                <label htmlFor="minPrice" className="search-form-label">
                  Min Price
                </label>
                <DecimalInputField
                  name="minPrice"
                  minValue={0}
                  maxValue={999999999}
                  placeholder="e.g., 50"
                />
              </div>
              <div>
                <label htmlFor="maxPrice" className="search-form-label">
                  Max Price
                </label>
                <DecimalInputField
                  name="maxPrice"
                  minValue={0}
                  maxValue={999999999}
                  placeholder="e.g., 500"
                />
                <ErrorMessage
                  name="maxPrice"
                  component="div"
                  className="text-red-500 text-sm mt-1"
                />
              </div>
              <div>
                <label htmlFor="manufacturer" className="search-form-label">
                  Manufacturer
                </label>
                <TextInputField
                  name="manufacturer"
                  placeholder="e.g., Fender"
                />
              </div>
            </div>
          </Form>
        );
      }}
    </Formik>
  );
};

export default InstrumentSearchForm;
