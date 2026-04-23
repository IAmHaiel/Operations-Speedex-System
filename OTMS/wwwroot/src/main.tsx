import React from 'react'
import ReactDOM from 'react-dom/client'
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom'
import './index.css'
import LoginPage from './Pages/login_page/login'
import Dashboard from './Pages/SystemAdmin_Dashboard/SystemAdmin_Dashboard'
import ForgotPasswordPage from './Pages/forgotpassword_page/forgotpassword_page';

const PrivateRoute = ({ children }: { children: React.ReactNode }) => {
    const token = localStorage.getItem('authToken');
    return token ? <>{children}</> : <Navigate to="/" replace />;
};

ReactDOM.createRoot(document.getElementById('root')!).render(
    <React.StrictMode>
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<LoginPage />} />

                <Route path="/forgotpassword_page" element={<ForgotPasswordPage />} />

                <Route path="/SystemAdmin_Dashboard" element={
                    <PrivateRoute>
                        <Dashboard />
                    </PrivateRoute>
                } />
            </Routes>
        </BrowserRouter>
    </React.StrictMode>
)