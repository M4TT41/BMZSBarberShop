import React from 'react';
import Hero from  "./Hero"
import About from "./About"
import Price from "./Price"
import AboutUs from './AboutUs';
import Opening from './Opening';
import Rating from './Rating';

function Main() {
  return (
    <div className="mainContainer">
      <Hero/>
      <About/>
      <Price/>
      <Rating/>
      <Opening/>
      <AboutUs/>
    </div>
  );
}

export default Main;