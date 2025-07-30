export interface ValidationRuleResponseModel {
  ruleName: string;
  ruleValue?: unknown;
  errorMessage?: string;
}

export interface FormFieldDescriptorResponseModel {
  name: string;
  label: string;
  type: string;
  isRequired: boolean;
  defaultValue?: unknown;
  options?: string[];
  placeholder?: string;
  validationRules: ValidationRuleResponseModel[];
}
