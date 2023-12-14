import api from './bloggApi';

const commentEndpoints = api.injectEndpoints({
  endpoints: (builder) => ({

    getComments: builder.query({
      query: (blogId) => `comments/blog/${blogId}`,
    }),

    getCommentById: builder.query({
      query: (commentId) => `comments/${commentId}`,
    }),

    addComment: builder.mutation({
      query: ({ blogId, data }) => ({
        url: `comments/blog/${blogId}`,
        method: 'POST',
        body: data,
      }),
    }),

    updateComment: builder.mutation({
      query: ({ commentId, data }) => ({
        url: `comments/${commentId}`,
        method: 'PUT',
        body: data,
      }),
    }),

    deleteComment: builder.mutation({
      query: (commentId) => ({
        url: `comments/${commentId}`,
        method: 'DELETE',
      }),
    }),

    getReplies: builder.query({
      query: (commentId) => `replies/${commentId}`,
    }),

    addReply: builder.mutation({
      query: ({ commentId, data }) => ({
        url: `replies/${commentId}`,
        method: 'POST',
        body: data,
      }),
    }),

    updateReply: builder.mutation({
      query: ({ replyId, data }) => ({
        url: `replies/${replyId}`,
        method: 'PUT',
        body: data,
      }),
    }),

    deleteReply: builder.mutation({
      query: (replyId) => ({
        url: `replies/${replyId}`,
        method: 'DELETE',
      }),
    }),

  }),
});

export const {
  useGetCommentsQuery,
  useGetCommentByIdQuery,
  useAddCommentMutation,
  useUpdateCommentMutation,
  useDeleteCommentMutation,
  useGetRepliesQuery,
  useAddReplyMutation,
  useUpdateReplyMutation,
  useDeleteReplyMutation,
} = commentEndpoints;
