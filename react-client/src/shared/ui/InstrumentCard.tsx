import type { InstrumentResponseModel } from "@/shared/types/instrument";
import { Link } from "react-router-dom";
import noImage from "@/assets/no-image.jpg";

interface InstrumentCardProps {
  instrument: InstrumentResponseModel;
}

const InstrumentCard = ({ instrument }: InstrumentCardProps) => {
  const imageUrl =
    instrument.photoModels.length > 0
      ? instrument.photoModels[0].photoUrl
      : noImage;

  const formattedPrice = new Intl.NumberFormat("en-US", {
    style: "currency",
    currency: "USD",
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  }).format(instrument.price);

  return (
    <Link className="group card" to={`/instruments/${instrument.id}`}>
      <div className="w-full h-50 overflow-hidden bg-gray-100 flex items-center justify-center rounded-t-lg">
        <img
          className="w-full h-full object-contain"
          src={imageUrl}
          alt={instrument.name}
        />
      </div>
      <div className="px-6 py-4">
        <h3 className="mb-2 break-words leading-snug">{instrument.name}</h3>
        <p className="text-gray-700 text-base mb-2">
          <span className="font-semibold">Manufacturer:</span>{" "}
          {instrument.manufacturer}
        </p>
        <p className="text-indigo-600 text-lg font-bold">{formattedPrice}</p>
      </div>
      <div className="px-6 pt-4 pb-2">
        <span className="inline-block bg-indigo-100 rounded-full px-3 py-1 text-sm font-semibold text-indigo-700 mr-2 mb-2">
          #{instrument.type}
        </span>
      </div>
    </Link>
  );
};

export default InstrumentCard;
