interface PaginationControlsProps {
  currentPage: number;
  totalPages: number;
  totalCount: number;
  pageSize: number;
  onPreviousPageClick: () => void;
  onNextPageClick: () => void;
}

const PaginationControls = ({
  currentPage,
  totalPages,
  totalCount,
  pageSize,
  onPreviousPageClick,
  onNextPageClick,
}: PaginationControlsProps) => {
  const itemsCount: number = Math.min(currentPage * pageSize, totalCount);

  return (
    <div className="flex flex-col items-center mt-12">
      <div className="flex justify-center items-center space-x-4 mb-4">
        <button
          onClick={onPreviousPageClick}
          disabled={currentPage === 1}
          className="btn-base btn-pagination"
        >
          Previous
        </button>
        <span className="text-xl font-medium text-gray-700">
          {currentPage} / {totalPages}
        </span>
        <button
          onClick={onNextPageClick}
          disabled={currentPage === totalPages}
          className="btn-base btn-pagination"
        >
          Next
        </button>
      </div>
      {totalCount > 0 && (
        <span className="text-base text-gray-600">
          {itemsCount} of {totalCount} items displayed
        </span>
      )}
    </div>
  );
};

export default PaginationControls;
