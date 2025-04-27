import React, { useState, useEffect, useRef } from 'react';
import { useNavigate } from 'react-router-dom';
import emailjs from '@emailjs/browser';
import './Booking.css';
import { toast} from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

function BookingSummary() {
    const navigate = useNavigate();
    const form = useRef();

    const user = JSON.parse(localStorage.getItem("user"));
    const fodrasz = localStorage.getItem("fodrasz");
    const foglalas = localStorage.getItem("foglalas");

    const [szolgaltatas, setSzolgaltatas] = useState();
    const [fodraszat, setFodraszat] = useState();
    const [telepules, setTelepules] = useState();
    const [fodraszNev, setFodraszNev] = useState();
    const [hely, setHely] = useState();
    const [returnNaptar, setReturnNaptar] = useState();

    useEffect(() => {
        fetch(`https://localhost:44364/api/Fodraszat/${localStorage.getItem("fodraszat")}`)
            .then(response => response.json())
            .then(data => setFodraszat(data));

        fetch(`https://localhost:44364/api/Varos/${localStorage.getItem('telepules')}`)
            .then(response => response.json())
            .then(data => setTelepules(data));

        fetch(`https://localhost:44364/api/Szolgaltatas/${localStorage.getItem("szolgaltatas")}`)
            .then(response => response.json())
            .then(data => setSzolgaltatas(data));
    }, []);

    useEffect(() => {
        if (fodrasz) {
            fetch(`https://localhost:44364/api/Fodrasz/${fodrasz}/1`)
                .then(response => response.json())
                .then(data => setFodraszNev(data));
        }
    }, [fodrasz]);

    useEffect(() => {
        if (telepules && fodraszat) {
            setHely(`${telepules.TelepulesNev} ${fodraszat.Utca} ${fodraszat.HazSzam}`);
        }
    }, [telepules, fodraszat]);

    useEffect(() => {
        if (returnNaptar != null) {
            fetch("https://localhost:44364/api/Foglalas", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    SzolgaltatasId: szolgaltatas?.Id,
                    VasarloId: user?.Id,
                    NaptarId: returnNaptar?.Id
                })
            })
            .then(response => response.json())
            .then(() => {
                toast.success("Sikeres foglalás!");
                navigate('/',{replace : true});
            });
            setReturnNaptar(null);
        }
    }, [returnNaptar]);

    const handleBack = (e) => {
        e.preventDefault();
        navigate(-1);
    };

    const sendEmail = (e) => {
        e.preventDefault();

        emailjs
            .sendForm('service_v8vxanq', 'template_pju04ee', form.current, {
                publicKey: 'OuLw2CHvoU66s3pUn',
            })
            .then(() => {
                fetch("https://localhost:44364/api/Naptar", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify({
                        Datum: foglalas,
                        FodraszId: fodrasz
                    })
                })
                .then(response => response.json())
                .then(data => {
                   
                    setReturnNaptar(data);
                });
            });
    };

    return (
        <div className="summary">
            <form ref={form} onSubmit={sendEmail}>
                <div className="summaryContainer">
                    <div className='current'>
                        <p>Város</p>
                        <p>Fodrászat</p>
                        <p>Foglalás</p>
                        <p className="now">Visszaigazolás</p>
                    </div>

                    <div className="summaryBox">
                        <h2>Foglalás összegzése</h2>
                        <div className="summaryDetails">
                            <input hidden name='nev' value={user?.Nev || ''} />
                            <input hidden name='to_email' value={user?.EMail || ''} />
                            <div className="summaryItem">
                                <h3>Fodrászat</h3>
                                <input type="text" name="fodraszat" value={hely || ''} readOnly />
                            </div>
                            <div className="summaryItem">
                                <h3>Fodrász</h3>
                                <input type="text" name="fodrasz" value={fodraszNev?.Nev || ''} readOnly />
                            </div>
                            <div className="summaryItem">
                                <h3>Szolgáltatás</h3>
                                <input name="szolgaltatas" value={szolgaltatas?.SzolgaltatasNev || ''} readOnly />
                            </div>
                            <div className="summaryItem">
                                <h3>Időpont</h3>
                                <input
                                    name="idopont"
                                    value={foglalas ? new Date(foglalas).toLocaleString('hu-HU', {
                                        year: 'numeric',
                                        month: 'long',
                                        day: 'numeric',
                                        hour: 'numeric',
                                        minute: 'numeric'
                                    }) : ''}
                                    readOnly
                                />
                            </div>
                        </div>

                        <div className="bookingActions">
                            <button className="confirmButton" type="submit">
                                Foglalás megerősítése
                            </button>
                            <button className="cancelButton" onClick={handleBack}>
                                Vissza
                            </button>
                        </div>
                    </div>
                </div>
            </form>

          
        </div>
    );
}

export default BookingSummary;
