import { getInstrumentDetails } from "@/shared/api";
import type { LoaderFunctionArgs } from "react-router-dom";

export const loader = async ({ params }: LoaderFunctionArgs) => {
  const id = params.id!;

  const instrument = await getInstrumentDetails(id);

  return instrument;
};
