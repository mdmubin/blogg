import apiSlice from './apiSlice';

const blogSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({

    getBlogList: builder.query({
      query: () => 'blog',
    }),

    getBlog: builder.query({
      query: (blogId) => `blog/${blogId}`,
    }),

    postBlog: builder.mutation({
      query: (data) => ({
        url: 'blog',
        method: 'POST',
        body: data,
      }),
    }),

    updateBlog: builder.mutation({
      query: ({ blogId, data }) => ({
        url: `blog/${blogId}`,
        method: 'PUT',
        body: data,
      }),
    }),

    deleteBlog: builder.mutation({
      query: (blogId) => ({
        url: `blog/${blogId}`,
        method: 'DELETE',
      }),
    }),

  }),
});

export const {
  useGetBlogListQuery,
  useGetBlogQuery,
  usePostBlogMutation,
  useUpdateBlogMutation,
  useDeleteBlogMutation,
} = blogSlice;
