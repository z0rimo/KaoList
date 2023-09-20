import React, { ChangeEvent, useRef } from "react";
import LazyCircleProfileIcon from "../../svgs/LazyCircleProfileIcon";
import LazyCircleCameraIcon from "../../svgs/LazyCircleCameraIcon";

type MyPageThumbnailProps = {
    id?: string;
} & React.HTMLAttributes<HTMLDivElement>;

function MyPageThumbnail(props: MyPageThumbnailProps) {
    const { id, ...rest } = props;

    const [imageUrl, setImageUrl] = React.useState<string | null>(null);
    const fileInputRef = useRef(null);

    React.useEffect(() => {
        const fetchImage = async () => {
            const result = await window.api.kaoList.mypages.myPageGetProfileImage({ id: id });
            if (result && result.imageUrl) {
                setImageUrl(result.imageUrl);
            }
        }
        fetchImage();
    }, [props.id]);

    const handleClick = () => {
        console.log("handleClick called");
        if (fileInputRef.current) {
            (fileInputRef.current as unknown as HTMLInputElement).click();
        }
    }

    const handleImageUpload = async (e: ChangeEvent<HTMLInputElement>) => {
        console.log("handleImageUpload called");
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