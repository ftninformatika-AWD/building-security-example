import Axios from "../apis/Axios";

export const fetchApartments = async () => {
    try {
        const response = await Axios.get('/Apartments');
        return response.data;
    } catch (error) {
        throw new Error('Failed to fetch apartments');
    }
};

export const fetchApartmentById = async (id) => {
    try {
        const response = await Axios.get('/Apartments/' + id);
        return response.data;
    } catch (error) {
        throw new Error('Failed to fetch apartment by id');
    }
};

export const deleteApartment = async (id) => {
    try {
        const response = await Axios.delete('/Apartments/' + id);
        return response.data;
    } catch (error) {
        throw new Error('Failed to delete apartment');
    }
};

export const createApartment = async (data) => {
    try {
        const response = await Axios.post('/Apartments', data);
        return response.data;
    } catch (error) {
        throw new Error('Failed to create an apartment');
    }
};

export const updateApartment = async (id, data) => {
    try {
        const response = await Axios.put('/Apartments/' + id, data);
        return response.data;
    } catch (error) {
        throw new Error('Failed to update an apartment');
    }
};

