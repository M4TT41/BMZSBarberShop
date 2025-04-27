import React, { useState, useEffect } from "react";
import "./Gallery.css";
import StoryBar from "./StoryBar";
import spinner from '../assets/spinner.svg';

import Select from "react-select";

function Gallery() {
    const [cities, setCities] = useState([]);
    const [allBarbers, setAllBarbers] = useState([]);
    const [filteredBarbers, setFilteredBarbers] = useState([]);
    const [selectedCityId, setSelectedCityId] = useState(null);
    const [loading, setLoading] = useState(true);
    const [selectedBarber, setSelectedBarber] = useState(null); 
    
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

    useEffect(() => {
        fetch("https://localhost:44364/api/Varos")
            .then(response => response.json())
            .then(data => {
                setCities(data);
                if (data.length > 0) {
                    setSelectedCityId(data[0].Id);
                }
            })
            .catch(error => console.error(error));
    }, []);

    useEffect(() => {
        fetch("https://localhost:44364/api/Fodrasz/1")
            .then(response => response.json())
            .then(data => {
                setAllBarbers(data);
                setLoading(false);
            })
            .catch(error => {
                console.error(error);
                setLoading(false);
            });
    }, []);

    useEffect(() => {
        if (!selectedCityId) {
            setFilteredBarbers(allBarbers);
        } else {
            fetch("https://localhost:44364/api/Fodraszat/0")
                .then(response => response.json())
                .then(fodraszatok => {
                    const cityFodraszatok = fodraszatok.filter(f => f.VarosId === selectedCityId);
                    const fodraszatIds = cityFodraszatok.map(f => f.Id);
                    const cityBarbers = allBarbers.filter(barber => fodraszatIds.includes(barber.FodraszatId));
                    setFilteredBarbers(cityBarbers);
                })
                .catch(error => console.error(error));
        }

 
        setSelectedBarber(null); 
    }, [selectedCityId, allBarbers]);

   
    return (
        <>
            <div className="selection">
                <Select
                    onChange={(selectedOption) => setSelectedCityId(selectedOption ? selectedOption.value : null)}
                    options={cities.map(city => ({
                        value: city.Id,
                        label: city.TelepulesNev
                    }))}
                    styles={customStyles}
                    placeholder="Válassz várost!"
                    isClearable
                    isSearchable
                />
            </div>
            
            {loading ? (
                <div className="loading">
                    <img className="loading" alt='spinner' src={spinner}/>
                </div>
            ) : (
                <StoryBar barbers={filteredBarbers} selectedBarber={selectedBarber} setSelectedBarber={setSelectedBarber} />
            )}
        </>
    );
}

export default Gallery;
