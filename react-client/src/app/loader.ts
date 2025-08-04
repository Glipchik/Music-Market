import { useAuthStore } from "@/features/auth/store";
import { useCategoriesStore } from "@/features/instrumentCategories/store";

export const loader = async () => {
  await useAuthStore.getState().fetchUser();
  await useCategoriesStore.getState().fetchCategories();
};
