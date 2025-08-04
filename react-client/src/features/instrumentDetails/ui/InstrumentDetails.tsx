import { useState } from "react";
import type { InstrumentResponseModel } from "@/shared/types/instrument";
import ImageGallery from "@/shared/ui/ImageGallery";
import ContactsModal from "./ContactsModal";
import type { UserContactsModel } from "../model/types";
import { formatPrice } from "@/shared/lib/formatters/formatPrice";
import { useAuthStore } from "@/features/auth/store";
import { getInstrumentContacts } from "@/shared/api";
import { useTranslation } from "react-i18next";

interface InstrumentDetailsProps {
  instrument: InstrumentResponseModel;
}

const InstrumentDetails = ({ instrument }: InstrumentDetailsProps) => {
  const { t } = useTranslation(["instrumentDetails", "instrumentForm"]);

  const [sellerContacts, setSellerContacts] =
    useState<UserContactsModel | null>(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isLoadingContacts, setIsLoadingContacts] = useState(false);
  const login = useAuthStore((state) => state.login);

  if (!instrument) {
    return <div className="text-center text-gray-600 py-16">{t("noData")}</div>;
  }

  const formattedPrice = formatPrice(instrument.price);

  const handleShowContacts = async () => {
    setIsModalOpen(true);
    setIsLoadingContacts(true);
    const contacts = await getInstrumentContacts(instrument.id);
    setSellerContacts(contacts);
    setIsLoadingContacts(false);
  };

  const closeModal = () => {
    setIsModalOpen(false);
    setSellerContacts(null);
    setIsLoadingContacts(false);
  };

  const handleLoginRedirect = () => {
    closeModal();
    login();
  };

  return (
    <div className="bg-white rounded-lg shadow-2xl overflow-hidden">
      <div className="p-8 grid grid-cols-1 md:grid-cols-2 gap-x-12 gap-y-8">
        <ImageGallery
          photoUrls={instrument.photoModels.map(
            (photoModel) => photoModel.photoUrl
          )}
          altText={instrument.name}
        />
        <div>
          <h2 className="text-3xl font-bold text-gray-900 mb-2 leading-tight">
            {instrument.name}
          </h2>
          <p className="text-4xl font-extrabold text-indigo-700 mb-6">
            {formattedPrice}
          </p>

          <div className="text-gray-700 text-lg mb-2">
            <span className="font-semibold">{t("manufacturer")}:</span>{" "}
            {instrument.manufacturer}
          </div>
          <div className="text-gray-700 text-lg mb-6">
            <span className="font-semibold">{t("type")}:</span>{" "}
            {instrument.type}
          </div>
          <button
            onClick={handleShowContacts}
            className="btn-base w-full bg-green-600 text-white py-3 rounded-lg text-lg hover:bg-green-700 transition duration-300"
          >
            {t("showContacts")}
          </button>
        </div>
      </div>
      <div className="p-8 pt-0 grid grid-cols-1 md:grid-cols-2 gap-x-12 gap-y-8">
        <div>
          <h3 className="text-2xl font-bold text-gray-900 mb-3">
            {t("description")}
          </h3>
          <p className="text-gray-800 text-base leading-relaxed w-full overflow-hidden break-all whitespace-normal">
            {instrument.description}
          </p>
        </div>
        <div>
          {instrument.properties && instrument.properties.length > 0 && (
            <>
              <h3 className="text-2xl font-bold text-gray-900 mb-4">
                {t("specifications")}
              </h3>
              <ul className="list-disc list-inside text-gray-700 space-y-3">
                {instrument.properties.map((prop, index) => (
                  <li key={index}>
                    <span className="font-semibold">
                      {t(`instrumentForm:${prop.label}`)}:
                    </span>{" "}
                    {typeof prop.value === "boolean"
                      ? prop.value
                        ? t("instrumentDetails:yes")
                        : t("instrumentDetails:no")
                      : String(prop.value)}
                  </li>
                ))}
              </ul>
            </>
          )}
        </div>
      </div>
      <ContactsModal
        contacts={sellerContacts}
        isOpen={isModalOpen}
        onClose={closeModal}
        onLoginClick={handleLoginRedirect}
        isLoading={isLoadingContacts}
      />
    </div>
  );
};

export default InstrumentDetails;
