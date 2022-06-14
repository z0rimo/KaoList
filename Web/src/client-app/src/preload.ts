import authService from './api-authorization/AuthorizeService';
import { applicationPaths } from "./api-authorization/ApiAuthorizationConstants";

window.authService = authService;
window.authPaths = applicationPaths;