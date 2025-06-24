import React from 'react';
import { useAuth } from '../../context/AuthContext';

const ApartmentCard = ({ apartment, onEdit, onDelete }) => {

    const { isAuthenticated, role } = useAuth();
    
    return (
        <div className="apartment-card">
            <div className="apartment-info">
                <h3 className="apartment-address">{apartment.buildingAddress}</h3>
                <p className="info-text">Area: {apartment.area} m²</p>
                <p className="info-text">Rooms: {apartment.numberOfRooms}</p>

                <div className="apartment-actions">
                    { /* s obzirom da su administrator i seller jedine dve role ulogovanih korisnika, uslov je mogao
                        da bude i samo isAuthenticated */}
                    {isAuthenticated && (role.toLowerCase() === 'administrator' || role.toLowerCase() === 'seller') && (
                        <button className="btn edit" onClick={() => onEdit(apartment.id)}>Edit</button>
                    )}
                    
                    {isAuthenticated && role.toLowerCase() === 'administrator' && (
                        <button className="btn delete" onClick={() => onDelete(apartment.id)}>Delete</button>
                    )}
                </div>
            </div>
        </div>
    );
};

export default ApartmentCard;

