import axios from 'axios';

var Axios = axios.create({
  baseURL: 'http://localhost:5231/api',
  /* other custom settings */
});

export default Axios;