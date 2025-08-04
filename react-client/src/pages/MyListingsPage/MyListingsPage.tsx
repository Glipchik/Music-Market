import { useLoaderData, useSearchParams, Link } from "react-router-dom";
import MyListingCard from "@/features/myListings/ui/MyListingCard";
import PaginationControls from "@/shared/ui/PaginationControls";
import type { PaginatedModel } from "@/shared/types/pagination";
import type { UserInstrumentResponseModel } from "@/features/myListings/model/types";
import { useTranslation } from "react-i18next";

const MyListingsPage = () => {
  const { t } = useTranslation("myListingsPage");
  const { items, page, pageSize, totalCount, totalPages } =
    useLoaderData<PaginatedModel<UserInstrumentResponseModel>>();

  const [, setSearchParams] = useSearchParams();

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

  const noListingsFound = items.length === 0;

  return (
    <div className="container mx-auto px-4 py-8 max-w-7xl">
      <h1 className="mb-8 text-center">{t("pageTitle")}</h1>

      {noListingsFound ? (
        <div className="flex flex-col items-center justify-center py-16 bg-white rounded-lg shadow-md">
          <p className="text-xl text-gray-600 mb-4">{t("noListingsMessage")}</p>
          <Link
            to="/instruments/create"
            className="btn-base px-6 py-3 bg-indigo-600 text-white hover:bg-indigo-700"
          >
            {t("listFirstInstrument")}
          </Link>
        </div>
      ) : (
        <>
          <div className="flex flex-col gap-6">
            {items.map((instrument) => (
              <MyListingCard key={instrument.id} instrument={instrument} />
            ))}
          </div>

          {totalPages > 1 && (
            <PaginationControls
              currentPage={page}
              totalPages={totalPages}
              totalCount={totalCount}
              pageSize={pageSize}
              onPreviousPageClick={handlePreviousPageClick}
              onNextPageClick={handleNextPageClick}
            />
          )}
        </>
      )}
    </div>
  );
};

export default MyListingsPage;
