import React, { ChangeEvent, useRef } from "react";
import LazyCircleProfileIcon from "../../svgs/LazyCircleProfileIcon";
import LazyCircleCameraIcon from "../../svgs/LazyCircleCameraIcon";
import { useProfileImage } from '../../contexts/ProfileImageContext';

type MyPageThumbnailProps = {
    id?: string;
} & React.HTMLAttributes<HTMLDivElement>;

function MyPageThumbnail(props: MyPageThumbnailProps) {
    const { id, ...rest } = props;

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
            {imageUrl ? (
                <img src={imageUrl} className="thumbnail" alt="User thumbnail" />
            ) : (
                <LazyCircleProfileIcon className="thumbnail" />
            )}
            <button className="thumbnail-change" onClick={handleClick}>
                <input
                    type="file"
                    style={{ display: "none" }}
                    onChange={handleImageUpload}
                    ref={fileInputRef}
                />
                <LazyCircleCameraIcon className="rounded-circle circle-camera" />
            </button>
        </div>
    )
}

export default React.memo(MyPageThumbnail);