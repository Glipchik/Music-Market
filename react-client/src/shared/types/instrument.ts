import type { PropertyModel } from "./property";

export interface InstrumentResponseModel {
  id: string;
  name: string;
  price: number;
  manufacturer: string;
  description?: string;
  photoModels: PhotoModel[];
  type: string;
  properties: PropertyModel[];
}

export interface PhotoModel {
  photoName: string,
  photoUrl: string
}
