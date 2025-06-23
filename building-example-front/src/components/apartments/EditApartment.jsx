import React, { useState, useEffect } from 'react';
import { fetchApartmentById, updateApartment } from '../../services/apartments';
import ApartmentForm from './ApartmentForm';
import { useNavigate, useParams } from 'react-router-dom';

const EditApartment = () => {
    const [initialData, setInitialData] = useState(null);
    const [loading, setLoading] = useState(true);
    const [saving, setSaving] = useState(false);

    const navigate = useNavigate()
    const params = useParams()
    const apartmentId = params.id

    useEffect(() => {
        const loadApartment = async () => {
            try {
                const data = await fetchApartmentById(apartmentId);
                setInitialData(data);
            } catch (err) {
                console.error('Failed to fetch apartment', err);
            } finally {
                setLoading(false);
            }
        };

        loadApartment();
    }, [apartmentId]);

    const handleUpdate = async (updatedData) => {
        setSaving(true);
        try {
            await updateApartment(apartmentId, updatedData);
            navigate('/apartments')
        } catch (error) {
            console.error('Update failed', error);
        } finally {
            setSaving(false);
        }
    };

    if (loading) {
        return (
            <div className="loading-container">
                <div className="loading-spinner" />
                <p>Loading apartment data...</p>
            </div>
        );
    }

    return (
        <ApartmentForm
            initialData={initialData}
            onSubmit={handleUpdate}
            submitting={saving}
        />
    );
};

export default EditApartment;
