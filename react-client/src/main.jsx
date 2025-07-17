import { createRoot } from 'react-dom/client'
import AppProviders from './app/providers' 
import { RouterProvider } from 'react-router-dom'
import { router } from './app/router'
import React from 'react';


createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <AppProviders>
      <RouterProvider router={router} />
    </AppProviders>
  </React.StrictMode>,
)
