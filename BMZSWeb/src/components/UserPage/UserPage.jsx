import React, { useEffect, useState } from 'react';
import './UserPage.css';
import { useNavigate } from 'react-router-dom';
import spinner from '../assets/spinner.svg';
import { MdDelete } from "react-icons/md";
import { toast } from 'react-toastify';


function UserPage() {
  const [user, setUser] = useState(null);
  const [bookingId, setBookingId] = useState(null);
  const [bookedDates, setBookedDates] = useState([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();
  const currentDate = new Date()
  useEffect(() => {
    const storedUser = localStorage.getItem('user');
    if (storedUser) {
      setUser(JSON.parse(storedUser));
    } else {
      window.location.href = '/login';
    }
  }, []);


  useEffect(() => {
    if (user) {
      fetch(`https://localhost:44364/api/Foglalas/${user.Id}`)
        .then((response) => response.json())
        .then((data) => setBookingId(data))
        .catch((error) => console.log(error));
    }
  }, [user]);

  useEffect(() => {
    const fetchBookedDates = async () => {
      if (bookingId) {
        let arr = [];
        try {
          for (const x of bookingId) {
        
            const response = await fetch(`https://localhost:44364/api/Naptar${x.NaptarId}`);
            const data = await response.json();
            let datum = new Date(data.Datum); 
            Object.assign(data, {FoglalasId : x.Id})
            const today = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate());
            if (datum >= today) {
              arr.push(data);

            }
          }
          setBookedDates(arr);
        } catch (error) {
          console.log(error);
        } finally {
          setLoading(false);
        }
      }
    };
  
    fetchBookedDates();
  }, [bookingId]);

  const handleLogout = () => {
    localStorage.clear('user');
    window.location.href = '/login';
  };

  const handlePwdChange = () => {
    navigate('/pwdChange');
  };


  const deleteBooking = (e,id,FoglalasId) => {
    e.preventDefault()
    fetch(`https://localhost:44364/api/Foglalas${FoglalasId}`, {
      method: 'DELETE',
    })
      .then(response => response.json())
      .then(data => setBookedDates(prevDates => prevDates.filter(date => date.FoglalasId !== FoglalasId))
      ,toast.success('Foglalás törölve!')  
    )
      .catch(error => {toast.error('Hiba törlés közben!');
    });
    console.log('Naptar id'+id)
    fetch(`https://localhost:44364/api/Naptar${id}`, {
      method: 'DELETE',
    })
      .then(response => response.json())
      .then(data => {console.log(data)})
      .catch(error => {toast.error('Hiba törlés közben!');
    });

  }

  return (
    <div className="userPage">
      {user ? (
        <div className="userGrid">
          <div className="userpage">
            <h1 className="greetingText">Üdvözöljük, {user.Nev}!</h1>
            <div className="userCard">
              <div className="userInfo">
                <p><strong>Felhasználónév:</strong></p> <p>{user.FelhasznaloNev}</p>
                <p><strong>Email:</strong></p> <p>{user.EMail}</p>
                <p><strong>Telefonszám:</strong></p> <p>{user.TelefonSzam}</p>
              </div>
            </div>
            <div className="buttons">
              <button onClick={handleLogout} className="logoutButton">Kijelentkezés</button>
              <button onClick={handlePwdChange} className="pwdChangeButton">Adat módosítás</button>
            </div>
          </div>
          <div className="bookedDates">
            <h1 className="greetingText">Foglalt időpontok:</h1>
            <div className="dates">
            {loading ? (
              <div className="loading"><img src={spinner} alt="Betöltés..." /></div>
            ) : bookedDates.length ? (
              bookedDates.map((x) => (
               
                <div className="bookedDateRow">
                  <div className='row'>
                  {new Date(x.Datum).toLocaleDateString("hu-HU", {
                    year: 'numeric',
                    month: 'long',
                    day: 'numeric',
                    hour: 'numeric',
                    minute: 'numeric',
                    timeZone: 'Etc/GMT-4',
                  })} | {x.Fodrasz.Nev}
                  
                  <div className='delete' key={x.Id} onClick={(e) => deleteBooking(e, x.Id, x.FoglalasId)}><MdDelete></MdDelete></div></div>
                </div>
              ))
            ) : (
              <div className="noDates">Nincs foglalásod.</div>
            )}
          </div>
          </div>
        </div>
      ) : (
        <div className="loading"><img src={spinner} alt="Betöltés..." /></div>
      )}
    </div>
  );
}

export default UserPage;
