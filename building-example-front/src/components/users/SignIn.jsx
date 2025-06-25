import React, { useState } from 'react';
import './styles/UserForms.scss';
import { signIn } from '../../services/auth';
import { useNavigate } from 'react-router-dom';
import { useAuth } from './../../context/AuthContext';


const SignIn = () => {

    const [username, setUsername] = useState(null)
    const [password, setPassword] = useState(null)

    const navigate = useNavigate()
    const { login } = useAuth();

    const handleSubmit = async (e) => {
        e.preventDefault();

        const data = {
            username: username,
            password: password
        }

        try {
            const token = await signIn(data);
            login(token);
            navigate('/')
        } catch (error) {
            alert('Unsuccessful sign in - check username/password.')
            console.error(error);
        }
    };

    return (
        <div className="signin-page">
            <form className="signin-form" onSubmit={handleSubmit}>
                <h2>Sign In</h2>
                <label>
                    Username
                    <input type="text" name="username" onChange={(e) => setUsername(e.target.value)} required />
                </label>
                <label>
                    Password
                    <input type="password" name="password" onChange={(e) => setPassword(e.target.value)} required />
                </label>
                <button className="btn" type="submit">Sign in</button>
            </form>
        </div>
    );
};

export default SignIn;
