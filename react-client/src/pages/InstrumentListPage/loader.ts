import type { LoaderFunctionArgs } from "react-router-dom";
import { DEFAULT_PAGE_SIZE } from "@/shared/config/constants";
import { getFilteredInstruments } from "@/shared/api";

export const loader = async ({ request, params }: LoaderFunctionArgs) => {
  const url = new URL(request.url);
  const typeId = params.typeId;
  const page = url.searchParams.get("page") ?? "1";
  const pageSize =
    url.searchParams.get("pageSize") ?? DEFAULT_PAGE_SIZE.toString();

  const minPrice = url.searchParams.get("minPrice");
  const maxPrice = url.searchParams.get("maxPrice");
  const manufacturer = url.searchParams.get("manufacturer");
  const instrumentName = url.searchParams.get("name");

  const queryParams = new URLSearchParams();
  queryParams.append("page", page);
  queryParams.append("pageSize", pageSize);

  if (typeId) queryParams.append("type", typeId);
  if (minPrice) queryParams.append("minPrice", minPrice);
  if (maxPrice) queryParams.append("maxPrice", maxPrice);
  if (manufacturer) queryParams.append("manufacturer", manufacturer);
  if (instrumentName) queryParams.append("name", instrumentName);

  const instruments = await getFilteredInstruments(queryParams.toString());

  return instruments;
};
