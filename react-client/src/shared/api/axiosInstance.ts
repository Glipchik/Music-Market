import axios from "axios";
import { API_BASE_URL } from "../config/constants";

const api = axios.create({
  baseURL: API_BASE_URL,
  withCredentials: true,
  headers: {
    "X-CSRF": "1",
  },
});

export default api;
