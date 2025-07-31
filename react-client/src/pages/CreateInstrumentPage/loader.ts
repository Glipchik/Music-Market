import { getFormMetadata } from "@/shared/api";
import type { FormFieldDescriptorResponseModel } from "@/shared/types/form";
import type { LoaderFunctionArgs } from "react-router-dom";

export const loader = async ({ request }: LoaderFunctionArgs) => {
  const url = new URL(request.url);
  const selectedType = url.searchParams.get("type");

  let formSchema: FormFieldDescriptorResponseModel[] = [];

  if (selectedType) {
    formSchema = await getFormMetadata(selectedType);
  }

  return formSchema;
};
