import React, { ChangeEvent, useRef } from "react";
import LazyCircleProfileIcon from "../../svgs/LazyCircleProfileIcon";
import LazyCircleCameraIcon from "../../svgs/LazyCircleCameraIcon";
import { useProfileImage } from '../../contexts/ProfileImageContext';

type MyPageThumbnailProps = {
    id?: string;
} & React.HTMLAttributes<HTMLDivElement>;

function MyPageThumbnail(props: MyPageThumbnailProps) {
    const { id, ...rest } = props;
    const [hover, setHover] = React.useState(false);
    const handleMouseEnter = () => setHover(true);
    const handleMouseLeave = () => setHover(false);
    const { imageUrl, setImageUrl } = useProfileImage();
    const fileInputRef = useRef(null);

    React.useEffect(() => {
        const fetchImage = async () => {
            const result = await window.api.kaoList.mypages.myPageGetProfileImage({ id: id });
            if (result && result.imageUrl) {
                setImageUrl(result.imageUrl);
            }
        }
        fetchImage();
    }, [id, setImageUrl]);

    const handleClick = () => {
        if (fileInputRef.current) {
            (fileInputRef.current as unknown as HTMLInputElement).click();
        }
    }

    const handleImageUpload = async (e: ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files ? e.target.files[0] : null;
        if (!file) return;

        await window.api.kaoList.mypages.myPageSetProfileImage({ image: file });
    }

    return (
        <div className="thumbnail-wrapper" {...rest}>
            <div onClick={handleClick} onMouseEnter={handleMouseEnter} onMouseLeave={handleMouseLeave}>
                {imageUrl ? (
                    <img src={imageUrl} className="thumbnail" alt="User thumbnail" />
                ) : (
                    <LazyCircleProfileIcon className="thumbnail" style={{cursor: "pointer"}} />
                )}
                <button className="thumbnail-change">
                    <input
                        type="file"
                        style={{ display: "none" }}
                        onChange={handleImageUpload}
                        ref={fileInputRef}
                    />
                    <LazyCircleCameraIcon className="rounded-circle circle-camera" />
                </button>
            </div>
            {hover && <p className="notification-image">프로필 이미지는 2MB. 확장자는 JPG, PNG만 가능합니다.</p>}
        </div>
    )
}

export default React.memo(MyPageThumbnail);