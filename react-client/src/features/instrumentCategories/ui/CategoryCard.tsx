import { Link } from "react-router-dom";
import type { Category } from "../store/types";
import { useTranslation } from "react-i18next";

interface CategoryCardProps {
  category: Category;
}

const CategoryCard = ({ category }: CategoryCardProps) => {
  const { value, labelKey, iconPath } = category;
  const { t } = useTranslation("categories");

  return (
    <Link
      to={`/instruments/type/${value}`}
      className="group flex flex-col items-center p-6 text-center card"
    >
      {iconPath && (
        <img
          src={iconPath}
          alt={`${t(labelKey)} icon`}
          className="w-32 h-32 mb-4 object-contain transition-transform duration-300 group-hover:scale-120"
        />
      )}
      <h3>{t(labelKey)}</h3>
    </Link>
  );
};

export default CategoryCard;
