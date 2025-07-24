import api from "@/shared/api/axiosInstance";
import type { Claim } from "../model/types";

export const getUserInfo = async () => {
  const response = await api.get<Claim[]>("/bff/user");

  return response;
};
