import api from "@/shared/api/axiosInstance";
import type { Claim } from "../store/types";
import axios from "axios";

export const getUserInfo = async () => {
  try {
    const response = await api.get<Claim[]>("/bff/user");

    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error) && error.response?.status === 401) {
      return null;
    }

    throw new Response(null, { status: 500 });
  }
};
