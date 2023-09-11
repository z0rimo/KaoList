import React from 'react';
import MyPageFollowedSongList from './MyPageFollowedSongList';
import MyPageSongSearchLogList from './MyPageSongSearchLogList';
import MyPageSignInLogList from './MyPageSignInLogList';

function MyPageActiviyList() {
    return (
        <div className='bottom-right-box-shadow mypage-item-wrapper'>
            <MyPageFollowedSongList />
            <MyPageSongSearchLogList />
            <MyPageSignInLogList />
        </div>
    )
}

export default React.memo(MyPageActiviyList);