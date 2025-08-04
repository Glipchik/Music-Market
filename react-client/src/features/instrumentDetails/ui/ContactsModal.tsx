import { useTranslation } from "react-i18next";
import type { UserContactsModel } from "../model/types";
import { LockClosedIcon } from "@heroicons/react/24/outline";

interface ContactsModalProps {
  contacts?: UserContactsModel | null;
  isOpen: boolean;
  onClose: () => void;
  onLoginClick: () => void;
  isLoading: boolean;
}

const ContactsModal = ({
  contacts,
  isOpen,
  onClose,
  onLoginClick,
  isLoading,
}: ContactsModalProps) => {
  const { t } = useTranslation("contacts");

  if (!isOpen) {
    return null;
  }

  return (
    <div className="fixed inset-0 bg-black/70 flex items-center justify-center z-[100] p-4">
      <div className="bg-white rounded-lg shadow-2xl p-6 sm:p-8 relative max-w-sm w-full md:max-w-md lg:max-w-lg text-center">
        <button
          onClick={onClose}
          className="absolute top-3 right-3 text-gray-500 hover:text-gray-800 text-3xl font-bold leading-none cursor-pointer"
          aria-label="Close modal"
        >
          &times;
        </button>
        {isLoading ? (
          <div className="flex flex-col justify-center items-center py-8">
            <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-500"></div>
            <p className="ml-4 text-xl text-gray-700 mt-4">{t("loading")}</p>
          </div>
        ) : !contacts ? (
          <div className="flex flex-col items-center justify-center py-4">
            <LockClosedIcon className="w-20 h-20 text-blue-500 mb-6" />

            <h2 className="text-3xl font-extrabold text-gray-800 mb-3">
              {t("restrictedTitle")}
            </h2>
            <p className="text-gray-600 text-lg mb-8 leading-relaxed">
              {t("restrictedMessage")}
            </p>
            <button
              onClick={(e) => {
                e.stopPropagation();
                onLoginClick();
              }}
              className="btn-base bg-blue-600 text-white text-xl px-8 py-3 rounded-full hover:bg-blue-700 
              transition duration-300 ease-in-out transform hover:scale-105 shadow-lg"
            >
              {t("loginButton")}
            </button>
          </div>
        ) : (
          <>
            <h2 className="text-2xl font-bold text-gray-900 mb-4">
              {t("contactsTitle")}
            </h2>
            <>
              <p className="text-gray-800 text-lg mb-2">
                <span className="font-semibold">{t("nameLabel")}:</span>{" "}
                {contacts.name}
              </p>
              <p className="text-gray-800 text-lg mb-4">
                <span className="font-semibold">{t("emailLabel")}:</span>{" "}
                {contacts.email}
              </p>
            </>
          </>
        )}
      </div>
    </div>
  );
};

export default ContactsModal;
