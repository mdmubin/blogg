import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

const apiSlice = createApi({
  reducerPath: 'bloggApi',
  baseQuery: fetchBaseQuery({ baseUrl: 'http://localhost:5268/api/' }),
  endpoints: () => ({}),
});

export default apiSlice;
