import React, { useState, useEffect } from "react";
import { Link , useNavigate} from 'react-router-dom';
import Select from 'react-select';
import "./Barbershop.css";

function Barbershop() {
    const telepulesId = parseInt(localStorage.getItem('telepules'));



    const [selectedBarbershop, setSelectedBarbershop] = useState(null);
    const [selectedBarber, setSelectedBarber] = useState(null);
    const [selectedService, setSelectedService] = useState(null);

    const [barbershopOptions, setBarbershopOptions] = useState([]);
    const [barberOptions, setBarberOptions] = useState([]);
    const [serviceOptions, setServiceOptions] = useState([]);

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
        placeholder: (provided) => ({
            ...provided,
            color: '#666',
            fontSize: '0.9rem'
        }),
        singleValue: (provided) => ({
            ...provided,
            fontSize: '0.9rem'
        }),
        dropdownIndicator: (provided) => ({
            ...provided,
            padding: '2px'
        }),
        clearIndicator: (provided) => ({
            ...provided,
            padding: '2px'
        }),
        menu: (provided) => ({
            ...provided,
            marginTop: '4px',
            width: '250px'
        }),
        valueContainer: (provided) => ({
            ...provided,
            padding: '0 4px'
        })
    };

    const navigate = useNavigate()
    const handleBack = (e)  => {
        e.preventDefault()
        navigate("/city")
    }

    useEffect(() => {
        fetch('https://localhost:44364/api/Fodraszat/0')
            .then(response => {
                if (!response.ok) {
                    throw new Error(`Hiba történt: ${response.status}`);
                }
                return response.json();
            })
            .then(data => {
                const filtered = data.filter(a => a.VarosId === telepulesId);
                const options = filtered.map(a => ({
                    value: a.Id,
                    label: `${a.Utca} ${a.HazSzam}`
                }));
                setBarbershopOptions(options);
            })
            .catch(error => {
                console.error('Hiba a fodrászatok lekérésekor:', error);
            });
    }, [telepulesId]);

    useEffect(() => {
        if (selectedBarbershop) {
            fetch(`https://localhost:44364/api/Fodrasz/GetFodraszByFodraszat/${selectedBarbershop.value}/1`)
                .then(response => {
                    if (!response.ok) {
                        throw new Error(`Hiba történt: ${response.status}`);
                    }
                    return response.json();
                })
                .then(data => {
                    const options = data.map(a => ({
                        value: a.Id,
                        label: a.Nev
                    }));
                    setBarberOptions(options);
                })
                .catch(error => {
                    console.error('Hiba a fodrászok lekérésekor:', error);
                });
        } else {
            setBarberOptions([]);
            setSelectedBarber(null);
        }
    }, [selectedBarbershop]);

    useEffect(() => {
        fetch('https://localhost:44364/api/Szolgaltatas')
            .then(response => {
                return response.json();
            })
            .then(data => {
                const options = data.map(service => ({
                    value: service.Id,
                    label: `${service.SzolgaltatasNev} - ${service.Ar} Ft (${service.Idotartalom} perc)`
                }));
                setServiceOptions(options);
            })
            .catch(error => {
                console.error('Hiba a szolgáltatások lekérésekor:', error);
            });
    }, []);

    const handleBarbershopChange = (selectedOption) => {
        setSelectedBarbershop(selectedOption);
    };

    const handleBarberChange = (selectedOption) => {
        setSelectedBarber(selectedOption);
        if (selectedOption) {
            localStorage.setItem("fodrasz", selectedOption.value);
        }
    };

    const handleServiceChange = (selectedOption) => {
        setSelectedService(selectedOption);
        if (selectedOption) {
            localStorage.setItem("szolgaltatas", selectedOption.value);
        }
    };

    const isValidSelection = selectedBarbershop && selectedBarber && selectedService;

    return  (
        <div className="booking">
            <div className='current'>
                <p>Város</p>
                <p className="now">Fodrászat</p>
                <p>Foglalás</p>
                <p>Visszaigazolás</p>
            </div>

            <div className="bookContainer">
                <div className="comboContainer">
                    <Select
                        value={selectedBarbershop}
                        onChange={handleBarbershopChange}
                        options={barbershopOptions}
                        styles={customStyles}
                        placeholder="Válassz fodrászatot!"
                        isClearable
                        isSearchable
                    />

                    <Select
                        value={selectedBarber}
                        onChange={handleBarberChange}
                        options={barberOptions}
                        styles={customStyles}
                        placeholder="Válassz fodrászt!"
                        isClearable
                        isSearchable
                        isDisabled={!selectedBarbershop}
                    />

                    <Select
                        value={selectedService}
                        onChange={handleServiceChange}
                        options={serviceOptions}
                        styles={customStyles}
                        placeholder="Válassz szolgáltatást!"
                        isClearable
                        isSearchable
                    />
                </div>

                <div className="next">
                    {isValidSelection ? (
                        <button
                            className="text"
                            onClick={() => {
                                localStorage.setItem("fodraszat", selectedBarbershop.value);
                                navigate('/calendar',{ replace: true })
                            }}
                        >
                            Következő
                        </button>
                    ) : (
                        <p className="error">Válassz ki egy fodrászatot, fodrászt és szolgáltatást a továbblépéshez!</p>
                    )}
                </div>
                    <button className="cancelButton" onClick={handleBack}>Vissza</button>
            </div>
        </div>
    )
}

export default Barbershop;


