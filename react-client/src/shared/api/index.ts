import axios from "axios";
import type { FormFieldDescriptorResponseModel } from "../types/form";
import api from "./axiosInstance";
import type { Category } from "@/features/instrumentCategories/store/types";
import type { UserContactsModel } from "@/features/instrumentDetails/model/types";
import type { InstrumentResponseModel } from "../types/instrument";
import { TOP_INSTRUMENTS_LIMIT } from "../config/constants";
import type { PaginatedModel } from "../types/pagination";
import type { UserInstrumentResponseModel } from "@/features/myListings/model/types";

export const getFormMetadata = async (type: string) => {
  try {
    const response = await api.get<FormFieldDescriptorResponseModel[]>(
      `/api/instruments/form/${type}`
    );
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error) && error.response?.status) {
      throw new Response(null, { status: error.response.status });
    }
    throw new Response(null, { status: 500 });
  }
};

export const getInstrumentTypes = async () => {
  try {
    const response = await api.get<Category[]>("/api/instruments/types");
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error) && error.response?.status) {
      throw new Response(null, { status: error.response.status });
    }
    throw new Response(null, { status: 500 });
  }
};

export const getInstrumentContacts = async (instrumentId: string) => {
  try {
    const response = await api.get<UserContactsModel>(
      `/api/instruments/${instrumentId}/contacts`
    );
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error) && error.response?.status) {
      throw new Response(null, { status: error.response.status });
    }
    throw new Response(null, { status: 500 });
  }
};

export const getInstrumentDetails = async (instrumentId: string) => {
  try {
    const response = await api.get<InstrumentResponseModel>(
      `/api/instruments/${instrumentId}`
    );

    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error) && error.response?.status) {
      throw new Response(null, { status: error.response.status });
    }
    throw new Response(null, { status: 500 });
  }
};

export const getTopInstruments = async (limit: number | null = null) => {
  try {
    const response = await api.get<InstrumentResponseModel[]>(
      `/api/instruments/top?limit=${limit ?? TOP_INSTRUMENTS_LIMIT}`
    );
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error) && error.response?.status) {
      throw new Response(null, { status: error.response.status });
    }
    throw new Response(null, { status: 500 });
  }
};

export const getFilteredInstruments = async (queryParamsString: string) => {
  try {
    const response = await api.get<PaginatedModel<InstrumentResponseModel>>(
      `/api/instruments?${queryParamsString}`
    );
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error) && error.response?.status) {
      throw new Response(null, { status: error.response.status });
    }
    throw new Response(null, { status: 500 });
  }
};

export const uploadFiles = async (files: File[]) => {
  try {
    if (files.length === 0) return [];

    const formData = new FormData();
    files.forEach((file) => formData.append("files", file));
    const response = await api.post<string[]>(
      `/api/instruments/files?folder=instruments`,
      formData
    );
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error) && error.response?.status) {
      throw new Response(null, { status: error.response.status });
    }
    throw new Response(null, { status: 500 });
  }
};

export const createInstrument = async (payload: Record<string, unknown>) => {
  try {
    const response = await api.post<InstrumentResponseModel>(
      "/api/instruments",
      payload
    );
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error) && error.response?.status) {
      throw new Response(null, { status: error.response.status });
    }
    throw new Response(null, { status: 500 });
  }
};

export const deleteFiles = async (fileNames: string[]) => {
  try {
    await api.delete("/api/instruments/files", {
      data: fileNames,
    });
  } catch (error) {
    if (axios.isAxiosError(error) && error.response?.status) {
      throw new Response(null, { status: error.response.status });
    }
    throw new Response(null, { status: 500 });
  }
};

export const updateInstrument = async (
  id: string,
  payload: Record<string, unknown>
) => {
  try {
    await api.put(`/api/instruments/${id}`, payload);
  } catch (error) {
    if (axios.isAxiosError(error) && error.response?.status) {
      throw new Response(null, { status: error.response.status });
    }
    throw new Response(null, { status: 500 });
  }
};

export const getUserInstruments = async (queryParamsString: string) => {
  try {
    const response = await api.get<PaginatedModel<UserInstrumentResponseModel>>(
      `/api/instruments/my?${queryParamsString}`
    );
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error) && error.response?.status) {
      throw new Response(null, { status: error.response.status });
    }
    throw new Response(null, { status: 500 });
  }
};

export const deleteInstrumentById = async (id: string) => {
  try {
    await api.delete(`/api/instruments/${id}`);
  } catch (error) {
    if (axios.isAxiosError(error) && error.response?.status) {
      throw new Response(null, { status: error.response.status });
    }
    throw new Response(null, { status: 500 });
  }
};
