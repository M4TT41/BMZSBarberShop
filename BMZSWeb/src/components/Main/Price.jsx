import React from "react";
import "./Price.css";
import { FaCut, FaChild, FaMale, FaUserTie, FaPaintBrush, FaSprayCan, FaHandHoldingHeart, FaComments } from "react-icons/fa";

function Price() {
    return (
        <div className="priceContainer">
            <h1 className="priceTitle">Árlista</h1>
            <section className="priceListContainer">
                <div className="priceColumn">
                    <div className="priceItem">
                        <div className="serviceInfo">
                            <FaMale className="serviceIcon" />
                            <span className="serviceName">Hajvágás</span>
                        </div>
                        <span className="priceValue">3500 Ft</span>
                    </div>
                    <div className="priceItem">
                        <div className="serviceInfo">
                            <FaChild className="serviceIcon" />
                            <span className="serviceName">Gyermek hajvágás</span>
                        </div>
                        <span className="priceValue">2500 Ft</span>
                    </div>
                    <div className="priceItem">
                        <div className="serviceInfo">
                            <FaCut className="serviceIcon" />
                            <span className="serviceName">Hosszú hajvágás</span>
                        </div>
                        <span className="priceValue">4500 Ft</span>
                    </div>
                    <div className="priceItem">
                        <div className="serviceInfo">
                            <FaUserTie className="serviceIcon" />
                            <span className="serviceName">Szakáll igazítás</span>
                        </div>
                        <span className="priceValue">1500 Ft</span>
                    </div>
                    <div className="priceItem">
                        <div className="serviceInfo">
                            <FaPaintBrush className="serviceIcon" />
                            <span className="serviceName">Hajszínezés</span>
                        </div>
                        <span className="priceValue">4000 Ft</span>
                    </div>
                </div>
                <div className="priceColumn">
                    <div className="priceItem">
                        <div className="serviceInfo">
                            <FaSprayCan className="serviceIcon" />
                            <span className="serviceName">Hajformázás</span>
                        </div>
                        <span className="priceValue">2000 Ft</span>
                    </div>
                    <div className="priceItem">
                        <div className="serviceInfo">
                            <FaHandHoldingHeart className="serviceIcon" />
                            <span className="serviceName">Fejbőr masszázs</span>
                        </div>
                        <span className="priceValue">2500 Ft</span>
                    </div>
                    <div className="priceItem">
                        <div className="serviceInfo">
                            <FaComments className="serviceIcon" />
                            <span className="serviceName">Konzultáció</span>
                        </div>
                        <span className="priceValue">1000 Ft</span>
                    </div>
                    <div className="priceItem">
                        <div className="serviceInfo">
                            <FaUserTie className="serviceIcon" />
                            <span className="serviceName">Borotválás</span>
                        </div>
                        <span className="priceValue">3000 Ft</span>
                    </div>
                    <div className="priceItem">
                        <div className="serviceInfo">
                            <FaHandHoldingHeart className="serviceIcon" />
                            <span className="serviceName">Férfi arcpakolás</span>
                        </div>
                        <span className="priceValue">1500 Ft</span>
                    </div>
                </div>
            </section>
        </div>
    );
}

export default Price;
