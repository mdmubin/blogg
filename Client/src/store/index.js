import { configureStore } from '@reduxjs/toolkit';
import apiSlice from './slices/apiSlice';

const store = configureStore({
  reducer: {
    [apiSlice.reducerPath]: apiSlice.reducer,
  },
  middleware: (middleware) => middleware().concat(apiSlice.middleware),
  devTools: true,
});

export default store;
