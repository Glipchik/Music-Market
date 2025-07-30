import InstrumentCard from "@/shared/ui/InstrumentCard";
import { TOP_INSTRUMENTS_LIMIT } from "@/shared/config/constants";
import type { InstrumentResponseModel } from "@/shared/types/instrument";

interface TopInstrumentListProps {
  instruments: InstrumentResponseModel[];
}

const TopInstrumentList = ({ instruments }: TopInstrumentListProps) => {
  if (instruments.length === 0) {
    return (
      <div className="flex justify-center items-center h-48">
        <p className="text-lg text-gray-500">
          No popular instruments available yet.
        </p>
      </div>
    );
  }

  return (
    <section className="py-8">
      <h2 className="text-center mb-8">Most Popular Instruments</h2>
      <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-5 gap-6 px-4">
        {instruments
          .slice(0, TOP_INSTRUMENTS_LIMIT)
          .map((instrument: InstrumentResponseModel) => (
            <InstrumentCard key={instrument.id} instrument={instrument} />
          ))}
      </div>
    </section>
  );
};

export default TopInstrumentList;
