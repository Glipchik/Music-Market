import { NavLink } from "react-router-dom";
import type { Category } from "../store/types";

interface CategoryNavListProps {
  instrumentTypes: Category[];
  activeTypeId: string | undefined;
}

const CategoryNavList = ({
  instrumentTypes,
  activeTypeId,
}: CategoryNavListProps) => {
  if (instrumentTypes.length === 0) {
    return (
      <div className="py-4 text-center text-gray-500 text-sm">
        No categories available.
      </div>
    );
  }

  return (
    <div className="bg-white p-6 rounded-lg shadow-md border border-gray-100 h-full">
      <h3 className="mb-4 border-b pb-3 border-gray-200">Instrument Types</h3>
      <nav>
        <ul className="space-y-2">
          {instrumentTypes.map((category) => (
            <li key={category.value}>
              <NavLink
                to={`/instruments/type/${category.value}`}
                className={({ isActive }: { isActive: boolean }) =>
                  `flex items-center gap-3 p-2 rounded-md text-gray-700 hover:bg-gray-100 hover:text-indigo-600 transition-colors duration-200
                   ${
                     isActive || category.value === activeTypeId
                       ? "bg-indigo-50 text-indigo-700 font-semibold"
                       : ""
                   }`
                }
              >
                {category.iconPath && (
                  <img
                    src={category.iconPath}
                    alt={`${category.label} icon`}
                    className="w-6 h-6 object-contain"
                  />
                )}
                <span>{category.label}</span>
              </NavLink>
            </li>
          ))}
        </ul>
      </nav>
    </div>
  );
};

export default CategoryNavList;
