import React from 'react';
import Types from 'prop-types';
import InteractButtons from './InteractButtons';

function Reply({ data }) {
  return (
    <div className="d-flex flex-start mt-4">
      <a className="me-3" href="/#">
        {/* TODO: fix avatar image */}
        <img className="rounded-circle" src="https://mdbcdn.b-cdn.net/img/Photos/Avatars/img%20(11).webp" alt="avatar" width={65} height={65} />
      </a>
      <div className="flex-grow-1 flex-shrink-1">
        <div>
          <div className="d-flex justify-content-between align-items-center">
            <p className="mb-1">
              {data.username}
              <span className="small">{data.timePosted}</span>
            </p>
          </div>
          <p className="small mb-0">{data.content}</p>
        </div>

        <InteractButtons />
      </div>
    </div>
  );
}

Reply.propTypes = {
  data: Types.shape({
    username: Types.string,
    timePosted: Types.string,
    content: Types.string,
  }),
};

Reply.defaultProps = {
  data: {},
};

export default Reply;
