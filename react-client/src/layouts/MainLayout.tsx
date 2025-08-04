import { Outlet, useLocation } from "react-router-dom";
import Header from "@/widgets/Header/Header";
import Footer from "@/widgets/Footer/Footer";
import { useEffect } from "react";

const MainLayout = () => {
  const location = useLocation();

  useEffect(() => {
    window.scrollTo(0, 0);
  }, [location.pathname]);

  return (
    <div className="min-h-screen bg-gray-50 flex flex-col">
      <Header />
      <main className="flex-grow p-6">
        <Outlet />
      </main>
      <Footer />
    </div>
  );
};

export default MainLayout;
