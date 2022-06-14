import { Profile, User } from 'oidc-client';

interface IAuthenticationResultStatus {
    Redirect: 'redirect';
    Success: 'success';
    Fail: 'fail';
};

export const AuthenticationResultStatus: IAuthenticationResultStatus = {
    Redirect: 'redirect',
    Success: 'success',
    Fail: 'fail'
};

interface IAuthSuccessResponse {
    status: typeof AuthenticationResultStatus['Success'];
    state: any;
}

interface IAuthErrorResponse {
    status: typeof AuthenticationResultStatus['Fail'];
    message: string;
}

interface IAuthRedirectResponse {
    status: typeof AuthenticationResultStatus['Redirect'];
}

type AuthResponse = IAuthSuccessResponse | IAuthErrorResponse | IAuthRedirectResponse

export interface IAuthorizeService {
    isAuthenticated(): Promise<boolean>;
    getUser(): Promise<Profile | null>;
    getAccessToken(): Promise<string | null>;
    signIn(state: any): Promise<AuthResponse>;
    completeSignIn(url?: string | undefined): Promise<AuthResponse>;
    signOut(state: any): Promise<AuthResponse>;
    completeSignOut(url?: string | undefined): Promise<AuthResponse>;
    updateState(user?: User | null | undefined): void;
    subscribe(callback: () => void): number;
    unsubscribe(subscriptionId: number): void;
    notifySubscribers(): void;
    createArguments<T>(state?: T): { useReplaceToNavigate: true, data: T };
    error<T>(message: T): { status: typeof AuthenticationResultStatus['Fail'], message: T };
    success<T>(state?: T): { status: typeof AuthenticationResultStatus['Success'], state: T };
    redirect(): { status: typeof AuthenticationResultStatus['Redirect'] };
    ensureUserManagerInitialized(): Promise<void>;
}



declare global {
    interface Window {
        authService: IAuthorizeService;
    }
}

