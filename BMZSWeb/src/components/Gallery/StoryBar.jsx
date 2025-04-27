import React, { useState, useEffect } from "react";
import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import "./StoryBar.css";
import { FaStar } from "react-icons/fa";
import spinner from '../assets/spinner.svg';
import { toast, ToastContainer } from "react-toastify";


function StoryBar({ barbers, selectedBarber, setSelectedBarber }) {
    const [images, setImages] = useState([]);
    const [isLoading, setIsLoading] = useState(false);
    const [userRatings, setUserRatings] = useState({});
    const user = JSON.parse(localStorage.getItem("user"));

    useEffect(() => {
        document.querySelectorAll("img").forEach((element) => {
            element.setAttribute("draggable", false);
        });

        if (selectedBarber) {
            setIsLoading(true);
            fetch(`https://localhost:44364/api/Kepek${selectedBarber}/2`)
                .then((res) => res.json())
                .then((data) => {
                    setImages(Array.isArray(data) ? data : [data]);
                    setIsLoading(false);
                })
                .catch(() => {
                    setIsLoading(false);
                    setImages([]);
                });
        } else {
            setImages([]);
        }
    }, [selectedBarber]);

    useEffect(() => {
        if (images.length > 0 && user) {
            images.forEach((img) => {
                fetch(`https://localhost:44364/api/Szavazott${img.Id}/${user.Id}`)
                    .then((response) => response.json())
                    .then((data) => {
                        setUserRatings((prev) => ({
                            ...prev,
                            [img.Id]: data.Csillag,
                        }));
                    })
                    .catch((error) => console.log(error));
            });
        }
    }, [images, user]);

    const handleSelectBarber = (barber) => {
        const newBarberId = barber.Id.toString();
        if (selectedBarber === newBarberId) {
            setSelectedBarber(null);

        } else {
            setSelectedBarber(newBarberId);

        }
    };

    const handleRate = async (imageId, stars) => {
        if (!user) {
            toast.error("Nem vagy bejelentkezve!");
            return;
        }

        fetch(`https://localhost:44364/api/Szavazott/Szavazas/${imageId}/${user.Id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                Csillag: stars,
            }),
        })
            .then((response) => response.json())
            .then((csillag) => {
                setUserRatings((prev) => ({
                    ...prev,
                    [imageId]: csillag,
                }));
            });
    };

    return (
        <>
            <div className="storyContainer">
                <Slider dots={false} infinite={false} speed={250} slidesToShow={6} slidesToScroll={3} className="story">
                    {barbers.map((barber) => (
                        <div
                            className={`picture ${selectedBarber === barber.Id.toString() ? "selected" : ""}`}
                            key={barber.Id}
                            onClick={() => handleSelectBarber(barber)}
                        >
                            <img src={barber.PfpName} alt={barber.Nev} />
                            <div>{barber.Nev}</div>
                        </div>
                    ))}
                </Slider>
            </div>

            {selectedBarber && images.length > 0 ? (
                <div className="barberGallery">
                    {isLoading ? (
                        <div className="loading">
                            <img src={spinner} alt="loading" />
                        </div>
                    ) : (
                        <div className="gallery">
                            {images.map((img) => (
                                <div className="galleryCard" key={img.Id}>
                                    <div>
                                        <img src={img.EleresiUt} alt={img.kepNev} className="galleryImage"/>
                                        <p>{img.Leiras}</p>
                                        <div className="rating">
                                            {[5, 4, 3, 2, 1].map((star) => (
                                                <FaStar
                                                    key={star}
                                                    onClick={() => handleRate(img.Id, star)}
                                                    className={`star ${userRatings[img.Id] >= star ? "active" : ""}`}
                                                    style={{ cursor: "pointer" }}
                                                />
                                            ))}
                                        </div>
                                    </div>
                                </div>
                            ))}
                        </div>
                    )}
                </div>
            ) : images.length === 0 && selectedBarber ? (
                <div className="no"><span className="noBarber">A fodrász nem rendelkezik képpel!</span></div>
            ) : (
                <div className="no"><span className="noBarber">Válassz egy fodrászt a képek megtekintéséhez!</span></div>
            )}
            <ToastContainer />
        </>
    );
}

export default StoryBar;
