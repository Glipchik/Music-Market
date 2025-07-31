import InstrumentDetails from "@/features/instrumentDetails/ui/InstrumentDetails";
import type { InstrumentResponseModel } from "@/shared/types/instrument";
import { useLoaderData } from "react-router-dom";

const InstrumentDetailsPage = () => {
  const instrument = useLoaderData<InstrumentResponseModel>();

  return (
    <div className="container mx-auto px-4 py-8">
      <h1 className="mb-2">{instrument.name} Details</h1>
      <InstrumentDetails instrument={instrument} />
    </div>
  );
};

export default InstrumentDetailsPage;
