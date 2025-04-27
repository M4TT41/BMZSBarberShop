import React from "react";
import "./Opening.css";
import { HiOutlineQuestionMarkCircle } from "react-icons/hi2";

function Opening() {
  return (
    <div className="opening">
      <div className="mapContainer">
        <h2 className="mapTitle">ELHELYEZKEDÉSÜNK</h2>
        <div className="map">
          <iframe 
            src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2708.279420381466!2d16.915572812586163!3d47.2502391710392!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x47694ce7820c5ba5%3A0x5f639e6d61f7151b!2zU8OhcnbDoXIsIEVyZMOpbHkgdS4gMzcsIDk2MDA!5e0!3m2!1shu!2shu!4v1745674242851!5m2!1shu!2shu"
            width="100%" 
            style={{ border: 0 }} 
            allowFullScreen="" 
            loading="lazy"
            title="Google Térkép"
            className="mapping"
          ></iframe>
        </div>
      </div>

      <div className="openHours">
        <h1 className="openHoursTitle">
          <span>NYITVATARTÁS</span> 
          <div className="tooltip">
            <HiOutlineQuestionMarkCircle className="tooltipIcon" />
            <span className="tooltiptext">A nyitvatartás a fodrászatoktól és a fodrászok beosztasásától változhat!</span>
          </div>
        </h1>

        <div className="openHoursContainer">
          <div className="openHoursRow">
            <div className="openHoursDay">Hétfő</div>
            <div className="openHoursTime">8:00-16:00</div>
          </div>
          <div className="openHoursRow">
            <div className="openHoursDay">Kedd</div>
            <div className="openHoursTime">8:00-16:00</div>
          </div>
          <div className="openHoursRow">
            <div className="openHoursDay">Szerda</div>
            <div className="openHoursTime">8:00-16:00</div>
          </div>
          <div className="openHoursRow">
            <div className="openHoursDay">Csütörtök</div>
            <div className="openHoursTime">8:00-16:00</div>
          </div>
          <div className="openHoursRow">
            <div className="openHoursDay">Péntek</div>
            <div className="openHoursTime">8:00-16:00</div>
          </div>
          <div className="openHoursRow">
            <div className="openHoursDay">Szombat</div>
            <div className="openHoursTime">8:00-16:00</div>
          </div>
          <div className="openHoursRow">
            <div className="openHoursDay">Vasárnap</div>
            <div className="openHoursTime">Zárva</div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Opening;
