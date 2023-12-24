import React from 'react';
import Navbar from './components/Navbar';

function BlogListView() {
  return (
    <>
      <Navbar />

      <div className="container">
        <div className="row">
          <h3>Popular Blogs</h3>
        </div>

        <div>
          <h3>Recent Blogs</h3>
        </div>
      </div>
    </>
  );
}

export default BlogListView;
