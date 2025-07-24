import { create } from "zustand";
import { getUserInfo } from "../api/api";
import type { User } from "./types";
import {
  API_BASE_URL,
  REACT_APP_URL,
  IDENTITY_PROVIDER_BASE_URL,
} from "@/shared/config/constants";
import axios from "axios";

interface AuthStore {
  user: User | null;
  fetchUser: () => Promise<void>;
  login: () => void;
  logout: () => Promise<void>;
  signUp: () => void;
}

export const useAuthStore = create<AuthStore>((set) => ({
  user: null,

  fetchUser: async () => {
    try {
      const response = await getUserInfo();

      const claims = response.data;

      const nameClaim = claims.find((c) => c.type === "name")?.value;
      const subClaim = claims.find((c) => c.type === "sub")?.value;

      if (nameClaim && subClaim) {
        set({
          user: {
            name: nameClaim,
            sub: subClaim,
            claims,
          },
        });
      } else {
        set({ user: null });
      }
    } catch (error) {
      if (axios.isAxiosError(error)) {
        if (error.response?.status === 401) {
          set({ user: null });
        }
      }
    }
  },

  login: () => {
    window.location.href = `${API_BASE_URL}/bff/login?returnUrl=${REACT_APP_URL}`;
  },

  logout: async () => {
    try {
      const response = await getUserInfo();

      const claims = response.data;

      const logoutClaim = claims.find((c) => c.type === "bff:logout_url");
      const logoutUrl = logoutClaim?.value;

      if (logoutUrl) {
        window.location.href = `${API_BASE_URL}${logoutUrl}&returnUrl=${REACT_APP_URL}`;
      } else {
        set({ user: null });
      }
    } catch (error) {
      if (axios.isAxiosError(error)) {
        if (error.response?.status === 401) {
          set({ user: null });
        }
      }
    }
  },

  signUp: () => {
    window.location.href = `${IDENTITY_PROVIDER_BASE_URL}/Account/Create`;
  },
}));
