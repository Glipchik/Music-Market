import { RouterProvider } from "react-router-dom";
import { router } from "./router";
import "@/app/app.css";
import { ToastContainer } from "react-toastify";

const App = () => {
  return (
    <>
      <RouterProvider router={router} />
      <ToastContainer
        position="top-right"
        autoClose={3000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
      />
    </>
  );
};

export default App;
