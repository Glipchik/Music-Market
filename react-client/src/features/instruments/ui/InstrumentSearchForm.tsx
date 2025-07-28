import React from "react";
import { Form, useSubmit } from "react-router-dom";

interface InstrumentSearchFormProps {
  searchParams: URLSearchParams;
}

const InstrumentSearchForm = ({ searchParams }: InstrumentSearchFormProps) => {
  const submit = useSubmit();

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const form = event.currentTarget.form;
    submit(form);
  };

  return (
    <Form role="search" className="bg-white p-6 rounded-lg shadow-md mb-8">
      <h2 className="mb-4">Filter Instruments</h2>
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4 mb-4">
        <div>
          <label htmlFor="minPrice" className="search-form-label">
            Min Price
          </label>
          <input
            type="number"
            id="minPrice"
            name="minPrice"
            defaultValue={searchParams.get("minPrice") || ""}
            onChange={handleInputChange}
            className="search-form-input"
            placeholder="e.g., 50"
          />
        </div>
        <div>
          <label htmlFor="maxPrice" className="search-form-label">
            Max Price
          </label>
          <input
            type="number"
            id="maxPrice"
            name="maxPrice"
            defaultValue={searchParams.get("maxPrice") || ""}
            onChange={handleInputChange}
            className="search-form-input"
            placeholder="e.g., 500"
          />
        </div>
        <div>
          <label htmlFor="manufacturer" className="search-form-label">
            Manufacturer
          </label>
          <input
            type="text"
            id="manufacturer"
            name="manufacturer"
            defaultValue={searchParams.get("manufacturer") || ""}
            onChange={handleInputChange}
            className="search-form-input"
            placeholder="e.g., Fender"
          />
        </div>
        <div>
          <label htmlFor="instrumentName" className="search-form-label">
            Instrument Name
          </label>
          <input
            type="text"
            id="instrumentName"
            name="name"
            defaultValue={searchParams.get("name") || ""}
            onChange={handleInputChange}
            className="search-form-input"
            placeholder="e.g., Stratocaster"
          />
        </div>
      </div>
    </Form>
  );
};

export default InstrumentSearchForm;
