import api from './bloggApi';

const userEndpoints = api.injectEndpoints({
  endpoints: (builder) => ({

    profile: builder.query({
      query: (username) => ({
        url: `users/${username}`,
      }),
    }),

    login: builder.mutation({
      query: (loginInfo) => ({
        url: 'users/login',
        method: 'POST',
        body: loginInfo,
      }),
    }),

    register: builder.mutation({
      query: (registerInfo) => ({
        url: 'users',
        method: 'POST',
        body: registerInfo,
      }),
    }),

  }),
});

export const {
  useProfileQuery,
  useLoginMutation,
  useRegisterMutation,
} = userEndpoints;
