import InstrumentDetails from "@/features/instrumentDetails/ui/InstrumentDetails";
import type { InstrumentResponseModel } from "@/shared/types/instrument";
import { useLoaderData } from "react-router-dom";
import { useTranslation } from "react-i18next";

const InstrumentDetailsPage = () => {
  const instrument = useLoaderData<InstrumentResponseModel>();
  const { t } = useTranslation("instrumentDetails");

  return (
    <div className="container mx-auto px-4 py-8">
      <h1 className="mb-2">{t("pageTitle", { name: instrument.name })}</h1>
      <InstrumentDetails instrument={instrument} />
    </div>
  );
};

export default InstrumentDetailsPage;
