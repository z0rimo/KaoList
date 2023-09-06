import React from 'react';
import MyPageThumbnail from './MyPageThumbnail';
import MyPageEmailAddress from './MyPageEmailAddress';
import MyPageNickname from './MyPageNickname';
import MyPageChangePassword from './MyPageChangePassword';
import MyPageAgreeEmail from './MyPageAgreeEmail';
import MyPageExternalLogin from './MyPageExternalLogin';

function MyPageProfileList() {
    return (
        <div className='bottom-right-box-shadow mypage-item-wrapper'>
            <MyPageThumbnail />
            <MyPageEmailAddress />
            <MyPageNickname />
            <MyPageChangePassword />
            <MyPageAgreeEmail />
            <MyPageExternalLogin />
        </div>
    )
}

export default React.memo(MyPageProfileList)