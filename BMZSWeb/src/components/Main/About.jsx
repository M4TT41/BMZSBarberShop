import React from "react";
import { FaCut, FaPaintBrush, FaUserTie, FaSpa } from "react-icons/fa";
import "./About.css";

function About() {
    return (
        <div className="about">
            <h1 className="services">SZOLGÁLTATÁSAINK</h1>
            <div className="cards">
                <div className="aboutCard">
                    <FaCut size={60} className="icon" />
                    <h2>Hajvágás</h2>
                    <p>Professionális férfi, női és gyermek hajvágás modern technikákkal.</p>
                </div>
                <div className="aboutCard">
                    <FaPaintBrush size={60} className="icon" />
                    <h2>Hajfestés</h2>
                    <p>Kiváló minőségű hajfestékekkel készített divatos és klasszikus hajszínek.</p>
                </div>
                <div className="aboutCard">
                    <FaUserTie size={60} className="icon" />
                    <h2>Borbély szolgáltatások</h2>
                    <p>Elegáns és precíz haj- és szakállvágás igényes férfiak számára.</p>
                </div>
                <div className="aboutCard">
                    <FaSpa size={60} className="icon" />
                    <h2>Hajkezelések</h2>
                    <p>Tápláló és regeneráló hajkezelések az egészséges és fényes hajért.</p>
                </div>
            </div>
        </div>
    );
}

export default About;