import React from 'react'
import ReactDOM from 'react-dom/client'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import './index.css' 
import LoginPage from './Pages/login_page/login'

ReactDOM.createRoot(document.getElementById('root')!).render(
    <React.StrictMode>
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<LoginPage />} />
            </Routes>
        </BrowserRouter>
    </React.StrictMode>
)