import React, { useState } from "react";
import './LogIn.css';
import { Link, useNavigate } from 'react-router-dom';
import { toast } from "react-toastify";

function Login() {
    const navigate = useNavigate();

    function Clicked(event) {
        event.preventDefault()

        var email = document.getElementById("email").value
        var password = document.getElementById("jelszo").value

        fetch("https://localhost:44364/api/Vasarlo/authenticate", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                EMail: email,
                Jelszo: password,
            }),
        })
            .then((response) => {
                if (!response.ok) {
                    throw new Error(`HTTP hiba! Státuszkód: ${response.status}:${response.body}`);
                }
                return response.json();
            })
            .then((data) => {

                localStorage.setItem("user", JSON.stringify(data));
                toast.success("Sikeres Bejelentkezés!");
                navigate("/UserPage");
                window.location.reload();
            })
            .catch((error) => {
                console.error("Hiba történt:", error);
                toast.error("Hiba");
            });
    }

    return (
        <div className="login">
            <form>
                <input type="text" id="email" placeholder="email@email.com" />
                <input type="password" placeholder="Jelszó" id="jelszo" />
                <input type="submit" id="submit" onClick={Clicked} value={"Bejelentkezés"} />
                <div className="link2"><Link to="/registration">Regisztrálj!</Link></div>
            </form>

        </div>
    );
}

export default Login;
