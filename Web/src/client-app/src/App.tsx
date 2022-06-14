import './App.scss';
import MainLayout from './layouts/MainLayout/MainLayout';
import { Routes, Route, BrowserRouter } from 'react-router-dom';
import HomePage from './pages/HomePage';
import './i18n';
import Page from './pages/Page';
import { Login, LoginActions, Logout, LogoutActions } from './components/identity';

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
      <MainLayout>
        <Routes>
          <Route path='/' element={<HomePage />} />
          <Route path='/chart' >
            <Route path="discover" element={<Page />} />
            <Route path="like" element={<Page />} />
          </Route>
          <Route path='/community' element={<Page />} />
          <Route path={window.authPaths.Login} element={loginAction(LoginActions.Login)} />
          <Route path={window.authPaths.LoginFailed} element={loginAction(LoginActions.LoginFailed)} />
          <Route path={window.authPaths.LoginCallback} element={loginAction(LoginActions.LoginCallback)} />
          <Route path={window.authPaths.Profile} element={loginAction(LoginActions.Profile)} />
          <Route path={window.authPaths.Register} element={loginAction(LoginActions.Register)} />
          <Route path={window.authPaths.LogOut} element={logoutAction(LogoutActions.Logout)} />
          <Route path={window.authPaths.LogOutCallback} element={logoutAction(LogoutActions.LogoutCallback)} />
          <Route path={window.authPaths.LoggedOut} element={logoutAction(LogoutActions.LoggedOut)} />
        </Routes>
      </MainLayout>
    </BrowserRouter>

  );
}

export default App;
