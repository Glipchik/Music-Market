import type { InstrumentResponseModel } from "@/shared/types/instrument";
import InstrumentCard from "@/shared/ui/InstrumentCard";

interface InstrumentListProps {
  instruments: InstrumentResponseModel[];
}

const InstrumentList = ({ instruments }: InstrumentListProps) => {
  if (!instruments || instruments.length === 0) {
    return (
      <div className="text-center text-gray-600 py-8">
        No instruments to display.
      </div>
    );
  }

  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-6">
      {instruments.map((instrument) => (
        <InstrumentCard key={instrument.id} instrument={instrument} />
      ))}
    </div>
  );
};

export default InstrumentList;
