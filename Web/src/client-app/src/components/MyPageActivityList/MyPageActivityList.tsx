import React from 'react';
import MyPageSongFollowList from './MyPageSongFollowList';
import MyPageRecentSongSearchHistory from './MyPageRecentSongSearchHistory';
import MyPageAccessHistory from './MyPageAccessHistory';

function MyPageActiviyList() {
    return (
        <div className='bottom-right-box-shadow mypage-item-wrapper'>
            <MyPageSongFollowList />
            <MyPageRecentSongSearchHistory />
            <MyPageAccessHistory />
        </div>
    )
}

export default React.memo(MyPageActiviyList);