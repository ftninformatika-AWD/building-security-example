import React, { useState } from "react";
import { useEffect } from "react";
import { deleteApartment, fetchApartments } from "../../services/apartments";
import ApartmentCard from "./ApartmentCard";
import { useNavigate } from "react-router-dom";

const ApartmentsPreview = () => {

    const [apartments, setApartments] = useState([])
    const [loading, setLoading] = useState(true)

    const navigate = useNavigate()

    useEffect(() => {
        const loadApartments = async () => {
            setLoading(true);
            try {
                const data = await fetchApartments();
                console.log(data)
                setApartments(data);
            } catch (err) {
                console.error(err.message);
            } finally {
                setLoading(false); // Bez obzira na uspeh ili grešku, loading se postavlja na false
            }
        };

        loadApartments();
    }, []);

    const handleEdit = (id) => {
        navigate('/apartments/' + id)
    };
    
    const handleDelete = async (id) => {
        try {
            await deleteApartment(id);
            // Ukloni apartman lokalno nakon uspešnog brisanja
            setApartments(prev => prev.filter(apartment => apartment.id !== id));
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <div>
            {loading ? (
                <div className="loading-container">
                    <div className="loading-spinner"></div>
                    <p>Loading apartments...</p>
                </div>
            ) : (
                <div className="preview-wrapper">
                    {apartments.map(apartment => (
                        <ApartmentCard key={apartment.id} apartment={apartment} 
                            onEdit={handleEdit} onDelete={handleDelete}/>
                    ))}
                </div>
            )}
        </div>
    );
}

export default ApartmentsPreview
