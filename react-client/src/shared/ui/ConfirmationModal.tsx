interface ConfirmationModalProps {
  isOpen: boolean;
  onConfirm: () => void;
  onCancel: () => void;
  message: string;
  title?: string;
}

const ConfirmationModal = ({
  isOpen,
  onConfirm,
  onCancel,
  message,
  title = "Confirm Action",
}: ConfirmationModalProps) => {
  if (!isOpen) {
    return null;
  }

  return (
    <div className="fixed inset-0 bg-black/60 flex items-center justify-center z-[100] p-4">
      <div className="bg-white rounded-lg shadow-xl p-6 sm:p-8 max-w-sm w-full text-center transform scale-100 transition-transform duration-300 ease-out">
        <h3 className="text-2xl font-bold text-gray-900 mb-4">{title}</h3>
        <p className="text-gray-700 text-lg mb-6">{message}</p>
        <div className="flex justify-center gap-4">
          <button
            onClick={onCancel}
            className="btn-base flex-1 bg-gray-200 text-gray-800 hover:bg-gray-300"
          >
            Cancel
          </button>
          <button
            onClick={onConfirm}
            className="btn-base flex-1 bg-rose-600 text-white hover:bg-rose-700"
          >
            Confirm
          </button>
        </div>
      </div>
    </div>
  );
};

export default ConfirmationModal;
