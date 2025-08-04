import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";

const NotFoundErrorPage = () => {
  const { t } = useTranslation("notFound");

  return (
    <div className="bg-gradient-to-br from-blue-50 to-indigo-100 flex flex-col justify-center items-center p-4 sm:p-6 lg:p-8">
      <div
        className="bg-white p-12 rounded-3xl shadow-2xl text-center max-w-xl w-full border border-gray-100 
        transform transition-all duration-500 ease-out hover:scale-102 hover:shadow-3xl"
      >
        <span className="text-8xl font-extrabold text-blue-600 block mb-6 leading-none">
          404
        </span>
        <h1 className="text-5xl font-bold text-gray-800 mb-5 tracking-tight">
          {t("title")}
        </h1>
        <p className="text-xl text-gray-700 mb-10 leading-relaxed">
          {t("message")}
        </p>
        <Link
          to="/"
          className="bg-blue-600 text-white font-semibold py-4 px-10 rounded-full shadow-lg hover:bg-blue-700 
          focus:outline-none focus:ring-4 focus:ring-blue-300 transition duration-300 ease-in-out transform hover:-translate-y-1"
        >
          {t("button")}
        </Link>
      </div>
    </div>
  );
};

export default NotFoundErrorPage;
