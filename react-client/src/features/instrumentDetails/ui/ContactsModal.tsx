import type { UserContactsModel } from "../model/types";

interface ContactsModalProps {
  contacts?: UserContactsModel | null;
  isOpen: boolean;
  onClose: () => void;
  onLoginClick: () => void;
  isLoading: boolean;
}

const ContactsModal = ({
  contacts,
  isOpen,
  onClose,
  onLoginClick,
  isLoading,
}: ContactsModalProps) => {
  if (!isOpen) {
    return null;
  }

  return (
    <div className="fixed inset-0 bg-black/70 flex items-center justify-center z-[100] p-4">
      <div className="bg-white rounded-lg shadow-2xl p-6 sm:p-8 relative max-w-sm w-full md:max-w-md lg:max-w-lg text-center">
        <button
          onClick={onClose}
          className="absolute top-3 right-3 text-gray-500 hover:text-gray-800 text-3xl font-bold leading-none cursor-pointer"
          aria-label="Close modal"
        >
          &times;
        </button>
        {isLoading ? (
          <div className="flex justify-center items-center py-8">
            <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-gray-900"></div>
            <p className="ml-4 text-xl text-gray-700">Loading contacts...</p>
          </div>
        ) : !contacts ? (
          <>
            <h2 className="text-2xl font-bold text-red-600 mb-4">
              Authorization Required
            </h2>
            <p className="text-gray-700 text-lg mb-6">
              Please log in to view seller contacts.
            </p>
            <button
              onClick={(e) => {
                e.stopPropagation();
                onLoginClick();
              }}
              className="btn-base bg-blue-600 text-white text-lg hover:bg-blue-700"
            >
              Log In
            </button>
          </>
        ) : (
          <>
            <h2 className="text-2xl font-bold text-gray-900 mb-4">
              Seller Contacts
            </h2>
            <>
              <p className="text-gray-800 text-lg mb-2">
                <span className="font-semibold">Name:</span> {contacts.name}
              </p>
              <p className="text-gray-800 text-lg mb-4">
                <span className="font-semibold">Email:</span> {contacts.email}
              </p>
            </>
          </>
        )}
      </div>
    </div>
  );
};

export default ContactsModal;
