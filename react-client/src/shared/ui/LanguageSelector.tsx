import React from "react";
import { useTranslation } from "react-i18next";

const LanguageSelector = () => {
  const { t, i18n } = useTranslation();

  const changeLanguage = (event: React.ChangeEvent<HTMLSelectElement>) => {
    i18n.changeLanguage(event.target.value);
  };

  return (
    <div className="language-selector">
      <label htmlFor="language-select" className="sr-only">
        {t("language")}:
      </label>
      <select
        id="language-select"
        onChange={changeLanguage}
        value={i18n.language}
        className="px-2 py-1 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 
                   focus:border-indigo-500 text-gray-700 text-sm appearance-none"
      >
        <option value="en">EN</option>
        <option value="ru">RU</option>
        <option value="de">DE</option>
      </select>
    </div>
  );
};

export default LanguageSelector;
