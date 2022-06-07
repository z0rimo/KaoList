import React from 'react';
import { Route } from 'react-router';
import Home from './components/Home';
import { Routes } from 'react-router-dom';

function App() {
    return (
        <Routes>
            <Route path='/' element={<Home />} />
        </Routes>
    )
}

export default React.memo(App);