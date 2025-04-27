import React, { useState, useEffect } from 'react';
import './GoTop.css';

const GoTop = () => {
  const [isVisible, setIsVisible] = useState(false);

  const handleScroll = () => {
    if (window.scrollY > 50) {
      setIsVisible(true);
    } else {
      setIsVisible(false);
    }
  };

  const scrollToTop = () => {
    window.scrollTo({
      top: 0,
      behavior: 'smooth',
    });
  };

  useEffect(() => {
    window.addEventListener('scroll', handleScroll);
    return () => {
      window.removeEventListener('scroll', handleScroll);
    };
  }, []);

  return (
    <button
      className={`goTopButton ${isVisible ? 'visible' : ''}`}
      onClick={scrollToTop}
    >
      <span className='arrow'>&#8679;</span>
    </button>
  );
};

export default GoTop;