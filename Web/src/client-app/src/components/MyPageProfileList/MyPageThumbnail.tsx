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
    const { imageUrl, setImageUrl } = useProfileImage();
    const handleMouseEnter = () => setHover(true);
    const handleMouseLeave = () => setHover(false);
    const fileInputRef = useRef(null);

    React.useEffect(() => {
        const fetchImage = async () => {
            const result = await window.api.kaoList.mypages.myPageGetProfileImage({ id: id });
            if (result && result.imageUrl) {
                setImageUrl(result.imageUrl);
            }
        }
        fetchImage();
    }, [id, setImageUrl, imageUrl]);

    const handleClick = () => {
        if (fileInputRef.current) {
            (fileInputRef.current as unknown as HTMLInputElement).click();
        }
    }

    const handleImageUpload = async (e: ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files ? e.target.files[0] : null;
        if (!file) return;

        // Check if its' as supported file type.
        const supportedFormats = ['image/jpeg', 'image/png'];
        if (!supportedFormats.includes(file.type)) {
            alert('jpg, png 파일만 업로드 가능합니다.');
            window.location.reload();
            return;
        }

        // Preview changed image.
        const reader = new FileReader();
        reader.onload = (e) => {
            setImageUrl(e.target?.result as string);
        };
        reader.readAsDataURL(file);
        
        // Image uplaod requests.
        const uploadResponse = await window.api.kaoList.mypages.myPageSetProfileImage({ image: file });
        
        // If the upload was successful, set the new image URL to the status.
        if (uploadResponse.statusCode === 200 && uploadResponse.imageUrl) {
            const newImageUrl = uploadResponse.imageUrl + "?t=" + performance.now();
            const imagePreload = new Image();
            imagePreload.src = newImageUrl;
            imagePreload.onload = () => {
                setImageUrl(newImageUrl); 
            };
        }
    }

    return (
        <div className="thumbnail-wrapper" {...rest}>
            <div className="sticky-thumbnail" onClick={handleClick} onMouseEnter={handleMouseEnter} onMouseLeave={handleMouseLeave}>
                {imageUrl ? (
                    <img src={imageUrl} className="thumbnail" alt="User thumbnail" />
                ) : (
                    <LazyCircleProfileIcon className="thumbnail" />
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