import { getTopInstruments } from "@/shared/api";

export const loader = async () => {
  const instruments = await getTopInstruments();

  return instruments;
};
