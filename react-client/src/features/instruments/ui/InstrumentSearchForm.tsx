import React from "react";
import { Form, useSubmit } from "react-router-dom";
import { MagnifyingGlassIcon } from "@heroicons/react/24/outline";

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
      <h2 className="text-2xl font-bold text-gray-800 mb-5">
        Filter Instruments
      </h2>
      <div className="mb-6">
        <label htmlFor="instrumentName" className="sr-only">
          Search by Instrument Name
        </label>
        <div className="relative">
          <input
            type="text"
            id="instrumentName"
            name="name"
            defaultValue={searchParams.get("name") || ""}
            onChange={handleInputChange}
            className="search-form-input w-full pl-10 pr-4"
            placeholder="Search instruments..."
          />
          <MagnifyingGlassIcon className="absolute left-3 top-1/2 transform -translate-y-1/2 h-5 w-5 text-gray-400" />
        </div>
      </div>
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
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
      </div>
    </Form>
  );
};

export default InstrumentSearchForm;
