export interface ValidationRuleResponseModel {
  ruleName: string;
  ruleValue?: unknown;
  errorMessageKey?: string;
}

export interface FormFieldDescriptorResponseModel {
  name: string;
  labelKey: string;
  placeholderKey?: string;
  type: string;
  isRequired: boolean;
  defaultValue?: unknown;
  defaultValueKey?: string;
  options?: string[];
  optionKeys?: string[];
  validationRules: ValidationRuleResponseModel[];
}
