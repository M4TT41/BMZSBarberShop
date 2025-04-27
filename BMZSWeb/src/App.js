import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import NavBar from './components/NavBar/NavBar';
import Main from './components/Main/Main';
import Footer from './components/Footer/Footer';
import Gallery from './components/Gallery/Gallery';
import Registration from './components/User/Registration';
import Login from './components/User/LogIn';
import UserPage from './components/UserPage/UserPage';
import City from './components/Booking/City'
import BookingSummary from './components/Booking/BookingSummary';
import './App.css';
import CalendarSelect from './components/Booking/CalendarSelect';
import Barbershop from './components/Booking/Barbershop';
import PwdChange from './components/UserPage/PwdChange';
import { ToastContainer } from 'react-toastify';

function App() {
  return (
    <div>
    <Router>
      <div>
     
        <NavBar />
        <Routes>
          <Route path="/" element={<Main />} />
          <Route path="/registration" element={<Registration />} />
          <Route path="/gallery" element={<Gallery />} />
          <Route path="/login" element={<Login />} />
          <Route path="/userpage" element={<UserPage />} />
          <Route path="/booking" element={<Barbershop/>} />
          <Route path='/city' element={<City/>} />
          <Route path="/calendar" element={<CalendarSelect />} />
          <Route path="/bookingSummary" element={<BookingSummary />} />
          <Route path="/pwdChange" element={<PwdChange />} />
        </Routes>
        <Footer />
      </div>
    
    </Router>
    <ToastContainer
                position="top-right"
                autoClose={3000}
                hideProgressBar={false}
                newestOnTop={false}
                closeOnClick
                pauseOnHover
                draggable
            />
    </div>
  );
}

export default App;
