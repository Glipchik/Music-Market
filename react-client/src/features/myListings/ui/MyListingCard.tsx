import { useState, useRef } from "react";
import DailyViewsHistogram from "@/shared/ui/DailyViewsHistogram";
import { Form, Link, useNavigate, useSubmit } from "react-router-dom";
import noImage from "@/assets/no-image.jpg";
import type { UserInstrumentResponseModel } from "../model/types";
import { formatPrice } from "@/shared/lib/formatters/formatPrice";
import ConfirmationModal from "@/shared/ui/ConfirmationModal";
import { useTranslation } from "react-i18next";

interface MyListingCardProps {
  instrument: UserInstrumentResponseModel;
}

const MyListingCard = ({ instrument }: MyListingCardProps) => {
  const { t } = useTranslation("myListings");
  const navigate = useNavigate();
  const [isModalOpen, setIsModalOpen] = useState(false);
  const formRef = useRef<HTMLFormElement>(null);
  const submit = useSubmit();

  const imageUrl =
    instrument.photoUrls && instrument.photoUrls.length > 0
      ? instrument.photoUrls[0]
      : noImage;

  const formattedPrice = formatPrice(instrument.price);

  const handleCardClick = () => {
    navigate(`/instruments/${instrument.id}`);
  };

  const handleDeleteSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setIsModalOpen(true);
  };

  const handleConfirmDelete = () => {
    setIsModalOpen(false);
    if (formRef.current) {
      submit(formRef.current);
    }
  };

  const handleCancelDelete = () => {
    setIsModalOpen(false);
  };

  return (
    <div
      className="bg-white rounded-xl shadow-lg hover:shadow-xl transition-all 
    duration-300 overflow-hidden flex flex-col sm:flex-row border border-gray-100"
    >
      <div
        onClick={handleCardClick}
        className="w-full h-48 sm:w-64 sm:h-auto flex-shrink-0 cursor-pointer overflow-hidden rounded-t-xl sm:rounded-l-xl 
        sm:rounded-t-none flex items-center justify-center bg-gray-50 group-hover:scale-[1.02] transition-transform duration-300"
      >
        <img
          className="w-full max-h-60 object-contain"
          src={imageUrl}
          alt={instrument.name}
        />
      </div>
      <div className="p-5 flex flex-col justify-between flex-grow">
        <div>
          <h3
            className="text-2xl font-bold text-gray-900 mb-2 break-words leading-tight 
            hover:text-indigo-700 cursor-pointer transition-colors duration-200"
            onClick={handleCardClick}
          >
            {instrument.name}
          </h3>
          <p className="text-indigo-600 text-3xl font-extrabold mb-4">
            {formattedPrice}
          </p>
          <div className="flex flex-wrap gap-2 mb-4">
            <span className="inline-block bg-indigo-50 text-indigo-700 text-sm font-medium px-3 py-1 rounded-full shadow-sm">
              #{instrument.type}
            </span>
          </div>
        </div>
        <div className="mt-4 border-t border-gray-200 pt-4">
          <div className="grid grid-cols-2 gap-x-3 text-sm text-gray-500 mb-4 text-center">
            <div className="flex flex-col items-center">
              <p className="font-bold text-gray-800 text-xl">
                {instrument.totalStats?.views || 0}
              </p>
              <p className="text-sm">{t("views")}</p>
            </div>
            <div className="flex flex-col items-center">
              <p className="font-bold text-gray-800 text-xl">
                {instrument.totalStats?.contactViews || 0}
              </p>
              <p className="text-sm">{t("contactViews")}</p>
            </div>
          </div>
          <div className="mb-4">
            <DailyViewsHistogram dailyStats={instrument.dailyStats || []} />
          </div>
          <div className="flex gap-4 mt-4">
            <Link
              to={`${instrument.id}/edit`}
              className="btn-base flex-1 text-center py-2 px-4 rounded-lg
              bg-blue-600 text-white font-semibold
              hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-opacity-50
              transition-colors duration-200"
            >
              {t("edit")}
            </Link>
            <Form
              method="post"
              action={`${instrument.id}/delete`}
              onSubmit={handleDeleteSubmit}
              className="flex-1"
              ref={formRef}
            >
              <button
                type="submit"
                className="btn-base w-full py-2 px-4 rounded-lg
                bg-rose-600 text-white font-semibold
                hover:bg-rose-700 focus:outline-none focus:ring-2 focus:ring-rose-500 focus:ring-opacity-50
                transition-colors duration-200"
              >
                {t("delete")}
              </button>
            </Form>
          </div>
        </div>
      </div>
      <ConfirmationModal
        isOpen={isModalOpen}
        onConfirm={handleConfirmDelete}
        onCancel={handleCancelDelete}
        title={t("deleteConfirmTitle")}
        message={t("deleteConfirmMessage", { name: instrument.name })}
      />
    </div>
  );
};

export default MyListingCard;
