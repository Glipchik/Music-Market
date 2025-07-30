import * as Yup from "yup";

export const buildSearchValidationSchema = () => {
  return Yup.object().shape({
    name: Yup.string(),
    manufacturer: Yup.string(),
    minPrice: Yup.number(),
    maxPrice: Yup.number().test(
      "max-gte-min",
      "Max price must be greater than or equal to Min price",
      function (value) {
        const { minPrice } = this.parent;
        if (value != null && minPrice != null) {
          return value >= minPrice;
        }
        return true;
      }
    ),
  });
};
