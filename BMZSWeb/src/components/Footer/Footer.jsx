import React, { useState, useEffect } from 'react';
import "./Footer.css"
import { FaFacebook, FaInstagram, FaTwitter } from 'react-icons/fa';
import GoTop from './GoTop';

function Footer() {
 
  return (
    <div className='footer'>
      <GoTop />
        <footer>
        <p>DESIGNED BY FREEPIK.</p>
        <div className="socialIcons">
            <FaFacebook />
            <FaInstagram />
            <FaTwitter />
          </div>
          <p>2025 BMZS. All rights reserved.</p>
          
        </footer>
          </div>
  );
}

export default Footer;
