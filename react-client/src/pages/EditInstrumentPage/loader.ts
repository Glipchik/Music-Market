import { getFormMetadata, getInstrumentDetails } from "@/shared/api";
import type { LoaderFunctionArgs } from "react-router-dom";

export const loader = async ({ params }: LoaderFunctionArgs) => {
  const instrumentId = params.id!;

  const instrument = await getInstrumentDetails(instrumentId);
  const formSchema = await getFormMetadata(instrument.type);

  return { instrument, formSchema };
};
