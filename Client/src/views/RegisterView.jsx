import React from 'react';
import Navbar from './components/Navbar';
import RegisterForm from './components/RegisterForm';

function RegisterView() {
  return (
    <>
      <Navbar />
      <div className="container">
        <div className="row my-5 justify-content-center">
          <div className="col col-lg-6">
            <RegisterForm />
          </div>
        </div>
      </div>
    </>
  );
}

export default RegisterView;
