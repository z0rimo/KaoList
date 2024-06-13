import './App.scss';
import './api/kaolistApi.ts';
import { Routes, Route, BrowserRouter } from 'react-router-dom';
import HomePage from './pages/HomePage';
import './i18n';
import { Login, LoginActions, Logout, LogoutActions } from './components/identity';
import EmptyPage from './pages/EmptyPage';
import RoutePath from './RoutePath';
import IdentityContext, { useIdentityContextBlock } from './contexts/IdentityContext';
import React from 'react';
import authService from './api-authorization/AuthorizeService';
import SearchPage from './pages/SearchPage/SearchPage';
import SearchContext, { useSearchContext } from './contexts/SearchContext';
import DiscoverChartPage from './pages/ChartPages/DiscoverChartPage/DiscoverChartPage';
import LikedChartPage from './pages/ChartPages/LikedChartPage/LikedChartPage';
import SongDetailPage from './pages/SongDetailPage/SongDetailPage';
import MyPage from './pages/MyPage';
import ProfileImageContext, { useProfileImageContextBlock } from './contexts/ProfileImageContext';
import TotalSearchPage from './pages/TotalSearchPage';
import NotFoundPage from './pages/ErrorPages/NotFoundPage';

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
    const profileImageContext = useProfileImageContextBlock();

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
    }, [updateUserIdentity]);

    React.useEffect(() => {
        let subscriptionId = authService.subscribe(updateUserIdentity);
        return () => {
            authService.unsubscribe(subscriptionId);
        };
    }, [updateUserIdentity]);

    return (
        <IdentityContext.Provider value={identityContext}>
            <SearchContext.Provider value={searchContext}>
                <ProfileImageContext.Provider value={profileImageContext}>
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
                            <Route path={RoutePath['totalSearch']} element={<TotalSearchPage />} />
                            <Route path={RoutePath['search']} element={<SearchPage />} />
                            <Route path={RoutePath['myPage']} element={<MyPage />} />
                            <Route path={window.authPaths.Login} element={loginAction(LoginActions.Login)} />
                            <Route path={window.authPaths.LoginFailed} element={loginAction(LoginActions.LoginFailed)} />
                            <Route path={window.authPaths.LoginCallback} element={loginAction(LoginActions.LoginCallback)} />
                            <Route path={window.authPaths.Profile} element={loginAction(LoginActions.Profile)} />
                            <Route path={window.authPaths.Register} element={loginAction(LoginActions.Register)} />
                            <Route path={window.authPaths.LogOut} element={logoutAction(LogoutActions.Logout)} />
                            <Route path={window.authPaths.LogOutCallback} element={logoutAction(LogoutActions.LogoutCallback)} />
                            <Route path={window.authPaths.LoggedOut} element={logoutAction(LogoutActions.LoggedOut)} />
                            <Route path="*" element={<NotFoundPage />}></Route>
                        </Routes>
                    </BrowserRouter>
                </ProfileImageContext.Provider>
            </SearchContext.Provider>
        </IdentityContext.Provider>
    );
}

export default App;
