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
import SearchPage from './pages/SearchPage/SearchPage';
import SearchContext, { useSearchContext } from './contexts/SearchContext';
import DiscoverChartPage from './pages/ChartPage/DiscoverChartPage/DiscoverChartPage';
import LikedChartPage from './pages/ChartPage/LikedChartPage/LikedChartPage';
import SongDetailPage from './pages/SongDetailPage/SongDetailPage';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href') ?? undefined;

function loginAction(name: string) {
    return (<Login action={name}></Login>);
}

function logoutAction(name: string) {
    return (<Logout action={name}></Logout>);
}

function App() {
    const identityContext = useIdentityContextBlock();
    const searchContext = useSearchContext();
    
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
            <SearchContext.Provider value={searchContext}>
                <BrowserRouter basename={baseUrl}>
                    <Routes>
                        <Route path='/' element={<HomePage />} />
                        <Route path='/chart'>
                            <Route path="discover" element={<DiscoverChartPage />} />
                            <Route path="like" element={<LikedChartPage />} />
                        </Route>
                        <Route path='/customer'>
                            <Route path={RoutePath['terms']} element={<EmptyPage />} />
                            <Route path={RoutePath['policy']} element={<EmptyPage />} />
                            <Route path={RoutePath['inquiry']} element={<EmptyPage />} />
                        </Route>
                        <Route path='/songs'>
                            <Route path={RoutePath['songDetail']} element={<SongDetailPage />} />
                        </Route>
                        <Route path='/community' element={<EmptyPage />} />
                        <Route path={RoutePath['playlist']} element={<PlaylistPage />} />
                        <Route path={RoutePath['search']} element={<SearchPage />} />
                        <Route path={RoutePath['myPage']} element={<EmptyPage />} />
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
            </SearchContext.Provider>
        </IdentityContext.Provider>
    );
}

export default App;
