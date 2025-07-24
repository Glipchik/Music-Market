/// <reference types="vite/client" />

interface ImportMetaEnv {
  readonly VITE_API_BASE_URL: string;
  readonly VITE_IDENTITY_PROVIDER_BASE_URL: string
  readonly VITE_REACT_APP_URL: string;
}

interface ImportMeta {
  readonly env: ImportMetaEnv;
}
