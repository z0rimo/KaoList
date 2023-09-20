import { useState, useMemo, createContext, useContext } from 'react';

export interface IProfileImageContext {
    imageUrl: string | null;
    setImageUrl: (imageUrl: string | null) => void;
}

export function useProfileImageContextBlock() {
    const [imageUrl, setImageUrl] = useState<string | null>(null);

    return useMemo<IProfileImageContext>(() => ({
        imageUrl: imageUrl,
        setImageUrl: setImageUrl,
    }), [imageUrl]);
}

const ProfileImageContext = createContext<IProfileImageContext>({
    imageUrl: null,
    setImageUrl: () => { },
});

export function useProfileImage() {
    return useContext(ProfileImageContext);
}

export default ProfileImageContext;
