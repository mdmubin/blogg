import React from 'react';
import Types from 'prop-types';
import Navbar from './components/Navbar';

function ProfileView({ userData }) {
  const labelStyle = { width: 124 };

  return (
    <>
      <Navbar />
      <div className="container py-3">
        <div className="form-group-sm card p-3">

          <div className="mb-3 px-4 pt-4">
            <h3 className="pb-3">Profile</h3>
          </div>

          <div className="px-4 pb-3">
            <img src={userData.profilePictureUrl} className="img-thumbnail" alt="pfp" width={150} height={150} />
          </div>

          <div className="input-group px-4 mb-3">
            <span className="input-group-text" style={labelStyle}>User ID</span>
            <input type="text" disabled aria-label="Username" className="form-control" value={userData.id} />
          </div>

          <form onSubmit={() => { }}>

            <div className="input-group px-4 mb-3">
              <span className="input-group-text" style={labelStyle}>Username</span>
              <input type="text" aria-label="Username" className="form-control" value={userData.username} />
            </div>

            <div className="input-group px-4 mb-3">
              <span className="input-group-text" style={labelStyle}>Email</span>
              <input type="text" aria-label="Email" className="form-control" value={userData.email} />
            </div>

            <div className="input-group px-4 mb-3">
              <span className="input-group-text" style={labelStyle}>Profile Picture</span>
              <input type="text" aria-label="First name" className="form-control" value={userData.profilePictureUrl} />
            </div>

            <div className="container text-center pb-3">
              <button type="submit" className="my-3 btn btn-primary" disabled={false}>Save Changes</button>
            </div>

          </form>
        </div>
      </div>
    </>
  );
}

ProfileView.propTypes = {
  userData: Types.shape({
    id: Types.string,
    username: Types.string,
    email: Types.string,
    profilePictureUrl: Types.string,
  }),
};

ProfileView.defaultProps = {
  userData: {
    id: '00000000-0000-0000-0000-000000000000',
    username: '',
    email: '',
    profilePictureUrl: 'https://avatars.githubusercontent.com/u/72853968?v=4',
  },
};

export default ProfileView;
