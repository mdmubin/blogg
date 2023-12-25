import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

const authInfo = JSON.parse(JSON.parse(localStorage.getItem('authInfo')));

const bloggApi = createApi({
  reducerPath: 'bloggApi',
  baseQuery: fetchBaseQuery({
    baseUrl: 'http://localhost:5268/api/',
    headers: {
      Authorization: `Bearer ${authInfo?.token ?? ''}`,
    },
    responseHandler: 'text',
  }),
  endpoints: () => ({}),
});

export default bloggApi;
