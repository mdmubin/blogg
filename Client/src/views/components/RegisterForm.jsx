import React, { useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import { Link, useNavigate } from 'react-router-dom';
import { useRegisterMutation } from '../../redux/api/userEndpoints';
import Logo from './Logo';

function RegisterForm() {
  const navigate = useNavigate();

  const { loggedIn } = useSelector((state) => state.user);

  const [email, setEmail] = useState('');
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');

  const [error, setError] = useState({ isError: false, status: 0, data: '' });

  const [termsAgreed, setTermsAgreed] = useState(false);
  const [registerClicked, setRegisterClicked] = useState(false);

  const [register, { isLoading }] = useRegisterMutation();

  useEffect(() => {
    if (loggedIn) {
      navigate('/');
    }
  }, [loggedIn, navigate]);

  const registerHandler = (e) => {
    e.preventDefault();
    setRegisterClicked(true);

    if (termsAgreed && registerClicked) {
      Promise.resolve(register({
        username, email, password, confirmPassword,
      }).unwrap())
        .then(() => navigate('/'))
        .catch((err) => setError({ isError: true, ...err }));
    }
  };

  return (
    <>
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
              onChange={(e) => setUsername(e.target.value)}
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
              onChange={(e) => setEmail(e.target.value)}
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
              onChange={(e) => setPassword(e.target.value)}
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
              onChange={(e) => setConfirmPassword(e.target.value)}
            />
          </div>

          <div className="mb-3 px-4">
            <input
              type="checkbox"
              className="form-check-input"
              id="TermsConditionsAgree"
              onChange={(e) => setTermsAgreed(e.target.checked)}
            />
            <label className="form-check-label px-2" htmlFor="TermsConditionsAgree">I agree to the terms &amp; conditions.</label>
          </div>

          <div className="container text-center pb-4">
            <button type="submit" className="my-3 btn btn-primary" disabled={isLoading}>
              {isLoading && <span className="spinner-border spinner-border-sm me-2" />}
              Register
            </button>
            <div className="row">
              <Link to="/login">Already have an account?</Link>
            </div>
          </div>

        </form>
      </div>

      {!termsAgreed && registerClicked
        && (
          <div className="d-flex justify-content-center pt-3">
            <div className="alert alert-danger" role="alert">
              You have not agreed to our terms and conditions
            </div>
          </div>
        )}

      {error.isError
        && (
          <div className="d-flex justify-content-center pt-3">
            <div className="alert alert-danger" role="alert">
              {`Error ${error.status}: ${error.data}`}
            </div>
          </div>
        )}
    </>
  );
}

export default RegisterForm;
