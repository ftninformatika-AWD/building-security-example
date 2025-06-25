import axios from 'axios';
import { getGlobalLogout } from '../context/AuthContext';

var Axios = axios.create({
  baseURL: 'http://localhost:5231/api',
  /* other custom settings */
});

/*
Deo koda koji presrece svaki zahtev koji ide ka backend-u, proverava da li postoji jwt token
u local storage-u, i ubacuje ga u header zahteva
*/
Axios.interceptors.request.use(
  function (config) {
    const token = localStorage.getItem('token');
    if (token) {
      config.headers['Authorization'] = `Bearer ${token}`;
    }
    return config;
  },
  function (error) {
    return Promise.reject(error);
  }
)

Axios.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response && error.response.status === 401) {
      // Logika za automatsku odjavu korisnika 
      alert('Not authorized! Please sign in again.')

      setTimeout(() => {
        const logout = getGlobalLogout();
        if (logout) {
          logout(); // Poziva logout iz konteksta
        }
      }, 1000);
      
    }
    return Promise.reject(error);
  }
);

export default Axios;