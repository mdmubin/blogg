import React from 'react';
import Types from 'prop-types';

function InteractButtons({ likeCount, dislikeCount }) {
  return (
    <div className="d-flex align-items-center py-1">

      {/* likes count */}
      <div>
        <i className="bi bi-hand-thumbs-up pe-1" />
        {likeCount}
      </div>

      <div className="mx-2">
        <i className="bi bi-hand-thumbs-down pe-1" />
        {dislikeCount}
      </div>
      <span className="btn btn-sm btn-outline-secondary ">
        <i className="bi bi-reply" />
        reply
      </span>
    </div>
  );
}

InteractButtons.propTypes = {
  likeCount: Types.number,
  dislikeCount: Types.number,
};

InteractButtons.defaultProps = {
  likeCount: 0,
  dislikeCount: 0,
};

export default InteractButtons;
