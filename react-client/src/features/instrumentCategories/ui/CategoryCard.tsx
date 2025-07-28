import { Link } from "react-router-dom";
import type { Category } from "../store/types";

interface CategoryCardProps {
  category: Category;
}

const CategoryCard = ({ category }: CategoryCardProps) => {
  const { value, label, iconPath } = category;

  return (
    <Link
      to={`/instruments/type/${value}`}
      className="group flex flex-col items-center p-6 text-center card"
    >
      {iconPath && (
        <img
          src={iconPath}
          alt={`${label} icon`}
          className="w-32 h-32 mb-4 object-contain transition-transform duration-300 group-hover:scale-120"
        />
      )}
      <h3>{label}</h3>
    </Link>
  );
};

export default CategoryCard;
