import axios from 'axios';

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

export default Axios;