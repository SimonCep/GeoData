import { useState, createContext, useContext } from 'react';
import { login, register as apiRegister } from '../services/api';
import axios from 'axios';

let AuthContext = createContext();

export function AuthProvider({ children }) {
    let [token, setToken] = useState('');
    let [signedIn, setSignedIn] = useState(false);
    
    let signin = async (email, password, callback) => {
        const token = await login(email, password);
        
        if (token)
        {
            setToken(token);
            setSignedIn(true);
            axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
            callback();
        }
    };

    let signout = (callback) => {
        setToken('');
        setSignedIn(false);
        axios.defaults.headers.common['Authorization'] = '';
        callback();
    };

    let register = async (email, password) => {
        const succeeded = await apiRegister(email, password);
        
        if (succeeded)
        {
            // Do something after successful registration, if desired.
        }
    }

    let value = { signedIn, token, signin, signout, register };

    return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export function useAuth() {
    return useContext(AuthContext);
}
