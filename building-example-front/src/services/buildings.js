import Axios from "../apis/Axios";

export const fetchBuildings = async () => {
    try {
        const response = await Axios.get('/Buildings');
        return response.data;
    } catch (error) {
        throw new Error('Failed to fetch buildings');
    }
};