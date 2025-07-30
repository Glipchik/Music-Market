import type { PropertyModel } from "@/shared/types/property";
import type {
  InstrumentDailyStatResponseModel,
  InstrumentStatResponseModel,
} from "@/shared/types/stats";

export interface UserInstrumentResponseModel {
  id: string;
  name: string;
  price: number;
  manufacturer: string;
  description?: string;
  photoUrls: string[];
  type: string;
  properties: PropertyModel[];
  totalStats: InstrumentStatResponseModel;
  dailyStats: InstrumentDailyStatResponseModel[];
}
