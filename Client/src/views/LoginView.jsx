import React from 'react';
import LoginForm from './components/LoginForm';
import Navbar from './components/Navbar';

function LoginView() {
  return (
    <>
      <Navbar />
      <div className="container">
        <div className="row my-5 justify-content-center">
          <div className="col col-lg-6">
            <LoginForm />
          </div>
        </div>
      </div>
    </>
  );
}

export default LoginView;
