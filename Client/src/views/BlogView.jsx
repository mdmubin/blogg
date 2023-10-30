import React from 'react';
import Navbar from './components/Navbar';
import Blog from './components/blogs/Blog';
import CommentBox from './components/comments/CommentBox';

function BlogView() {
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

          <Blog />

          <div className="pb-5">
            <CommentBox />
          </div>
        </div>
      </div>
    </>
  );
}

export default BlogView;
