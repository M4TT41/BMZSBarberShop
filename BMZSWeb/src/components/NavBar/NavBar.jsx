import React, { useState, useEffect } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import logo from '../assets/BMZSwhite.svg';
import './NavBar.css';
import { UserAvatarFilledAlt } from "@carbon/icons-react";

function NavBar() {
    const [user, setUser] = useState(null);
    const [publicId, setPublicId] = useState('');
    const [isNavOpen, setIsNavOpen] = useState(false);
    const navigate = useNavigate();

    const cloudName = 'bmzsbarbershop';

    const getImageUrl = (publicId) =>
        publicId
            ? `https://res.cloudinary.com/${cloudName}/image/upload/${publicId}`
            : '';

    useEffect(() => {
        const storedUser = localStorage.getItem('user');
        if (storedUser) {
            const parsedUser = JSON.parse(storedUser);
            setUser(parsedUser);
            if (parsedUser.ProfilePic) {
                setPublicId(parsedUser.ProfilePic);
            }
        }
    }, []);

    const handleLoginClick = () => {
        navigate(user ? '/userpage' : '/login');
    };

    return (
        <nav className="menu">
            <div className={`navBar ${isNavOpen ? 'active' : ''}`}>
                <Link to="/gallery" aria-label="Gallery" onClick={() => setIsNavOpen(false)}>
                    Galéria&#8192;
                </Link>
                <Link to="/" aria-label="Home" onClick={() => setIsNavOpen(false)}>
                    <img src={logo} alt="Logo" className="logo" />
                </Link>
                <Link to="/city" aria-label="Booking" onClick={() => setIsNavOpen(false)}>
                    Foglalás
                </Link>
            </div>
            <div className="loginButton" onClick={handleLoginClick} aria-label="Login">
                {user ? (
                    <img
                        src={getImageUrl(publicId)}
                        alt="Avatar"
                        className="avatarImg"
                    />
                ) : (
                    <UserAvatarFilledAlt className="avatarImg" />
                )}
            </div>
        </nav>
    );
}

export default NavBar;
