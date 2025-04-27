import React from 'react'
import './Hero.css'
import hero1 from "../assets/3bb8c26018d39a710ccd0d62b904b784.jpg";
import hero2 from "../assets/a6f8671e693389a874a22683824ec4b8.jpg";
import { Link } from 'react-router-dom';

function Hero(){
    return(
        <div className='landing'>
      <section className="hero">
        <div className="heroContent">
          <h1 className='company'><span>BMZS</span><span  className='stroke'>BARBERSHOP</span></h1>
          <p className='textP'>Friss hajvágás, éles kontúrok – a stílus nálunk kezdődik. 💈✨</p>
          <Link to="/city" className="ctaButton">Foglalj időpontot!</Link>
        </div>
      </section>
      <section className="imageGrid">
        <div className="imageC">
          <img src={hero1} alt="Barber service" className="gridImg"/>  
          
        </div>
        <div className="imageC">
          <img src={hero2} alt="Barber service" className="gridImg"/>  
   
        </div>
      </section>
      </div>
    )
    
}
export default Hero