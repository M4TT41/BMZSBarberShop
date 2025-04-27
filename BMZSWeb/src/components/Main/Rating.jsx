import React, { useEffect } from "react"
import './Rating.css'
import { useState } from "react"
import {FaStar} from "react-icons/fa"

function Rating(){


    const [pictures,setPictures] = useState([])
    const [bestPictures,setBestPictures] = useState([])
    const [rating,setRating] = useState({})


    useEffect(() =>{
        fetch('https://localhost:44364/api/Szavazott')
        .then(response => response.json())
        .then(data => setPictures(data))
        .catch(error => console.log(error))
    },[])

    useEffect(() => {
        const fetchData = async () => {
            let mySet = new Set();
            let obj = [];
    
            for (const element of pictures) {
                if (!mySet.has(element.KepekId)) {
                    mySet.add(element.KepekId);
    
                    let obj1 = {};
                    let s = 0;
                    let ind = 0;
    
         
                    for (const i of pictures) {
                        if (element.KepekId === i.KepekId) {
                            ind = ind + 1;
                            s = s + i.Csillag;
                        }
                    }
                    
                    let avg = s / ind;
    
                    Object.defineProperties(obj1, {
                        kId: {
                            value: element.KepekId,
                            writable: true,
                        },
                        csSz: {
                            value: s,
                            writable: true
                        },
                        csA: {
                            value: avg.toFixed(1)
                        },
                  
                    });
    
                    obj.push(obj1);
                }
            }
    
            setRating(obj.sort((a, b) => b.csA - a.csA));
        };
    
        fetchData();
    
    }, [pictures]);
    

    useEffect(() => {
            let obj = [rating[0], rating[1], rating[2]]; 
            let arr = []

        
            fetch('https://localhost:44364/api/Kepek')
                .then(response => response.json())
                .then(data => {
                    data.forEach((img) => {
                        obj.forEach((best) => {
                            let picture = {}
                            Object.defineProperties(picture, {
                                kId: {
                                    value: best.kId
               
                                },
                                csSz: {
                                    value: best.csSz
               
                                },
                                csA: {
                                    value: best.csA
                                },
                                url: {
                                    value: img.EleresiUt
                                }
                            });


                            if(best.kId === img.Id){
                                arr.push(picture)
             
                            }
                        })
                    });
                    
                    setBestPictures(arr.sort((a, b) => b.csA - a.csA))
                })
                .catch(error => console.log(error));
         
                

    }, [rating]);
 

    
    return(
        <div className="ratingContainer">
            <div className="bestRatings"
            >
                <div className="ratingTitle">
                    <h1> LEGJOBBAK ÉRTÉKELT HAJAINK:</h1>
                </div>
            <div className="images">
                {bestPictures.map((picture) => {
                    return (
                        <div className="imageContainer">
                            
                        <img 
                            key={picture.kId} 
                            src={picture.url} 
                            alt="legjobb"
                            className="galleryImage ratingImage"
                        />
                        <p className="ratingText">{picture.csA} {<FaStar className="star"/>}</p>
                       </div>
                        );
                    })}
            </div>
            </div>
        </div>

    )
}

export default Rating