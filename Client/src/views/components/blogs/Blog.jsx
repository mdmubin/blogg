import React from 'react';
import Types from 'prop-types';

function Blog({ data }) {
  return (
    <div className="row">
      <h3>{data.heading}</h3>
      <p className="blog-post-meta">{data.postedDateTime}</p>

      {data.content.split('\n').map((p) => <p>{p}</p>)}

      <div className="py-4">
        <p className="h6">Tags:</p>
        {data.tags.map((t, i) => <div className="btn btn-sm btn-primary ms-2 my-1" key={`tag${i + 1}`}>{t}</div>)}
      </div>
    </div>
  );
}

Blog.propTypes = {
  data: Types.shape({
    postedDateTime: Types.string,
    heading: Types.string,
    pageTitle: Types.string,
    coverImgUrl: Types.string,
    content: Types.string,
    tags: Types.arrayOf(Types.string),
  }),
};

Blog.defaultProps = {
  data: {
    postedDateTime: new Date().toLocaleString(),
    heading: '',
    pageTitle: '',
    coverImgUrl: '',
    content: '',
    tags: [],
  },
};

export default Blog;
