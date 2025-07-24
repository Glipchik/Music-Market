import { create } from "zustand";
import { getUserInfo } from "../api";
import type { User } from "./types";
import {
  API_BASE_URL,
  REACT_APP_URL,
  IDENTITY_PROVIDER_BASE_URL,
} from "@/shared/config/constants";

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
      const claims = await getUserInfo();

      if (!claims) {
        set({ user: null });
        return;
      }

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
      set({ user: null });
      throw error;
    }
  },

  login: () => {
    window.location.href = `${API_BASE_URL}/bff/login?returnUrl=${REACT_APP_URL}`;
  },

  logout: async () => {
    try {
      const claims = await getUserInfo();

      const logoutUrl = claims?.find((c) => c.type === "bff:logout_url")?.value;

      if (!logoutUrl) {
        set({ user: null });
        return;
      }

      window.location.href = `${API_BASE_URL}${logoutUrl}&returnUrl=${REACT_APP_URL}`;
    } catch (error) {
      set({ user: null });
      throw error;
    }
  },

  signUp: () => {
    window.location.href = `${IDENTITY_PROVIDER_BASE_URL}/Account/Create`;
  },
}));
