import React from 'react';
import Comment from './Comment';

function CommentBox() {
  const commentData = [];

  return (
    <div className="col">
      <div className="card">
        <div className="card-body p-4">

          <div className="border-bottom  mb-4 pb-2">
            <h4 className="text-center">Comments</h4>
          </div>

          <div className="row">
            <div className="col">
              {commentData.length
                ? commentData.map((c, i) => (<Comment data={c} key={`comment${i + 1}`} />))
                : (
                  <div className="container text-center">
                    <button className="btn btn-primary align" type="button">
                      Load Comments
                      <i className="bi bi-chat-dots ps-2" />
                    </button>
                  </div>
                )}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default CommentBox;
