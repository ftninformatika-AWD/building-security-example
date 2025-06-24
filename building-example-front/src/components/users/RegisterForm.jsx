import React, { useState } from 'react';
import './styles/UserForms.scss';
import { register } from '../../services/auth';
import { useNavigate } from 'react-router-dom';

const RegisterForm = () => {
    const navigate = useNavigate();

    const [formData, setFormData] = useState({
        email: '',
        password: '',
        confirmPassword: '',
        username: '',
        name: '',
        surname: '',
    });

    const [pwError, setPwError] = useState('');

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
        if (pwError) setPwError('');
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (formData.password !== formData.confirmPassword) {
            setPwError("Passwords do not match!");
            return;
        }

        try {
            const { confirmPassword, ...dataToSend } = formData;
            await register(dataToSend);
            alert('Registration successful!')
            setTimeout(() => {
                navigate('/signin');
            }, 2000);
        } catch (error) {
            alert(error)
            console.error(error);
        }
    };

    return (
        <div className="register-page">
            <form className="register-form" onSubmit={handleSubmit}>
                <h2>Register</h2>

                <label>
                    Email
                    <input type="email" name="email" value={formData.email} onChange={handleChange} required />
                </label>

                <label>
                    Username
                    <input type="text" name="username" value={formData.username} onChange={handleChange} required />
                </label>

                <label>
                    Password
                    <input type="password" name="password" value={formData.password} onChange={handleChange} required />
                </label>

                <label>
                    Confirm Password
                    <input type="password" name="confirmPassword" value={formData.confirmPassword} onChange={handleChange} required />
                </label>

                <label>
                    Name
                    <input type="text" name="name" value={formData.name} onChange={handleChange} required />
                </label>

                <label>
                    Surname
                    <input type="text" name="surname" value={formData.surname} onChange={handleChange} required />
                </label>

                {pwError && <p className="error-message">{pwError}</p>}

                <button className="btn" type="submit">Register</button>
            </form>
        </div>
    );
};

export default RegisterForm;
