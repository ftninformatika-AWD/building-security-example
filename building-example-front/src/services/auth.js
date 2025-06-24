import Axios from "../apis/Axios";

export const signIn = async (data) => {
    try {
        const response = await Axios.post('/Auth/login', data);
        return response.data;
    } catch (error) {
        console.error(error);
        throw new Error('Failed to sign in');
    }
};

export const register = async (data) => {
    try {
        const response = await Axios.post('/Auth/register', data);
        return response.data;
    } catch (error) {
        console.error(error);
        throw new Error(error.response.data.error);
    }
};

export const logout = () => {
    localStorage.removeItem("token")
    location.replace("http://localhost:5173")
}