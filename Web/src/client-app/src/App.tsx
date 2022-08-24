import './App.scss';
import { Routes, Route, BrowserRouter } from 'react-router-dom';
import HomePage from './pages/HomePage';
import './i18n';
import { Login, LoginActions, Logout, LogoutActions } from './components/identity';
import EmptyPage from './pages/EmptyPage';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href') ?? undefined;

function loginAction(name: string) {
  return (<Login action={name}></Login>);
}

function logoutAction(name: string) {
  return (<Logout action={name}></Logout>);
}

function App() {
  return (
    <BrowserRouter basename={baseUrl}>
        <Routes>
          <Route path='/' element={<HomePage />} />
          <Route path='/chart' >
            <Route path="discover" element={<EmptyPage />} />
            <Route path="like" element={<EmptyPage />} />
          </Route>
          <Route path='/community' element={<EmptyPage />} />
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
  );
}

export default App;