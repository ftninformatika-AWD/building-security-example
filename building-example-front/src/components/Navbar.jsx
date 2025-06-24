import React from 'react';
import { Link } from 'react-router-dom';
import './styles/Navbar.scss'; 
import { useAuth } from '../context/AuthContext';
import { logout } from '../services/auth';

const Navbar = () => {
    const { isAuthenticated } = useAuth();

    return (
        <nav className="navbar-container">
            <ul className="navbar-links left">
                <li className="navbar-item">
                    <Link to="/buildings" className="navbar-link">Buildings</Link>
                </li>
                <li className="navbar-item">
                    <Link to="/apartments" className="navbar-link">Apartments</Link>
                </li>
                {isAuthenticated && (
                    <li className="navbar-item">
                        <Link to="/apartments/add" className="navbar-link">Add new apartment</Link>
                    </li>
                )}
            </ul>
            <ul className="navbar-links right">
            {!isAuthenticated ? (
                <>
                    <li><Link to="/register" className="navbar-link">Register</Link></li>
                    <li><Link to="/signin" className="navbar-link">Sign in</Link></li>
                </>
                ) : (
                <li><button onClick={logout} className="navbar-link">Logout</button></li>
            )}
            </ul>
        </nav>
    );
};

export default Navbar;
