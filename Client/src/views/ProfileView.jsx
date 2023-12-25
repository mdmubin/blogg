import React, { useEffect } from 'react';
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { useProfileQuery } from '../redux/api/userEndpoints';
import Navbar from './components/Navbar';

function ProfileView() {
  const navigate = useNavigate();

  const { loggedIn } = useSelector((state) => state.user);

  const authInfo = JSON.parse(JSON.parse(localStorage.getItem('authInfo')));
  const {
    isLoading, data, error, isError,
  } = useProfileQuery(authInfo?.username);
  const userData = isLoading || isError ? {} : JSON.parse(data);

  useEffect(() => {
    if (!loggedIn) {
      navigate('/login');
    }
  }, [loggedIn]);

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
            <img src={userData.profilePictureUrl ?? 'https://avatars.githubusercontent.com/u/72853968?v=4'} className="img-thumbnail" alt="pfp" width={150} height={150} />
          </div>

          <div className="input-group px-4 mb-3">
            <span className="input-group-text" style={labelStyle}>User ID</span>
            <input type="text" aria-label="Username" className="form-control" value={userData.id ?? ''} disabled />
          </div>

          <form className="pb-3">

            <div className="input-group px-4 mb-3">
              <span className="input-group-text" style={labelStyle}>Username</span>
              <input
                type="text"
                aria-label="Username"
                className="form-control"
                value={userData.username ?? ''}
                disabled
              />
            </div>

            <div className="input-group px-4 mb-3">
              <span className="input-group-text" style={labelStyle}>Email</span>
              <input
                type="text"
                aria-label="Email"
                className="form-control"
                value={userData.email ?? ''}
                disabled
              />
            </div>

            <div className="input-group px-4 mb-3">
              <span className="input-group-text" style={labelStyle}>Profile Picture</span>
              <input
                type="text"
                aria-label="First name"
                className="form-control"
                value={userData.profilePictureUrl ?? 'https://avatars.githubusercontent.com/u/72853968?v=4'}
                disabled
              />
            </div>

            {/* <div className="container text-center pb-3">
              <button type="submit" className="my-3 btn btn-primary">Save Changes</button>
            </div> */}

          </form>
        </div>
      </div>

      {isError
        && (
          <div className="d-flex justify-content-center pt-3">
            <div className="alert alert-danger" role="alert">
              {`Error ${error.status}: Failed to retrieve data. Try refreshing / logging back in.`}
            </div>
          </div>
        )}

    </>
  );
}

export default ProfileView;
