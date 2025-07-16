import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import AppProviders from './app/providers' 
import { RouterProvider } from 'react-router-dom'
import { router } from './app/router'

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <AppProviders>
      <RouterProvider router={router} />
    </AppProviders>
  </StrictMode>,
)
