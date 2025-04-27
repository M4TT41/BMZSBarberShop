import React, { useState } from "react";
import { Link , useNavigate} from "react-router-dom";
import { toast} from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import "./Registration.css";

function Registration() {
    const navigate = useNavigate()
    const [password, setPassword] = useState("");
    const [matchPassword, setMatchPassword] = useState("");
    const [email, setEmail] = useState("");
    const [user, setUser] = useState("");
    const [nickname, setNickname] = useState("");
    const [tele, setTele] = useState("");
    const [checked, setChecked] = useState(false);
    const [errorMessage, setErrorMessage] = useState("Kezdj el gépelni!");
    const [matchError, setMatchError] = useState("Kezdj el gépelni!");
    const [emailError, setEmailError] = useState("Kezdj el gépelni!");
    const [userError, setUserError] = useState("Kezdj el gépelni!");
    const [nickError, setNickError] = useState("Kezdj el gépelni!");
    const [teleError, setTeleError] = useState("Kezdj el gépelni!");

    const passwordCheck = (input) => {
        const lowerCase = /[a-z]/g;
        const upperCase = /[A-Z]/g;
        const numbers = /[0-9]/g;
        const special = /\W|_/g;
        const space = /\s/;

        if (!input) return "Kezdj el gépelni!";
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
            ? "Megfelelő E-mail cím!"
            : "Nem megfelelő E-mail cím!";

    const validateUser = (input) =>
        input.length >= 5 ? "Megfelelő felhasználónév!" : "Legalább 5 karakterből kell állnia!";

    const validatePhone = (input) =>
        /^(\+36|06)?\s*(20|30|31|50|70)\s*\d{3}\s*\d{4}$/.test(input)
            ? "Megfelelő telefonszám!"
            : "Nem megfelelő telefonszám!";

    const handleSubmit = (event) => {
        event.preventDefault();

        if (!checked) {
            toast.error("El kell fogadni a felhasználási feltételeket!");
            return;
        }
        if (emailError !== "Megfelelő E-mail cím!") {
            toast.error(emailError);
            return;
        }
        if (userError !== "Megfelelő felhasználónév!") {
            toast.error(userError);
            return;
        }
        if (nickError !== "Megfelelő felhasználónév!") {
            toast.error(nickError);
            return;
        }
        if (teleError !== "Megfelelő telefonszám!") {
            toast.error(teleError);
            return;
        }
        if (errorMessage !== "Erős jelszó!") {
            toast.error(errorMessage);
            return;
        }
        if (matchError !== "Jelszó megegyezik!") {
            toast.error(matchError);
            return;
        }

        fetch("https://localhost:44364/api/Vasarlo", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                FelhasznaloNev: user,
                Nev: nickname,
                TelefonSzam: tele,
                ProfilePic: null,
                EMail: email,
                Jelszo: password
            })
        })
            .then((response) => {
                if (response.status === 409) throw new Error("Ez az e-mail cím már regisztrálva van.");
                if (!response.ok) throw new Error(`Hiba: ${response.status}`);
                return response.json();
            })
            .then(() => {toast.success("Sikeres regisztráció!")
                navigate('/login')
            })
            .catch((error) => toast.error(error.message));
    };

    return (
        <div className="registration">
            <form onSubmit={handleSubmit}>
                <input
                    type="email"
                    placeholder="email@email.com"
                    value={email}
                    onChange={(e) => {
                        setEmail(e.target.value);
                        setEmailError(validateEmail(e.target.value));
                    }}
                />
                <input
                    type="text"
                    placeholder="Felhasználónév"
                    value={user}
                    onChange={(e) => {
                        setUser(e.target.value);
                        setUserError(validateUser(e.target.value));
                    }}
                />
                <input
                    type="text"
                    placeholder="Teljes név"
                    value={nickname}
                    onChange={(e) => {
                        setNickname(e.target.value);
                        setNickError(validateUser(e.target.value));
                    }}
                />
                <input
                    type="tel"
                    placeholder="+36 30 222 2222"
                    value={tele}
                    onChange={(e) => {
                        setTele(e.target.value);
                        setTeleError(validatePhone(e.target.value));
                    }}
                />
                <input
                    type="password"
                    placeholder="Jelszó"
                    value={password}
                    onChange={(e) => {
                        setPassword(e.target.value);
                        setErrorMessage(passwordCheck(e.target.value));
                    }}
                />
                <input
                    type="password"
                    placeholder="Jelszó mégegyszer"
                    value={matchPassword}
                    onChange={(e) => {
                        setMatchPassword(e.target.value);
                        setMatchError(e.target.value === password ? "Jelszó megegyezik!" : "Jelszó nem egyezik!");
                    }}
                />
                <label>
                    <input
                        type="checkbox"
                        checked={checked}
                        onChange={(e) => setChecked(e.target.checked)}
                    /> Elfogadom a felhasználási feltételeket
                </label>
                <input type="submit"  value="Regisztráció" />
                <div className="link2">
                    <Link to="/login">Jelentkezz be!</Link>
                </div>
            </form>
           
        </div>
    );
}

export default Registration;
