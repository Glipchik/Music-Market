import { createBrowserRouter } from "react-router-dom";
import MainLayout from "@/layouts/MainLayout";
import HomePage from "@/pages/Home/HomePage";
import InstrumentDetailsPage from "@/pages/InstrumentDetailsPage/InstrumentDetailsPage";
import InstrumentListPage from "@/pages/InstrumentListPage/InstrumentListPage";
import CreateInstrumentPage from "@/pages/CreateInstrumentPage/CreateInstrumentPage";
import { loader as mainLoader } from "./loader";
import { loader as homeLoader } from "@/pages/Home/loader";
import { loader as instrumentDetailsLoader } from "@/pages/InstrumentDetailsPage/loader";
import { loader as instrumentListLoader } from "@/pages/InstrumentListPage/loader";
import { loader as createInstrumentLoader } from "@/pages/CreateInstrumentPage/loader";
import { loader as myListingsLoader } from "@/pages/MyListingsPage/loader";
import { loader as editInstrumentLoader } from "@/pages/EditInstrumentPage/loader";
import { deleteInstrumentAction } from "@/pages/MyListingsPage/action";
import MyListingsPage from "@/pages/MyListingsPage/MyListingsPage";
import RouteErrorHandler from "@/layouts/RouteErrorHandler";
import EditInstrumentPage from "@/pages/EditInstrumentPage/EditInstrumentPage";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <MainLayout />,
    loader: mainLoader,
    errorElement: <RouteErrorHandler />,
    children: [
      {
        errorElement: <RouteErrorHandler />,
        children: [
          {
            index: true,
            element: <HomePage />,
            loader: homeLoader,
          },
          {
            path: "instruments/:id",
            element: <InstrumentDetailsPage />,
            loader: instrumentDetailsLoader,
          },
          {
            path: "instruments/type/:typeId",
            element: <InstrumentListPage />,
            loader: instrumentListLoader,
          },
          {
            path: "instruments/create",
            element: <CreateInstrumentPage />,
            loader: createInstrumentLoader,
          },
          {
            path: "my-listings",
            element: <MyListingsPage />,
            loader: myListingsLoader,
          },
          {
            path: "my-listings/:id/edit",
            element: <EditInstrumentPage />,
            loader: editInstrumentLoader,
          },
          {
            path: "my-listings/:id/delete",
            action: deleteInstrumentAction,
          },
        ],
      },
    ],
  },
]);
