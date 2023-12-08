import React from 'react';
import { useParams } from 'react-router-dom';
import Navbar from './components/Navbar';
import Blog from './components/blogs/Blog';
import CommentBox from './components/comments/CommentBox';
import Spinner from './components/Spinner';
import { useGetBlogQuery } from '../store/slices/blogSlice';

function BlogView() {
  const { id } = useParams();
  const { data, isLoading } = useGetBlogQuery(id);

  return (
    <>
      <Navbar />
      <div className="container">
        <div className="col-md-12 col-lg-10 col-xl-8">
          <div>
            <div className="link-secondary my-3">
              <i className="bi bi-arrow-left pe-1" />
              Go Back
            </div>
          </div>

          {isLoading ? (<Blog data={data} />) : (<Spinner text="Loading" />)}

          <div className="pb-5">
            <CommentBox />
          </div>
        </div>
      </div>
    </>
  );
}

export default BlogView;
