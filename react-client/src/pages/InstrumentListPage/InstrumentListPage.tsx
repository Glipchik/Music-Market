import { useParams, useSearchParams } from "react-router-dom";
import InstrumentList from "@/features/instruments/ui/InstrumentList";
import PaginationControls from "@/shared/ui/PaginationControls";
import { useLoaderData } from "react-router-dom";
import CategoryNavList from "@/features/instrumentCategories/ui/CategoryNavList";
import { useCategoriesStore } from "@/features/instrumentCategories/store";
import type { InstrumentResponseModel } from "@/shared/types/instrument";
import type { PaginatedModel } from "@/shared/types/pagination";
import InstrumentSearchForm from "@/features/instruments/ui/InstrumentSearchForm";
import { useTranslation } from "react-i18next";

const InstrumentListPage = () => {
  const { typeId } = useParams();
  const [searchParams, setSearchParams] = useSearchParams();

  const { t } = useTranslation(["instrumentListPage", "categories"]);

  const instrumentTypes = useCategoriesStore((state) => state.instrumentTypes);

  const { items, page, pageSize, totalCount, totalPages } =
    useLoaderData<PaginatedModel<InstrumentResponseModel>>();

  const goToPage = (pageNumber: number) => {
    if (pageNumber < 1 || pageNumber > totalPages) return;

    setSearchParams((prev) => {
      const params = new URLSearchParams(prev);
      params.set("page", pageNumber.toString());
      return params;
    });
  };

  const handlePreviousPageClick = () => {
    goToPage(page - 1);
  };

  const handleNextPageClick = () => {
    goToPage(page + 1);
  };

  const noInstrumentsFound = items.length === 0;

  const displayTypeName = typeId ? typeId.replace(/-/g, " ") : "";

  const translatedCategoryName = typeId
    ? t(`categories:instrumentTypes.${displayTypeName}`)
    : "";

  return (
    <div className="container mx-auto px-4 py-8 flex flex-col min-h-[calc(100vh-200px)]">
      <h1 className="mb-8 capitalize">
        {t("instrumentListPage:instrumentTypeTitle", {
          type: translatedCategoryName,
        })}
      </h1>
      <div className="flex flex-col md:flex-row gap-8 flex-grow">
        <div className="md:w-1/4 lg:w-1/5 flex-shrink-0">
          <CategoryNavList
            instrumentTypes={instrumentTypes}
            activeTypeId={typeId}
          />
        </div>
        <div className="md:w-3/4 lg:w-4/5 flex flex-col">
          <InstrumentSearchForm searchParams={searchParams} />

          {noInstrumentsFound ? (
            <div className="flex justify-center items-center py-16">
              <p className="text-xl text-gray-600">
                {t("instrumentListPage:noInstrumentsFound", {
                  type: translatedCategoryName,
                })}
              </p>
            </div>
          ) : (
            <InstrumentList instruments={items} />
          )}
          {totalPages > 1 && (
            <div className="mt-auto">
              <PaginationControls
                currentPage={page}
                totalPages={totalPages}
                totalCount={totalCount}
                pageSize={pageSize}
                onPreviousPageClick={handlePreviousPageClick}
                onNextPageClick={handleNextPageClick}
              />
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default InstrumentListPage;
