import { combineReducers, configureStore } from '@reduxjs/toolkit';
import bloggApi from './api/bloggApi';
import userReducer from './slices/userSlice';

const store = configureStore({
  reducer: combineReducers({
    [bloggApi.reducerPath]: bloggApi.reducer,
    user: userReducer,
  }),
  middleware: (middleware) => middleware().concat(bloggApi.middleware),
  devTools: true,
});

export default store;
