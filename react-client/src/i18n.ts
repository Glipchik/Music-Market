import i18n from "i18next";
import { initReactI18next } from "react-i18next";
import LanguageDetector from "i18next-browser-languagedetector";
import Backend from "i18next-http-backend";

const supportedLanguages = ["en", "ru", "de"];

i18n
  .use(Backend)
  .use(LanguageDetector)
  .use(initReactI18next)
  .init({
    backend: {
      loadPath: "/locales/{{lng}}/{{ns}}.json",
    },
    supportedLngs: supportedLanguages,
    fallbackLng: "en",
    debug: true,
    interpolation: {
      escapeValue: false,
    },
  });

export default i18n;
