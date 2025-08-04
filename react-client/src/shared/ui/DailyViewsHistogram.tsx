import type { InstrumentDailyStatResponseModel } from "../types/stats";
import { useTranslation } from "react-i18next";

interface DailyViewsHistogramProps {
  dailyStats: InstrumentDailyStatResponseModel[];
}

const DailyViewsHistogram = ({ dailyStats }: DailyViewsHistogramProps) => {
  const { t, i18n } = useTranslation("dailyViewsHistogram");

  if (dailyStats.length === 0) {
    return <div className="text-center text-gray-500 py-4">{t("noData")}</div>;
  }

  const maxDailyViews = Math.max(
    1,
    dailyStats.reduce((max, stat) => Math.max(max, stat.views), 0)
  );

  const histogramBarHeightUnit = 80 / maxDailyViews;

  return (
    <div className="mb-4">
      <h4 className="font-semibold text-gray-800 mb-3">{t("title")}</h4>
      <div className="flex justify-around items-end h-36 bg-gray-50 p-2 rounded-md border border-gray-200">
        {dailyStats
          .sort(
            (a, b) => new Date(a.date).getTime() - new Date(b.date).getTime()
          )
          .map((stat) => (
            <div
              key={stat.date}
              className="flex flex-col items-center justify-end flex-1 mx-0.5"
            >
              {stat.views > 0 && (
                <span className="text-xs font-semibold text-gray-700 mb-1 leading-none">
                  {stat.views}
                </span>
              )}
              <div
                className="relative bg-gray-200 bg-opacity-50 w-full transition-all duration-300 ease-out"
                style={{
                  height: `${stat.views * histogramBarHeightUnit}px`,
                  minHeight: stat.views > 0 ? "5px" : "0",
                }}
                title={`${new Date(stat.date).toLocaleDateString(undefined, {
                  month: "short",
                  day: "numeric",
                  year: "numeric",
                })}: ${stat.views} views`}
              >
                {stat.views > 0 && (
                  <div className="absolute top-0 left-0 w-full h-1 bg-indigo-700"></div>
                )}
              </div>
              <span className="text-xs text-gray-500 mt-1">
                {new Date(stat.date).toLocaleDateString(i18n.language, {
                  day: "numeric",
                  month: "short",
                })}
              </span>
            </div>
          ))}
      </div>
    </div>
  );
};

export default DailyViewsHistogram;
