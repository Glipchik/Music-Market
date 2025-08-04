import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";

const Footer = () => {
  const { t } = useTranslation("footer");
  const year = new Date().getFullYear();

  return (
    <footer className="w-full bg-gray-800 text-white px-6 py-8 mt-auto rounded-t-lg shadow-inner">
      <div className="max-w-7xl mx-auto flex flex-col md:flex-row justify-between items-center text-center md:text-left">
        <div className="mb-4 md:mb-0">
          <p className="text-sm text-gray-400">{t("copyright", { year })}</p>
          <p className="text-xs text-gray-500 mt-1">{t("designed")}</p>
        </div>

        <nav className="flex flex-wrap justify-center md:justify-end gap-x-6 gap-y-2 text-sm font-medium">
          <Link
            to="/"
            className="text-gray-300 hover:text-indigo-400 transition-colors duration-200"
          >
            {t("home")}
          </Link>
          <Link
            to="/about"
            className="text-gray-300 hover:text-indigo-400 transition-colors duration-200"
          >
            {t("about")}
          </Link>
          <Link
            to="/contact"
            className="text-gray-300 hover:text-indigo-400 transition-colors duration-200"
          >
            {t("contact")}
          </Link>
          <Link
            to="/privacy"
            className="text-gray-300 hover:text-indigo-400 transition-colors duration-200"
          >
            {t("privacy")}
          </Link>
          <Link
            to="/terms"
            className="text-gray-300 hover:text-indigo-400 transition-colors duration-200"
          >
            {t("terms")}
          </Link>
          <a
            href="https://github.com/Glipchik/Music-Market"
            target="_blank"
            rel="noopener noreferrer"
            className="text-gray-300 hover:text-indigo-400 transition-colors duration-200"
          >
            {t("github")}
          </a>
        </nav>
      </div>
    </footer>
  );
};

export default Footer;
