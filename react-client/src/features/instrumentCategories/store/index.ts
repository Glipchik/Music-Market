import { create } from "zustand";
import type { Category } from "./types";
import { getInstrumentTypes } from "@/shared/api";

interface CategoriesStore {
  instrumentTypes: Category[];
  fetchCategories: () => Promise<void>;
}

export const useCategoriesStore = create<CategoriesStore>((set) => ({
  instrumentTypes: [],

  fetchCategories: async () => {
    const instrumentTypes = await getInstrumentTypes();
    set({ instrumentTypes: instrumentTypes });
  },
}));
