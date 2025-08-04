import { useRouteError, isRouteErrorResponse } from "react-router-dom";
import NotFoundErrorPage from "@/pages/NotFoundErrorPage/NotFoundErrorPage";
import ServiceUnavailableErrorPage from "@/pages/ServiceUnavailableErrorPage/ServiceUnavailableErrorPage";

const RouteErrorHandler = () => {
  const error = useRouteError();

  if (isRouteErrorResponse(error)) {
    if (error.status === 404) {
      return <NotFoundErrorPage />;
    }
  }

  return <ServiceUnavailableErrorPage />;
};

export default RouteErrorHandler;
