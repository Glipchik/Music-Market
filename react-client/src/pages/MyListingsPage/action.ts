import { deleteInstrumentById } from "@/shared/api";
import { redirect, type ActionFunctionArgs } from "react-router-dom";

export const deleteInstrumentAction = async ({
  params,
}: ActionFunctionArgs) => {
  const id = params.id!;

  await deleteInstrumentById(id);

  return redirect("/my-listings");
};
