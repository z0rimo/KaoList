export default interface IApplicationAuthPaths {
  ApiAuthorizationClientConfigurationUrl: string;
  ApiAuthorizationPrefix: string;
  Login: string;
  LoginFailed: string;
  LoginCallback: string;
  Register: string;
  Profile: string;
  LogOut: string;
  LoggedOut: string;
  LogOutCallback: string;
  IdentityRegisterPath: string;
  IdentityManagePath: string;
}


declare global {
  interface Window {
    authPaths: IApplicationAuthPaths;
  }
}