import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

const Footer = () => {
  return (
    <footer className=" w-100 bg-light footer text-center mt-5 py-4">
      <div>FTI Clinic</div>
      <div className="container">
        <h4>Follow us</h4>
        <FontAwesomeIcon icon={'fab fa-facebook-square' as any} className="mx-4" size="xl" color="blue" />
        <FontAwesomeIcon icon={'fab fa-twitter' as any} className="mx-4" size="xl" color="lightblue" />
        <FontAwesomeIcon icon={'fab fa-instagram' as any} className="mx-4" size="xl" color="purple" />
        <FontAwesomeIcon icon={'fab fa-linkedin-in' as any} className="mx-4" size="xl" />
      </div>
    </footer>
  );
};

export default Footer;
