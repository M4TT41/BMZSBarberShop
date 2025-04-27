import React, { useState, useEffect, useRef } from 'react';
import './change.css';
import { UserAvatarFilledAlt } from "@carbon/icons-react";
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { HiOutlineQuestionMarkCircle } from "react-icons/hi2";

function PwdChange() {
    const localUser = JSON.parse(localStorage.getItem('user')) || {};

    const [nickname, setNickname] = useState(localUser.Nev || "");
    const [user, setUser] = useState(localUser.FelhasznaloNev || "");
    const [tele, setTele] = useState(localUser.TelefonSzam || "");
    const [email, setEmail] = useState(localUser.EMail || "");
    const [password, setPassword] = useState("");
    const [matchPassword, setMatchPassword] = useState("");
    const [oldPassword, setOldPassword] = useState("");

    const [nickError, setNickError] = useState("");
    const [userError, setUserError] = useState("");
    const [teleError, setTeleError] = useState("");
    const [emailError, setEmailError] = useState("");
    const [matchError, setMatchError] = useState("");
    const [errorMessage, setErrorMessage] = useState("");

    const [publicId, setPublicId] = useState(localUser.ProfilePic || "");
    const uploadButtonRef = useRef(null);
    const cloudName = 'bmzsbarbershop';
    const uploadPreset = 'userimageupload';

    const getImageUrl = (publicId) =>
        publicId
            ? `https://res.cloudinary.com/${cloudName}/image/upload/${publicId}`
            : UserAvatarFilledAlt;

    useEffect(() => {
        if (window.cloudinary && uploadButtonRef.current) {
            const widget = window.cloudinary.createUploadWidget(
                {
                    cloudName,
                    uploadPreset,
                },
                (error, result) => {
                    if (!error && result && result.event === 'success') {
  
                        const updatedUser = { ...localUser, ProfilePic: result.info.public_id };
                        localStorage.setItem('user', JSON.stringify(updatedUser));
       
                        setPublicId(result.info.public_id);
                        toast.success("Profilkép sikeresen frissítve!");
                        fetch(`https://localhost:44364/api/Vasarlo/Patch/id?id=${updatedUser.Id}`, {
                            method: "PATCH",
                            headers: {
                                "Content-Type": "application/json"
                            },
                            body: JSON.stringify(updatedUser),
                        });
                    }
                }
            );

            const button = uploadButtonRef.current;
            const clickHandler = () => widget.open();
            button.addEventListener("click", clickHandler);

            return () => button.removeEventListener("click", clickHandler);
        }
    }, []);

    useEffect(() => {
        if (publicId) {
            const updatedUser = { ...localUser, ProfilePic: publicId };
            localStorage.setItem("user", JSON.stringify(updatedUser));
        }
    }, [publicId]);

    const passwordCheck = (input) => {
        const lowerCase = /[a-z]/g;
        const upperCase = /[A-Z]/g;
        const numbers = /[0-9]/g;
        const special = /\W|_/g;
        const space = /\s/;

        if (!input) return "Nem kötelező";
        if (!input.match(lowerCase)) return "Tartalmaznia kell kisbetűt!";
        if (!input.match(upperCase)) return "Legalább 1 nagybetűt kell tartalmaznia!";
        if (!input.match(numbers)) return "Legalább 1 számot kell tartalmaznia!";
        if (!input.match(special)) return "Legalább 1 speciális karaktert kell tartalmaznia!";
        if (input.match(space)) return "Nem tartalmazhat szóközt!";
        if (input.length < 8) return "Legalább 8 karakter hosszú legyen!";
        return "Erős jelszó!";
    };

    const validateEmail = (input) =>
        /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[A-Za-z]{2,}$/.test(input)
            ? ""
            : "Nem megfelelő E-mail cím!";

    const validateUser = (input) =>
        input.length >= 5 ? "" : "Legalább 5 karakterből kell állnia!";

    const validatePhone = (input) =>
        /^(\+36|06)?\s*(20|30|31|50|70)\s*\d{3}\s*\d{4}$/.test(input)
            ? ""
            : "Nem megfelelő telefonszám!";

    const handleSubmit = async (e) => {
        e.preventDefault();

        const userId = localUser.Id;
        if (!userId) {
            toast.error("Hiányzó felhasználói azonosító!");
            return;
        }

        if (password && password !== matchPassword) {
            toast.error("A jelszavak nem egyeznek!");
            return;
        }

        const updateData = {
            FelhasznaloNev: user,
            Nev: nickname,
            TelefonSzam: tele,
            EMail: email,
        };

        if (password) {
            updateData.NewPasswd = password;
            updateData.OldPasswd = oldPassword;
        }

        try {
            const response = await fetch(`https://localhost:44364/api/Vasarlo/Patch/id?id=${userId}`, {
                method: "PATCH",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(updateData),
            });

            if (response.ok) {
                const result = await response.json();
                toast.success("Sikeres változtatás!");
                localStorage.setItem("user", JSON.stringify(result));
            } else if (response.status === 409) {
                toast.error("Ez az email cím már használatban van.");
            } else if (response.status === 401) {
                toast.error("Hibás jelszó!");
            } else {
            }
        } catch (error) {
    
            toast.error("Hálózati hiba!");
        }
    };

    const isFormValid = () => {
        return (
            !nickError &&
            !userError &&
            !teleError &&
            !emailError &&
            !matchError &&
            nickname.length > 0 &&
            user.length > 0 &&
            tele.length > 0 &&
            email.length > 0
        );
    };

    return (
        <div className="changing">
            <div className="user">
                <div className="pfpContainer">
                    <img src={getImageUrl(publicId || localUser.ProfilePic)} className='pfp' alt="profil" />
                    <div className="pfpIcon" ref={uploadButtonRef} style={{ cursor: "pointer" }}>
                        <HiOutlineQuestionMarkCircle />
                    </div>
                </div>
                <form onSubmit={handleSubmit}>
                    <div className="userDetails">
                        <div className="userItems">
                            <div className="userItem">
                                <span>Név: </span>
                                <input
                                    type="text"
                                    value={nickname}
                                    onChange={(e) => {
                                        setNickname(e.target.value);
                                        setNickError(e.target.value.length >= 2 ? "" : "Minimum 2 karakter!");
                                    }}
                                />
                                {nickError && <span className="error">{nickError}</span>}
                            </div>

                            <div className="userItem">
                                <span>Felhasználónév: </span>
                                <input
                                    type="text"
                                    value={user}
                                    onChange={(e) => {
                                        setUser(e.target.value);
                                        setUserError(validateUser(e.target.value));
                                    }}
                                />
                                {userError && <span className="error">{userError}</span>}
                            </div>

                            <div className="userItem">
                                <span>Telefonszám: </span>
                                <input
                                    type="text"
                                    value={tele}
                                    onChange={(e) => {
                                        setTele(e.target.value);
                                        setTeleError(validatePhone(e.target.value));
                                    }}
                                />
                                {teleError && <span className="error">{teleError}</span>}
                            </div>

                            <div className="userItem">
                                <span>Email: </span>
                                <input
                                    type="text"
                                    value={email}
                                    onChange={(e) => {
                                        setEmail(e.target.value);
                                        setEmailError(validateEmail(e.target.value));
                                    }}
                                />
                                {emailError && <span className="error">{emailError}</span>}
                            </div>

                            <div className="userItem">
                                <span>Régi jelszó (nem kötelező): </span>
                                <input
                                    type="password"
                                    value={oldPassword}
                                    onChange={(e) => setOldPassword(e.target.value)}
                                />
                            </div>

                            <div className="userItem">
                                <span>Új jelszó (nem kötelező): </span>
                                <input
                                    type="password"
                                    value={password}
                                    onChange={(e) => {
                                        const value = e.target.value;
                                        setPassword(value);
                                        setErrorMessage(passwordCheck(value));
                                    }}
                                />
                                {errorMessage && <span className="error">{errorMessage}</span>}
                            </div>

                            <div className="userItem">
                                <span>Új jelszó még egyszer: </span>
                                <input
                                    type="password"
                                    value={matchPassword}
                                    onChange={(e) => {
                                        setMatchPassword(e.target.value);
                                        setMatchError(e.target.value === password ? "" : "Nem egyeznek!");
                                    }}
                                    disabled={!password}
                                />
                                {matchError && <span className="error">{matchError}</span>}
                            </div>
                        </div>

                        <input
                            className='submitButton'
                            type="submit"
                            value="Változtatás"
                            disabled={!isFormValid()}
                        />
                    </div>
                </form>
            </div>
           
        </div>
    );
}

export default PwdChange;
