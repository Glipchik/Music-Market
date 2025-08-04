import type { FormFieldDescriptorResponseModel } from "@/shared/types/form";
import type { InstrumentResponseModel } from "@/shared/types/instrument";
import { useLoaderData } from "react-router-dom";
import InstrumentForm from "@/widgets/InstrumentForm/InstrumentForm";
import { useTranslation } from "react-i18next";

const EditInstrumentPage = () => {
  const loaderData = useLoaderData() as {
    instrument: InstrumentResponseModel;
    formSchema: FormFieldDescriptorResponseModel[];
  };
  const { t } = useTranslation("instrumentForm");

  const { instrument, formSchema } = loaderData;

  return (
    <div className="container mx-auto px-4 py-8 max-w-3xl">
      <h1 className="mb-8 text-center text-3xl font-bold text-gray-800">
        {t("editTitle", { name: instrument.name })}
      </h1>
      <InstrumentForm
        formSchema={formSchema}
        instrumentType={instrument.type}
        instrument={instrument}
      />
    </div>
  );
};

export default EditInstrumentPage;
