import React from 'react';
import 'bootstrap/dist/js/bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap-icons/font/bootstrap-icons.min.css';
import { RouterProvider, createBrowserRouter } from 'react-router-dom';

import * as Views from './views';

const router = createBrowserRouter([
  { path: '/login', element: <Views.LoginView /> },
  { path: '/register', element: <Views.RegisterView /> },
]);

function App() {
  return (
    <RouterProvider router={router} />
  );
}

export default App;
