import React, { createContext, useState, useEffect, useContext } from 'react';
import { useIdentityContext } from './IdentityContext';

export interface IProfileImageContext {
    imageUrl: string;
    setImageUrl: React.Dispatch<React.SetStateAction<string>>;
    fetchUserProfileImage: () => Promise<void>;
}

const ProfileImageContext = createContext<IProfileImageContext>({
    imageUrl: '',
    setImageUrl: () => {},
    fetchUserProfileImage: async () => {},
});

export const useProfileImageContextBlock = () => {
    const [imageUrl, setImageUrl] = useState('');
    const { user } = useIdentityContext();

    const fetchUserProfileImage = async () => {
        if (!user || !user.sub) return;

        try {
            const response = await window.api.kaoList.mypages.myPageGetProfileImage({ id: user.sub });
            if (response && response.imageUrl) {
                setImageUrl(response.imageUrl);
            }
        } catch (error) {
            console.error('Error fetching profile image:', error);
        }
    };

    useEffect(() => {
        if (user && user.sub) {
            fetchUserProfileImage();
        }
    }, [user, fetchUserProfileImage]);

    return { imageUrl, setImageUrl, fetchUserProfileImage };
};

export const useProfileImage = () => useContext(ProfileImageContext);

export default ProfileImageContext;
