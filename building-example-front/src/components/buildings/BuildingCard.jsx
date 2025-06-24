import React from 'react';
import './styles/BuildingCard.scss';

const BuildingCard = ({ building }) => {
    return (
        <div className="building-card">
            <div className="building-info">
                <h3 className="building-address">{building.address}</h3>
                <p className="info-text">Floors: {building.floors}</p>
                <p className="info-text">
                    Elevator: {building.hasElevator ? 'Yes' : 'No'}
                </p>
                <p className="info-text">Built in: {building.yearOfConstruction}</p>
            </div>
        </div>
    );
};

export default BuildingCard;
