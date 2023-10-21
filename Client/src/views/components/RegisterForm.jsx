import React, { useEffect } from 'react';
import { Link } from 'react-router-dom';
import Logo from './Logo';

function RegisterForm() {
  useEffect(() => {
  }, []);

  const registerHandler = (e) => {
    e.preventDefault();
  };
  return (
    <div className="form-group-sm card p-3">
      <form onSubmit={registerHandler}>
        <div className="text-center mb-3">
          <Logo />
          <h3>Register</h3>
        </div>

        <div className="mb-3 px-4">
          <label htmlFor="UsernameInputField" className="form-label">Username</label>
          <input
            type="text"
            id="UsernameInputField"
            className="form-control"
            aria-describedby="emailHelp"
            placeholder="Username"
          // onChange={() => { }}
          />
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
          />
        </div>

        <div className="mb-3 px-4">
          <label htmlFor="ConfirmPasswordInputField" className="form-label">
            Confirm Password
          </label>
          <input
            type="password"
            className="form-control"
            id="ConfirmPasswordInputField"
            placeholder="Password"
          />
        </div>

        <div className="mb-3 px-4">
          <input type="checkbox" className="form-check-input" id="TermsConditionsAgree" />
          <label className="form-check-label px-2" htmlFor="TermsConditionsAgree">* I agree to the terms &amp; conditions.</label>
        </div>

        <div className="container text-center pb-4">
          <button type="submit" className="my-3 btn btn-primary" disabled={false}>
            <span className="spinner-border spinner-border-sm me-2" />
            Register
          </button>
          <div className="row">
            <Link to="/login">Already have an account?</Link>
          </div>
        </div>

      </form>
    </div>
  );
}

export default RegisterForm;
