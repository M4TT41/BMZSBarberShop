import React, { useState, useEffect } from "react";
import { Link, useNavigate, Navigate} from 'react-router-dom';
import Select from 'react-select';
import "./Barbershop.css";

function City() {
    var user = JSON.parse(localStorage.getItem("user"));

    const navigate = useNavigate()
    const handleBack = (e)  => {
        e.preventDefault()
        navigate("/")
    }
    const [options, setOptions] = useState([]);
    const [selectedCity, setSelectedCity] = useState(null);



    useEffect(() => {
        fetch('https://localhost:44364/api/Varos')
            .then(response => {
                if (!response.ok) {
                    throw new Error(`Hiba történt: ${response.status}`);
                }
                return response.json();
            })
            .then(data => {
                const formattedOptions = data.map(city => ({
                    value: city.Id,
                    label: city.TelepulesNev
                }));
                setOptions(formattedOptions);
            })
            .catch(error => {
                console.error('Hiba a lekérés során:', error);
            });
    }, []);

    const handleCityChange = (selectedOption) => {
        setSelectedCity(selectedOption);
    };

    const isValidSelection = selectedCity && selectedCity.value !== -1;

    const customStyles = {
        container: (provided) => ({
            ...provided,
            width: '250px', 
        }),
        control: (provided, state) => ({
            ...provided,
            backgroundColor: 'white',
            borderRadius: '4px',
            minHeight: '35px',
            padding: '0 2px',
            border: '1px solid #ccc',
            boxShadow: state.isFocused ? '0 0 0 1px #007bff' : null,
            '&:hover': {
                borderColor: '#007bff'
            }
        }),
        option: (provided, state) => ({
            ...provided,
            backgroundColor: state.isSelected ? '#007bff' : state.isFocused ? '#f0f0f0' : 'white',
            color: state.isSelected ? 'white' : 'black',
            cursor: 'pointer',
            padding: '4px 8px',
            fontSize: '0.9rem'
        }),
      

    };

    if (!user) {
        return <Navigate to="/login" />;
    }

    return (
        <div className="booking">
            <div className='current'>
                <p className="now">Város</p>
                <p>Fodrászat</p>
                <p>Foglalás</p>
                <p>Visszaigazolás</p>
            </div>
            <div className="bookContainer">
                <div className="comboContainer">
                    <Select
                        value={selectedCity}
                        onChange={handleCityChange}
                        options={options}
                        styles={customStyles}
                        placeholder="Válassz várost!"
                        isClearable
                        isSearchable
                    />
                </div>
                <div className="next">
                    {isValidSelection ? (
                        <button className="text" to="/booking" onClick={() => {
                            localStorage.setItem("telepules", selectedCity.value);
                            navigate('/booking',{ replace: true })
                        }}>
                            Következő
                        </button>
                    ) : (
                        <p className="error">Válassz ki egy várost a továbblépéshez!</p>
                    )}
                     
                </div>
                <button className="cancelButton" onClick={handleBack}>Vissza</button>
            </div>
        </div>
    );
}

export default City;
