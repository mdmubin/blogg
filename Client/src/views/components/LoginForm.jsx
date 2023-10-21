import React, { useEffect } from 'react';
import { Link } from 'react-router-dom';
import Logo from './Logo';

function LoginView() {
  useEffect(() => {
  }, []);

  const loginHandler = (e) => {
    e.preventDefault();
  };

  return (
    <div className="form-group-sm card p-3">
      <form onSubmit={loginHandler}>
        <div className="text-center mb-3">
          <Logo />
          <h3>Login</h3>
        </div>

        <div className="mb-3 px-4">
          <label htmlFor="EmailInputField" className="form-label">Email address</label>
          <input
            type="email"
            id="EmailInputField"
            className="form-control"
            aria-describedby="emailHelp"
            placeholder="Email"
          // onChange={() => { }}
          />
        </div>

        <div className="mb-3 px-4">
          <label htmlFor="PasswordInputField" className="form-label">
            Password
          </label>
          <input
            type="password"
            className="form-control"
            id="PasswordInputField"
            placeholder="Password"
          // onChange={() => { }}
          />
        </div>

        <div className="container text-center pb-4">
          <button type="submit" className="my-3 btn btn-primary" disabled={false}>
            <span className="spinner-border spinner-border-sm me-2" />
            Login
          </button>
          <div className="row">
            <Link to="/register">Don&apos;t have an account?</Link>
          </div>
        </div>

      </form>
    </div>
  );
}

export default LoginView;
