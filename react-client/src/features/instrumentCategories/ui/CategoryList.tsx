import CategoryCard from "@/features/instrumentCategories/ui/CategoryCard";
import type { Category } from "../store/types";
import { useTranslation } from "react-i18next";

interface CategoryListProps {
  categories: Category[];
}

const CategoryList = ({ categories }: CategoryListProps) => {
  const { t } = useTranslation("categories");

  if (categories.length === 0) {
    return (
      <div className="flex justify-center items-center py-16">
        <p className="text-lg text-gray-500">{t("empty")}</p>
      </div>
    );
  }

  return (
    <section className="py-8">
      <h2 className="text-center mb-8">{t("title")}</h2>
      <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6 px-4">
        {categories.map((category) => (
          <CategoryCard key={category.value} category={category} />
        ))}
      </div>
    </section>
  );
};

export default CategoryList;
