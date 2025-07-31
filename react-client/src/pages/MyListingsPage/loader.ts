import { getUserInstruments } from "@/shared/api";
import { DEFAULT_PAGE_SIZE } from "@/shared/config/constants";
import type { LoaderFunctionArgs } from "react-router-dom";

export const loader = async ({ request }: LoaderFunctionArgs) => {
  const url = new URL(request.url);
  const page = url.searchParams.get("page") ?? "1";
  const pageSize =
    url.searchParams.get("pageSize") ?? DEFAULT_PAGE_SIZE.toString();

  const queryParams = new URLSearchParams();
  queryParams.append("page", page);
  queryParams.append("pageSize", pageSize);

  const instruments = await getUserInstruments(queryParams.toString());

  return instruments;
};
