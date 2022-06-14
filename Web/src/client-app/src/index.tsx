
import React from 'react';
import ReactDOM from 'react-dom/client';
import './preload';
import './index.scss';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { applicationPaths } from './api-authorization/ApiAuthorizationConstants';
import authService from './api-authorization/AuthorizeService';

if (window.authPaths === undefined) {
    window.authPaths = applicationPaths;
}

if (window.authService === undefined) {
    window.authService = authService;
}

const root = ReactDOM.createRoot(
    document.getElementById('root') as HTMLElement
);


root.render(
    <React.StrictMode>
        <App />
    </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
