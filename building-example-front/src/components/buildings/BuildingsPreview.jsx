import React, { useEffect, useState } from "react";
import { fetchBuildings } from "../../services/buildings";
import BuildingCard from "./BuildingCard";
import './styles/BuildingPreview.scss';


const BuildingsPreview = () => {

    const [buildings, setBuildings] = useState([])
    const [loading, setLoading] = useState(true)

    useEffect(() => {
        const loadBuildings = async () => {
            setLoading(true);
            try {
                const data = await fetchBuildings();
                console.log(data)
                setBuildings(data);
            } catch (err) {
                console.error(err.message);
            } finally {
                setLoading(false); // Bez obzira na uspeh ili grešku, loading se postavlja na false
            }
        };

        loadBuildings();
    }, []);

    return (
        <div>
            {loading ? (
                <div className="loading-container">
                    <div className="loading-spinner"></div>
                    <p>Loading buildings...</p>
                </div>
            ) : (
                <div className="preview-wrapper">
                    {buildings.map(building => (
                        <BuildingCard building={building} key={building.id}/>
                    ))}
                </div>
            )}
        </div>
    );

}

export default BuildingsPreview
