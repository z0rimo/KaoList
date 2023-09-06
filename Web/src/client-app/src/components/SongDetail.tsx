import React from "react";
import { ISongSnippet } from "../api/kaolistApi";
import { useLocation } from "react-router-dom";
import StringHelper from "../StringHelper";
import { useTranslation } from "react-i18next";
import { SongRating } from "../enums/SongRating";
import ClassNameHelper from "../ClassNameHelper";
import { Link } from "react-router-dom";
import LazyStarIcon from "../svgs/LazyStarIcon";
import LazyStarSolidIcon from "../svgs/LazyStarSolidIcon";

export interface ISongDetailItem extends ISongSnippet {
    id: string;
    nickname?: string;
    rating?: SongRating;
}

type SongDetailItemProps = {
    url?: string;
    title?: string;
    nickname?: string;
}

function SongDetail(props: SongDetailItemProps) {
    const [item, setItem] = React.useState<ISongDetailItem>();
    const [isFollowed, setIsFollowed] = React.useState<boolean>(false);
    const [isBlinded, setIsBlinded] = React.useState<boolean>(false);
    const { url, title, nickname, ...rest } = props;
    const { t } = useTranslation('Chart');
    const location = useLocation();

    React.useEffect(() => {
        const fetchData = async () => {
            try {
                const params = new URLSearchParams(location.search);
                const id = params.get('id');
                if (id) {
                    const response = await window.api.kaoList.songs.songDetail({ id });
                    if (response.item) {
                        setItem({
                            id: response.item.id ?? '',
                            title: response.item.snippet?.title ?? '',
                            nickname: response.item.snippet?.songUsers?.[0]?.nickname ?? '',
                            rating: response.item.rating,
                            ...response.item.snippet,
                        });
                    }

                    const ratingResponse = await window.api.kaoList.songs.songGetRating({ ids: [id]});
                    console.log("fetched data:", ratingResponse);
                    const songRating = ratingResponse.resources[0]?.rating;
                    if (songRating) {
                        const songRatingEnum = SongRating[songRating];
                        setIsFollowed(songRatingEnum === "Follow");
                        setIsBlinded(songRatingEnum === "Blind");
                    }
                }
            } catch (error) {
                console.error("Error fetching song details:", error);
            }
        };

        fetchData();
    }, [location.search, item?.id]);

    const handleFollowClick = async () => {
        const newStatus = !isFollowed;
        setIsFollowed(newStatus);
        setIsBlinded(false);

        const rate = newStatus ? SongRating.Follow : SongRating.None;
        await window.api.kaoList.songs.songRate({ songId: item?.id ?? '', rate })
    }

    const handleBlindClick = async () => {
        const newStatus = !isBlinded;
        setIsBlinded(newStatus);
        setIsFollowed(false);

        const rate = newStatus ? SongRating.Blind : SongRating.None;
        await window.api.kaoList.songs.songRate({ songId: item?.id ?? '', rate })
    }

    return (
        <div className="song-detail" {...rest}>
            <Link to="https://youtu.be/{해당아이디}">
                <img src="https://i.ytimg.com/vi/{밸류}/default.jpg" alt={StringHelper.format(t('Thumbnail of {0}'), item?.title)} />
            </Link>
            <div className="song-detail-info-wrapper">
                <div className="song-detail-info">
                    <p className="title">{item?.title}</p>
                    <div className="button-group">
                        <button className={ClassNameHelper.concat('btn-primary', isFollowed && "follow")} onClick={handleFollowClick}>{t('Follow')}</button>
                        <button className={ClassNameHelper.concat('btn-primary', isBlinded && "blind")} onClick={handleBlindClick}>{t('Blind')}</button>
                        <p>{t('Artist')} · {item?.nickname}</p>
                    </div>
                </div>
                <button className="follow-btn" onClick={handleFollowClick}>
                    {isFollowed ? <LazyStarSolidIcon fill="#6BB9A4" width={22} height={22} /> : <LazyStarIcon fill="#5F6368" />}
                </button>
            </div>
        </div>
    )
}

export default React.memo(SongDetail);