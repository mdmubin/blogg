/* eslint-disable jsx-a11y/no-static-element-interactions */
/* eslint-disable jsx-a11y/click-events-have-key-events */

import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Link, useLocation } from 'react-router-dom';
import Types from 'prop-types';
import Logo from './Logo';
import { unsetAuthToken } from '../../redux/slices/userSlice';

function Navbar({ activeTab }) {
  const dispatch = useDispatch();
  const location = useLocation();

  const { loggedIn } = useSelector((state) => state.user);

  // needs double json.parse()... yes, I know it's weird
  const authInfo = loggedIn ? JSON.parse(JSON.parse(localStorage.getItem('authInfo'))) : '';
  const logoutHandler = () => {
    dispatch(unsetAuthToken());
  };

  return (
    <nav className="navbar navbar-expand-md border-bottom bg-light" data-bs-theme="light">
      <div className="container">
        <Link className="navbar-brand" to="/">
          <Logo w={48} h={48} />
          Blogg
        </Link>
        <button
          aria-controls="navbarToggleContent"
          aria-expanded="false"
          aria-label="Toggle navigation"
          className="navbar-toggler"
          data-bs-toggle="collapse"
          data-bs-target="#navbarToggleContent"
          type="button"
        >
          <span className="navbar-toggler-icon" />
        </button>
        <div className="collapse navbar-collapse" id="navbarToggleContent">
          <ul className="navbar-nav ms-auto px-2">
            <li className="nav-item">
              <Link className={`nav-link ${activeTab === 'HOME' ? 'active' : ''}`} to="/">
                <i className="bi bi-house pe-1" />
                Home
              </Link>
            </li>
            <li className="nav-item dropdown">
              <div
                className="nav-link dropdown-toggle"
                data-bs-toggle="dropdown"
                aria-expanded="false"
                role="button"
              >
                <i className="bi bi-person pe-1" />
                {loggedIn ? authInfo.username : ''}
              </div>
              <ul className="dropdown-menu">
                {loggedIn
                  ? (
                    <>
                      <li>
                        <Link className="dropdown-item" to="/settings">
                          <i className="bi bi-gear pe-2" />
                          Profile
                        </Link>
                      </li>
                      <li>
                        <Link className="dropdown-item not-selectable" to={location.pathname} onClick={logoutHandler}>
                          <i className="bi bi-box-arrow-right pe-2" />
                          Logout
                        </Link>
                      </li>
                    </>
                  )
                  : (
                    <li>
                      <Link className="dropdown-item" to={`/login?redirect=${location.pathname}`}>
                        <i className="bi bi-box-arrow-in-right pe-2" />
                        Login
                      </Link>
                    </li>
                  )}

                <li><hr className="dropdown-divider" /></li>

                <li>
                  <Link className="dropdown-item" href="https://github.com/mdmubin/blogg">
                    <i className="bi bi-github pe-2" />
                    Github
                  </Link>
                </li>
              </ul>
            </li>
          </ul>
        </div>

      </div>
    </nav>
  );
}

Navbar.propTypes = {
  activeTab: Types.string,
};

Navbar.defaultProps = {
  activeTab: '',
};

export default Navbar;
