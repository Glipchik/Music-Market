import { Link } from "react-router-dom";
import { useAuthStore } from "@/features/auth/store";
import { PlusIcon } from "@heroicons/react/16/solid";

const Header = () => {
  const user = useAuthStore((state) => state.user);
  const login = useAuthStore((state) => state.login);
  const logout = useAuthStore((state) => state.logout);
  const signUp = useAuthStore((state) => state.signUp);

  return (
    <header className="w-full bg-white border-b border-gray-200 shadow-sm py-4 rounded-b-lg">
      <div className="container mx-auto px-6 flex justify-between items-center">
        <Link
          to="/"
          className="text-3xl font-extrabold text-indigo-700 hover:text-indigo-800 transition-colors duration-200 flex items-center gap-2"
        >
          <span role="img" aria-label="musical notes">
            🎵
          </span>{" "}
          Music Market
        </Link>
        <div className="flex items-center gap-4">
          {user ? (
            <>
              <Link
                to="/instruments/create"
                className="btn-base btn-header inline-flex items-center gap-2 bg-gradient-to-r from-purple-500 to-indigo-600 shadow-lg
             hover:from-purple-600 hover:to-indigo-700 focus:ring-purple-400 focus:ring-opacity-75 duration-300"
              >
                <PlusIcon className="h-5 w-5" />
                List Instrument
              </Link>
              <Link
                to="/my-listings"
                className="px-4 py-2 text-indigo-700 font-semibold rounded-full hover:bg-indigo-50 transition-colors duration-200"
              >
                My Listings
              </Link>
              <span className="text-gray-800 text-lg font-medium">
                Hi, {user.name || user.sub}!
              </span>
              <button
                onClick={logout}
                className="btn-base btn-header bg-red-600 hover:bg-red-700 focus:ring-red-500"
              >
                Sign Out
              </button>
            </>
          ) : (
            <>
              <button
                onClick={login}
                className="btn-base btn-header bg-indigo-600 hover:bg-indigo-700 focus:ring-indigo-500"
              >
                Sign In
              </button>
              <button
                onClick={signUp}
                className="btn-base btn-header bg-green-600 hover:bg-green-700 focus:ring-green-500"
              >
                Sign Up
              </button>
            </>
          )}
        </div>
      </div>
    </header>
  );
};

export default Header;
