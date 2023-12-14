import React from 'react';
import 'bootstrap/dist/js/bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap-icons/font/bootstrap-icons.min.css';
import { Provider } from 'react-redux';
import { RouterProvider, createBrowserRouter } from 'react-router-dom';

import store from './redux/store';
import * as Views from './views';

const router = createBrowserRouter([
  { path: '/settings', element: <Views.ProfileView /> },
  { path: '/login', element: <Views.LoginView /> },
  { path: '/register', element: <Views.RegisterView /> },
  { path: '/blog/:id', element: <Views.BlogView /> },
]);

function App() {
  return (
    <Provider store={store}>
      <RouterProvider router={router} />
    </Provider>
  );
}

export default App;
