import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "./CalendarSelect.css";

function CalendarSelect() {
  const navigate = useNavigate();
  const [date, setDate] = useState(new Date());
  const year = date.getFullYear();
  const month = date.getMonth();
  const currentDate = new Date()
  const [selectedDay, setSelectedDay] = useState(null)
  const [selectedTime, setSelectedTime] = useState(null);
  const barber = localStorage.getItem('fodrasz') 
  const [bookedDates, setBookedDates] = useState([])
  const [shiftHours,setShiftHours] = useState([])
  const handleBack = (e)  => {
      e.preventDefault()
      navigate('/booking')
  }

  useEffect(() => {
    fetch(`https://localhost:44364/api/Naptar/GetDates/${barber}`)
      .then(response => response.json())
      .then(data => {
        const formattedDates = data.map(dateString => {
          const date = new Date(dateString.Datum);
          return date.toLocaleTimeString("hu-HU", {
            year: 'numeric',
            month: 'long',
            day: 'numeric',
            hour: 'numeric',
            minute: 'numeric',
            timeZone: 'Etc/GMT-4',
            hour12: false
            
            });
        });
        
        setBookedDates(formattedDates);
      })
  },[barber]);

  useEffect(() =>{
    fetch(`https://localhost:44364/api/beosztas/fodrasz/3`)
    .then(response => response.json())
    .then(data => setShiftHours(data))
    .catch(error => console.log(error))
  })
  const firstDay = new Date(year, month, 1);
  let startDay = firstDay.getDay();


  const daysInMonth = new Date(year, month + 1, 0).getDate();
  const days = [];
  for (let i = 1; i < (startDay === 0 ? 7 : startDay); i++) {
    days.push(<div key={`empty-${i}`} className="day empty disabled"></div>);
  }
  for (let day = 1; day <= daysInMonth; day++) {
    const thisDay = new Date(year, month, day);
    const isSelected = selectedDay && thisDay.toDateString() === selectedDay.toDateString();
    const isPast = thisDay <= new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate());
    const isSunday = thisDay.getDay() === 0
    days.push(
      <div 
        key={day} 
        className={`day ${isSelected ? "select" : ""} ${isPast ? "disabled" : ""} ${isSunday ? "disabled" : ""}`}
        onClick={() => { if (!isPast) handleSelectedDay(thisDay); }}
      >
        {day}
      </div>
    );
  }



  const handleSelectedDay = (day) => {
    setSelectedDay(day);  
    setSelectedTime(null); 
  }

  const handleTimeSelect = (time) => {
    setSelectedTime(time);
  }

  const handleNext = () => {
    if (selectedDay && selectedTime) {
      const bookingDateTime = new Date(selectedDay);
      const [hours, minutes] = selectedTime.split(':');
      bookingDateTime.setHours(parseInt(hours), parseInt(minutes));
      localStorage.setItem('foglalas', bookingDateTime.toISOString());
      navigate('/bookingSummary', {replace: true});
    }
  }

  const generateTimeSlots = () => {
    if (!selectedDay) return [];
  
    const shift = shiftHours.find(shift => {
      const shiftDate = new Date(shift.Datum);
      return shiftDate.toDateString() === selectedDay.toDateString();
    });
  
    let start, end;
  
    if (shift) {
      const startParts = shift.Kezdes.split(":");
      const endParts = shift.Vege.split(":");
  
      start = new Date(selectedDay);
      start.setHours(parseInt(startParts[0]), parseInt(startParts[1]));
  
      end = new Date(selectedDay);
      end.setHours(parseInt(endParts[0]), parseInt(endParts[1]));
    } else {
      start = new Date(selectedDay);
      start.setHours(8, 0);
  
      end = new Date(selectedDay);
      end.setHours(16, 0);
    }
  
    const dates = [];
  
    for (let time = new Date(start); time < end; time.setMinutes(time.getMinutes() + 30)) {
      const hours = time.getHours();
      const minutes = time.getMinutes();
      const timeString = `${hours}:${minutes < 10 ? '0' : ''}${minutes}`;
      const isSelected = selectedTime === timeString;
  
      const selectedDaySplit = selectedDay.toLocaleTimeString("hu-HU", {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
      }).split(' ');
     
      const bookingTime = `${selectedDaySplit[0]} ${selectedDaySplit[1]} ${selectedDaySplit[2]} ${timeString}`;
      console.log(timeString)
      const isBooked = bookedDates.some((booked) => booked === bookingTime);
 
      dates.push(
        <div 
          key={timeString} 
          className={`time ${isSelected ? 'select' : ''} ${isBooked ? 'bookedTime' : ''}`}
          onClick={() => {if (!isBooked) handleTimeSelect(timeString);}}
        >
          <div className={`${isBooked ? 'booked' : 'free'}`}></div>
          {timeString}
        </div>
      );
    }
  
    return dates;
  };
  

  const handlePrevMonth = () => {
    setDate(new Date(year, month - 1, 1));
    setSelectedDay(null)
    setSelectedTime(null)
  };

  const handleNextMonth = () => {
    setDate(new Date(year, month + 1, 1));
    setSelectedDay(null)
    setSelectedTime(null)
  };

  return (
    <div className="calendarSelect">
      <div className="calendar">
   
        
      <div className='current'>
                <p >Város</p>
                <p>Fodrászat</p>
                <p className="now">Foglalás</p>
                <p>Visszaigazolás</p>
            </div>
        <div className="calendarInside">
        <h1 className="date">Időpont</h1>
          <div className="currentMonth">
            <button className="change" onClick={handlePrevMonth}>{"<"}</button>
            <div className="currentMonthText">{date.toLocaleString("hu-HU", { month: "long" })} {year}</div>
            <button className="change" onClick={handleNextMonth}>{">"}</button>
          </div>

          <div className="weekDays">
            <div>H</div>
            <div>K</div>
            <div>Sz</div>
            <div>Cs</div>
            <div>P</div>
            <div>Sz</div>
            <div>V</div>
          </div>
          <div className="days">{days}</div>
        </div>
      </div>

      <div className="dateSelect">
        {selectedDay ? (
          <div className="timeSelection">
            <h2 className="selectedDay">{selectedDay.toLocaleDateString("hu-HU", {
              month: 'long',
              day: 'numeric'
            })}</h2>
            <div className={`timeContainer ${!selectedTime ? "noTime" : ""}`}>
              {generateTimeSlots()}
            </div>
            
            {selectedTime && (
              <div className="selectedDatetime">
                <p>Választott időpont: {selectedDay.toLocaleDateString("hu-HU")} {selectedTime}</p>
                <button className="nextButton" onClick={handleNext}>
                  Tovább
                </button>
                <button className="cancelButton" onClick={handleBack}>
                  Vissza
                </button>
              </div>
            )}
          </div>
        ) : (
          <div className="noDate"><span>Kérjük válasszon egy napot</span>
             <button className="cancelButton" onClick={handleBack}>Vissza</button>
          </div>
        )}
         
      </div>
      
    </div>
  );
}

export default CalendarSelect;