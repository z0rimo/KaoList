import React from "react";
import { useNavigate } from "react-router-dom";
import { useIdentityContext } from "../../contexts/IdentityContext";
import RoutePath from "../../RoutePath";
import LazyCaretDownSolidIcon from "../../svgs/LazyCaretDownSolidIcon";
import LazyCircleProfileIcon from "../../svgs/LazyCircleProfileIcon";
import Dropdown from "../Dropdown";
import SignIn from "../Header/SignIn";
import UserProfileDropdownContent from "./UserProfileDropdownContent";
import { useProfileImage } from "../../contexts/ProfileImageContext";

function UserProfile() {
    const { user } = useIdentityContext();
    const { imageUrl, setImageUrl } = useProfileImage();
    const navigate = useNavigate();
    const handleMyPageClick = React.useCallback(() => {
        navigate(RoutePath['myPage']);
    }, [navigate]);

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

    // Fetch the profile image each time the component is mounted.
    React.useEffect(() => {
        fetchUserProfileImage();
    }, [user, fetchUserProfileImage]);

    if (!user) {
        return <SignIn />
    }

    const userName = user.nickname ?? user.name;

    return (
        <Dropdown className="user-profile-dropdown">
            <span className="user-profile-dropdown-head">
                {imageUrl ? (
                    <img src={imageUrl} alt="User Profile" className="user-profile-img" width={24} height={24} />
                ) : (
                    <LazyCircleProfileIcon />
                )
                }
                <p className="user-name">{userName}</p>
                <LazyCaretDownSolidIcon className="caret-down-icon" />
            </span>
            <UserProfileDropdownContent
                className="user-profile-dropdown-content bottom-right-box-shadow displayed"
                name={user.name}
                nickname={user.nickname}
                onClick={handleMyPageClick}
            />
        </Dropdown>
    )
}

export default React.memo(UserProfile);