import React from 'react';

const ApartmentCard = ({ apartment, onEdit, onDelete }) => {

    
    return (
        <div className="apartment-card">
            <div className="apartment-info">
                <h3 className="apartment-address">{apartment.buildingAddress}</h3>
                <p className="info-text">Area: {apartment.area} m²</p>
                <p className="info-text">Rooms: {apartment.numberOfRooms}</p>

                <div className="apartment-actions">
                    <button className="btn edit" onClick={() => onEdit(apartment.id)}>Edit</button>
                    <button className="btn delete" onClick={() => onDelete(apartment.id)}>Delete</button>
                </div>
            </div>
        </div>
    );
};

export default ApartmentCard;

