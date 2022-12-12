import './App.scss';
import './api/kaolistApi.ts';
import { Routes, Route, BrowserRouter } from 'react-router-dom';
import HomePage from './pages/HomePage';
import './i18n';
import { Login, LoginActions, Logout, LogoutActions } from './components/identity';
import PlaylistPage from './pages/PlaylistPage';
import EmptyPage from './pages/EmptyPage';
import RoutePath from './RoutePath';
import IdentityContext, { useIdentityContextBlock } from './contexts/IdentityContext';
import React from 'react';
import authService from './api-authorization/AuthorizeService';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href') ?? undefined;

function loginAction(name: string) {
    return (<Login action={name}></Login>);
}

function logoutAction(name: string) {
    return (<Logout action={name}></Logout>);
}

function App() {
    const identityContext = useIdentityContextBlock();
    const updateUserIdentity = React.useCallback(() => {
        authService.getUser()
            .then(user => {
                if (user === identityContext.user) {
                    return;
                }

                identityContext.setUser(user)
            }).catch(console.error)
    }, [identityContext]);

    React.useEffect(() => {
        (async () => {
            if (!await authService.isAuthenticated()) {
                return;
            }

            updateUserIdentity();
        })();
    }, [updateUserIdentity])

    React.useEffect(() => {
        let id = authService.subscribe(updateUserIdentity);
        return () => {
            authService.unsubscribe(id);
        }
    }, [updateUserIdentity]);

    return (
        <IdentityContext.Provider value={identityContext}>
            <BrowserRouter basename={baseUrl}>
                <Routes>
                    <Route path='/' element={<HomePage />} />
                    <Route path='/chart' >
                        <Route path="discover" element={<EmptyPage />} />
                        <Route path="like" element={<EmptyPage />} />
                    </Route>
                    <Route path='/community' element={<EmptyPage />} />
                    <Route path={RoutePath['playlist']} element={<PlaylistPage />} />
                    <Route path={RoutePath['search']} element={<EmptyPage />} /> 
                    <Route path={window.authPaths.Login} element={loginAction(LoginActions.Login)} />
                    <Route path={window.authPaths.LoginFailed} element={loginAction(LoginActions.LoginFailed)} />
                    <Route path={window.authPaths.LoginCallback} element={loginAction(LoginActions.LoginCallback)} />
                    <Route path={window.authPaths.Profile} element={loginAction(LoginActions.Profile)} />
                    <Route path={window.authPaths.Register} element={loginAction(LoginActions.Register)} />
                    <Route path={window.authPaths.LogOut} element={logoutAction(LogoutActions.Logout)} />
                    <Route path={window.authPaths.LogOutCallback} element={logoutAction(LogoutActions.LogoutCallback)} />
                    <Route path={window.authPaths.LoggedOut} element={logoutAction(LogoutActions.LoggedOut)} />
                </Routes>
            </BrowserRouter>
        </IdentityContext.Provider>
    );
}

export default App;
