import type { ChangeEvent } from "react";
import { useLoaderData, useSearchParams } from "react-router-dom";
import { useCategoriesStore } from "@/features/instrumentCategories/store";
import InstrumentForm from "@/widgets/InstrumentForm/InstrumentForm";
import type { FormFieldDescriptorResponseModel } from "@/shared/types/form";

const CreateInstrumentPage = () => {
  const formSchema = useLoaderData<FormFieldDescriptorResponseModel[]>();
  const [searchParams, setSearchParams] = useSearchParams();

  const instrumentTypes = useCategoriesStore((state) => state.instrumentTypes);

  const currentSelectedType = searchParams.get("type") ?? "";

  const handleTypeChange = (e: ChangeEvent<HTMLSelectElement>) => {
    const newType = e.target.value;
    const updatedParams = new URLSearchParams(searchParams);

    if (!newType) {
      updatedParams.delete("type");
    } else {
      updatedParams.set("type", newType);
    }
    setSearchParams(updatedParams);
  };

  return (
    <div className="container mx-auto px-4 py-8 max-w-3xl">
      <h1 className="mb-8 text-center text-3xl font-bold text-gray-800">
        Create New Instrument Listing
      </h1>

      <div className="bg-white p-8 rounded-lg shadow-md mb-6">
        <label
          htmlFor="instrumentTypeSelector"
          className="block text-sm font-medium text-gray-700 mb-1"
        >
          Select Instrument Type
        </label>
        <select
          id="instrumentTypeSelector"
          name="instrumentType"
          value={currentSelectedType}
          onChange={handleTypeChange}
          className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm p-2"
          required
        >
          <option value="">-- Please select a type --</option>
          {instrumentTypes?.map((type) => (
            <option key={type.value} value={type.value}>
              {type.label}
            </option>
          ))}
        </select>
      </div>

      {currentSelectedType && (
        <InstrumentForm
          key={currentSelectedType}
          formSchema={formSchema}
          instrumentType={currentSelectedType}
        />
      )}
    </div>
  );
};

export default CreateInstrumentPage;
