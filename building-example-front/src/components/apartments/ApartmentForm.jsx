import React, { useState, useEffect } from 'react';
import './styles/AppartmentForm.scss';
import { fetchBuildings } from '../../services/buildings';

const ApartmentForm = ({ initialData = null, onSubmit, submitting = false }) => {
    const [formData, setFormData] = useState({
        apartmentNumber: '',
        floor: 0,
        area: 0,
        numberOfRooms: 1,
        buildingId: ''
    });

    const [buildings, setBuildings] = useState([]);
    const [fetchingBuildings, setFetchingBuildings] = useState(true);

    const isEditing = Boolean(initialData);

    // Ako imamo podatke za edit, setuj formu
    useEffect(() => {
        if (initialData) {
            setFormData({ ...initialData });
        }
    }, [initialData]);


    useEffect(() => {
        const loadBuildings = async () => {
            try {
                const data = await fetchBuildings(); 
                setBuildings(data);
            } catch (error) {
                console.error(error);
            } finally {
                setFetchingBuildings(false);
            }
        };

        loadBuildings();
    }, []);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: ['floor', 'area', 'numberOfRooms', 'buildingId'].includes(name)
                ? Number(value)
                : value
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (onSubmit) {
            await onSubmit(formData);
        }
    };

    return (
        <form className="apartment-form" onSubmit={handleSubmit}>
            <h2>{isEditing ? 'Edit Apartment' : 'Create New Apartment'}</h2>

            <label>
                Apartment Number:
                <input
                    type="text"
                    name="apartmentNumber"
                    value={formData.apartmentNumber}
                    onChange={handleChange}
                    required
                />
            </label>

            <label>
                Floor:
                <input
                    type="number"
                    name="floor"
                    value={formData.floor}
                    onChange={handleChange}
                    min="0"
                />
            </label>

            <label>
                Area (m²):
                <input
                    type="number"
                    name="area"
                    value={formData.area}
                    onChange={handleChange}
                    min="0"
                />
            </label>

            <label>
                Number of Rooms:
                <input
                    type="number"
                    name="numberOfRooms"
                    value={formData.numberOfRooms}
                    onChange={handleChange}
                    min="1"
                />
            </label>

            <label>
                Building:
                {fetchingBuildings ? (
                    <div className="loading-container">
                        <div className="loading-spinner"></div>
                        <p>Loading buildings...</p>
                    </div>
                ) : (
                    <select
                        name="buildingId"
                        value={formData.buildingId}
                        onChange={handleChange}
                        required
                    >
                        <option value="" disabled>Select a building</option>
                        {buildings.map(b => (
                            <option key={b.id} value={b.id}>
                                {b.name ?? b.address ?? `Building #${b.id}`}
                            </option>
                        ))}
                    </select>
                )}
            </label>

            <button className="btn edit" type="submit" disabled={submitting}>
                {submitting ? (isEditing ? 'Saving...' : 'Creating...') : (isEditing ? 'Save Changes' : 'Create Apartment')}
            </button>
        </form>
    );
};

export default ApartmentForm;
