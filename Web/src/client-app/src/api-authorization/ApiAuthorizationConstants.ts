export const applicationName = 'CodeRabbits.KaoList.Web';

export const logoutActions = {
  LogoutCallback: 'logout-callback',
  Logout: 'logout',
  LoggedOut: 'logged-out'
};

export const loginActions = {
  Login: 'login',
  LoginCallback: 'login-callback',
  LoginFailed: 'login-failed',
  Profile: 'profile',
  Register: 'register'
};

const prefix = '/authentication';

export const applicationPaths = {
  DefaultLoginRedirectPath: '/',
  ApiAuthorizationClientConfigurationUrl: `_configuration/${applicationName}`,
  ApiAuthorizationPrefix: prefix,
  Login: `${prefix}/${loginActions.Login}`,
  LoginFailed: `${prefix}/${loginActions.LoginFailed}`,
  LoginCallback: `${prefix}/${loginActions.LoginCallback}`,
  Register: `${prefix}/${loginActions.Register}`,
  Profile: `${prefix}/${loginActions.Profile}`,
  LogOut: `${prefix}/${logoutActions.Logout}`,
  LoggedOut: `${prefix}/${logoutActions.LoggedOut}`,
  LogOutCallback: `${prefix}/${logoutActions.LogoutCallback}`,
  IdentityRegisterPath: 'Identity/Account/Register',
  IdentityManagePath: 'Identity/Account/Manage'
};
