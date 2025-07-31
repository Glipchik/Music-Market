import TopInstrumentList from "@/features/instruments/ui/TopInstrumentList";
import CategoryList from "@/features/instrumentCategories/ui/CategoryList";
import { useLoaderData } from "react-router-dom";
import { useCategoriesStore } from "@/features/instrumentCategories/store";
import type { InstrumentResponseModel } from "@/shared/types/instrument";

const HomePage = () => {
  const instruments = useLoaderData<InstrumentResponseModel[]>();

  const instrumentTypes = useCategoriesStore((state) => state.instrumentTypes);

  return (
    <div className="container mx-auto px-4 py-8">
      <section className="text-center mb-12">
        <h1>Welcome to Instrument Haven!</h1>
        <p className="text-xl text-gray-700 max-w-2xl mx-auto">
          Discover your perfect melody. Explore our exquisite collection of
          musical instruments.
        </p>
      </section>
      <section className="mb-12">
        <CategoryList categories={instrumentTypes} />
      </section>
      <section>
        <TopInstrumentList instruments={instruments} />
      </section>
    </div>
  );
};

export default HomePage;
