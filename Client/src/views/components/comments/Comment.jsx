import React from 'react';
import Types from 'prop-types';
import Reply from './Reply';
import InteractButtons from './InteractButtons';

function Comment({ data }) {
  return (
    <div className="d-flex flex-start">
      <img className="rounded-circle me-3" src="https://mdbcdn.b-cdn.net/img/Photos/Avatars/img%20(10).webp" alt="avatar" width="65" height="65" />
      <div className="flex-grow-1 flex-shrink-1">
        <div>
          <div className="d-flex justify-content-between align-items-center">
            <p className="mb-1">
              {data.username}
              <span className="small">{data.timePosted}</span>
            </p>
          </div>

          <p className="small mb-0">{data.content}</p>

          <InteractButtons />
        </div>

        {data.replies.map((r, i) => (<Reply data={r} key={`reply${i + 1}`} />))}
      </div>
    </div>
  );
}

Comment.propTypes = {
  data: Types.shape({
    username: Types.string,
    timePosted: Types.string,
    content: Types.string,
    // eslint-disable-next-line react/forbid-prop-types
    replies: Types.array,
  }),
};

Comment.defaultProps = {
  data: {},
};

export default Comment;
