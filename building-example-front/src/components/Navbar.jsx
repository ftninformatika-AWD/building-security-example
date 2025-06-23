import React from 'react';
import { Link } from 'react-router-dom';
import './styles/Navbar.scss'; 

const Navbar = () => {
    return (
        <nav className="navbar-container">
            <ul className="navbar-links">
                <li className="navbar-item">
                    <Link to="/buildings" className="navbar-link">Buildings</Link>
                </li>
                <li className="navbar-item">
                    <Link to="/apartments" className="navbar-link">Apartments</Link>
                </li>
                <li className="navbar-item">
                    <Link to="/apartments/add" className="navbar-link">Add new apartment</Link>
                </li>
            </ul>
        </nav>
    );
};

export default Navbar;