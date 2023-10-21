import React from 'react';
import Types from 'prop-types';
import Logo from './Logo';

function Navbar({ activeTab }) {
  return (
    <nav className="navbar navbar-expand-md border-bottom bg-light" data-bs-theme="light">
      <div className="container">
        <a className="navbar-brand" href="/">
          <Logo w={48} h={48} />
          Blogg
        </a>
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
              <a className={`nav-link ${activeTab === 'HOME' ? 'active' : ''}`} href="/">
                Home
              </a>
            </li>
            <li className="nav-item dropdown">
              <div
                className="nav-link dropdown-toggle"
                data-bs-toggle="dropdown"
                aria-expanded="false"
                role="button"
              >
                Profile
              </div>
              <ul className="dropdown-menu">
                <li><a className="dropdown-item" href="/">Login</a></li>
                <li><a className="dropdown-item" href="/">Report</a></li>
                <li><hr className="dropdown-divider" /></li>
                <li><a className="dropdown-item" href="/">Github</a></li>
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
