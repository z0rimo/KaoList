import React from "react";
import SongDetail from "../../components/SongDetail";
import SongDetailOther, { ISongDetailOtherItem } from "../../components/SongDetailOther";
import StringHelper from "../../StringHelper";
import { useTranslation } from "react-i18next";
import SongDetailOtherMySong, { ISongDetailOtherMySongItem } from "../../components/SongDetailOtherMySong";
import MainLayout from "../../layouts/MainLayout/MainLayout";
import MainSection from "../../components/MainSection";
import './SongDetailPage.scss';

const SongDetailOtherItem = React.memo((props: ISongDetailOtherItem) => {
    const { t } = useTranslation('Chart')
    const navgiateToDetailClick = () => {
        window.location.href = `/songs/detail?id=${props.id}`;
    }

    return (
        <tr className="table-td song" onClick={navgiateToDetailClick}>
            <td>
                <img alt={StringHelper.format(t('Thumbnail of {0}'), props.title)}
                    src="https://i.ytimg.com/vi/XOxI7bEHQgc/hqdefault.jpg?sqp=-oaymwEcCPYBEIoBSFXyq4qpAw4IARUAAIhCGAFwAcABBg==&rs=AOn4CLC5kqwJDiTRyMg0D5mIsZ0ZyTcvRg" />
            </td>
            <td>
                <p className="title">{props.title}</p>
                <p>{props.songUsers?.map(item => item.nickname).join(", ")}</p>
            </td>
        </tr>
    )
});

const SongDetailOtherMySongItem = React.memo((props: ISongDetailOtherMySongItem) => {
    const navgiateToDetailClick = () => {
        window.location.href = `/songs/detail?id=${props.id}`;
    }

    return (
        <tr className="table-td song" onClick={navgiateToDetailClick}>
            <td>
                <p className="title">{props.title}</p>
            </td>
            <td>
                <p>{props.created?.toDateString()}</p>
            </td>
        </tr>
    )
});

const songDetailOtherItemRender: Parameters<typeof SongDetailOther>[0]['renderer'] = (item) => {
    return <SongDetailOtherItem {...item} />
}

const songDetailOtherMySongItemRender: Parameters<typeof SongDetailOther>[0]['renderer'] = (item) => {
    return <SongDetailOtherMySongItem {...item} />
}

const Table = React.memo((props: React.HTMLAttributes<HTMLTableElement>) => <table {...props} />);

function SongDetailPage() {
    const { t } = useTranslation('Chart')

    return (
        <MainLayout>
            <MainSection>
                <div className="song-detail-container bottom-right-box-shadow">
                    <SongDetail />
                    <SongDetailOther
                        maxResults={10}
                        Table={Table}
                        thead={
                            <thead>
                                <tr>
                                    <th>{t('Other Users')}</th>
                                </tr>
                            </thead>
                        }
                        renderer={songDetailOtherItemRender}
                    />
                    <SongDetailOtherMySong
                        maxResults={10}
                        Table={Table}
                        thead={
                            <thead>
                                <tr>
                                    <th>{t('Songs I have sung')}</th>
                                </tr>
                            </thead>
                        }
                        renderer={songDetailOtherMySongItemRender}
                    />
                </div>
            </MainSection>
        </MainLayout>
    )

}

export default React.memo(SongDetailPage);