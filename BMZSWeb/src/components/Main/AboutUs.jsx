import React, { useState } from "react";
import { FaFacebook, FaInstagram, FaTwitter, FaEnvelope, FaPhone, FaMapMarkerAlt, FaUser, FaComment, FaCheckCircle, FaExclamationCircle, FaSpinner, FaPaperPlane } from 'react-icons/fa';
import "./AboutUs.css";
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

function AboutUs() {
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    message: ''
  });

  const [isSubmitting, setIsSubmitting] = useState(false);
;

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setIsSubmitting(true);

    const messageData = {
      content: `**Új üzenet**\n\n**Név**: ${formData.name}\n**Email**: ${formData.email}\n**Üzenet**: ${formData.message}`,
    };

    try {
      const response = await fetch('https://discord.com/api/webhooks/1357406514712936558/EKObT4I1g0vJStjdb9WqA2DUqVxa4WMbCM6MdsxuFWfY3ei5MAE52ltFoBjwa3t0RjB4', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(messageData),
      });

      if (response.ok) {
        setFormData({ name: '', email: '', message: '' }); 
        toast.success("Sikeres üzenetküldés!")
      } else {
        toast.error("Sikertelen üzenetküldés!")
      }
    } catch (err) {
      
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <div className="aboutUs">
      <div className="contactSection">
        <section className="contactUs">
        <div className="header">
          <FaEnvelope style={{ marginRight: '1rem' }} />
          írj nekünk!
          </div>
          <form onSubmit={handleSubmit}>
            <div className="formItem">
              <label htmlFor="name">
                <FaUser className="formIcon" />
                Név:
              </label>
              <input
                type="text"
                id="name"
                name="name"
                value={formData.name}
                onChange={handleChange}
                required
                placeholder="Gipsz Jakab"
              />
            </div>
            <div className="formItem">
              <label htmlFor="email">
                <FaEnvelope className="formIcon" />
                Email:
              </label>
              <input
                type="email"
                id="email"
                name="email"
                value={formData.email}
                onChange={handleChange}
                required
                placeholder="email@email.com"
              />
            </div>
            <div className="formItem">
              <label htmlFor="message">
                <FaComment className="formIcon" />
                Üzenet:
              </label>
              <textarea
                id="message"
                name="message"
                value={formData.message}
                onChange={handleChange}
                required
                placeholder="Miben tudunk segíteni?"
              />
            </div>
            <button type="submit" disabled={isSubmitting}>
            <FaPaperPlane style={{ marginRight: '0.5rem' }} /> Küldés
            </button>
            
          </form>
        </section>

        <section className="contact">
        <div className="header">
          <FaPhone style={{ marginRight: '1rem' }} />
          Elérhetőség
        </div>
          <div className="info">
            <div className="textInfo">
              <p><FaEnvelope className="contactIcon" /> <strong>Email:</strong></p>
              <p>bmzsgames@gmail.com</p>
            </div>
            <div className="textInfo">
              <p><FaPhone className="contactIcon" /> <strong>Telefonszám:</strong></p>
              <p>06 30 323 7997</p>
            </div>
            <div className="textInfo">
              <p><FaMapMarkerAlt className="contactIcon" /> <strong>Cím:</strong></p>
              <p>Sárvár, Erdély utca 37</p>
            </div>
          </div>
          <div className="social">
            <a className="socialIcon" href="#"><FaFacebook /></a>
            <a className="socialIcon" href="#"><FaInstagram /></a>
            <a className="socialIcon" href="#"><FaTwitter /></a>
        </div>
        </section>
      </div>
    
    </div>
  );
}

export default AboutUs;
