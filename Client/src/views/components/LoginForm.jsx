import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { useLoginMutation } from '../../redux/api/userEndpoints';
import { setAuthToken } from '../../redux/slices/userSlice';
import Logo from './Logo';

function LoginView() {
  const dispatch = useDispatch();
  const location = useLocation();
  const navigate = useNavigate();

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState({ isError: false, status: 0, data: '' });

  const { loggedIn } = useSelector((state) => state.user);
  const [login, { isLoading }] = useLoginMutation();

  const searchParams = new URLSearchParams(location.search);
  const redirect = searchParams.get('redirect') || '/';

  useEffect(() => {
    if (loggedIn) {
      navigate(redirect);
    }
  }, [navigate, redirect, loggedIn]);

  const loginHandler = (e) => {
    e.preventDefault();
    Promise.resolve(login({ email, password }).unwrap())
      .then((res) => dispatch(setAuthToken(res)))
      .then(() => navigate(redirect))
      .catch((err) => setError({ isError: true, ...err }));
  };

  return (
    <>
      <div className="form-group-sm card p-3">
        <form onSubmit={loginHandler}>
          <div className="text-center mb-3">
            <Logo />
            <h3>Login</h3>
          </div>

          <div className="mb-3 px-4">
            <label htmlFor="EmailInputField" className="form-label ps-1">Email address</label>
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
            <label htmlFor="PasswordInputField" className="form-label ps-1">
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

          <div className="container text-center pb-4">
            <button type="submit" className="my-3 btn btn-primary" disabled={isLoading}>
              {isLoading && <span className="spinner-border spinner-border-sm me-2" />}
              Login
            </button>
            <div className="row">
              <Link to="/register">Don&apos;t have an account?</Link>
            </div>
          </div>

        </form>
      </div>

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

export default LoginView;
