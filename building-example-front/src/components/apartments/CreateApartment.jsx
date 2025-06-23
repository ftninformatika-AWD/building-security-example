import React, { useState } from 'react';
import { createApartment } from '../../services/apartments';
import ApartmentForm from './ApartmentForm';
import { useNavigate } from 'react-router-dom';

const CreateApartment = () => {
    const [loading, setLoading] = useState(false);

    const navigate = useNavigate()

    const handleCreate = async (data) => {
        setLoading(true);
        try {
            await createApartment(data);
            navigate('/apartments')
        } catch (error) {
            console.error(error);
        } finally {
            setLoading(false);
        }
    };

    return (
        <ApartmentForm
            onSubmit={handleCreate}
            submitting={loading}
        />
    );
};

export default CreateApartment;
