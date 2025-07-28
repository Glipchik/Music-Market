export interface FormFieldDescriptorResponseModel {
  name: string;
  label: string;
  type: string;
  isRequired: boolean;
  defaultValue?: unknown;
  options?: string[];
  placeholder?: string;
}
