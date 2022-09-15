import { Profile } from "oidc-client";
import React from "react";

export interface IIdentityContext {
    user: Profile | null;
    setUser: (user: Profile | null) => void;
}

export function useIdentityContextBlock() {
    const [user, setUser] = React.useState<Profile | null>(null);

    return React.useMemo<IIdentityContext>(() => ({
        user: user,
        setUser: setUser,
    }), [user]);
}

const IdentityContext = React.createContext<IIdentityContext>({
    user: null,
    setUser: () => { },
});

export function useIdentityContext() {
    return React.useContext(IdentityContext);
}

export default IdentityContext;
