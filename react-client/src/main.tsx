import "@/app/app.css";
import { createRoot } from "react-dom/client";
import App from "./app/App";
import "./i18n";

createRoot(document.getElementById("root")!).render(<App />);
