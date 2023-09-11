import React from 'react';
import MyPageThumbnail from './MyPageThumbnail';
import MyPageEmailAddress from './MyPageEmailAddress';
import MyPageNickname from './MyPageNickname';
import MyPageChangePassword from './MyPageChangePassword';
import MyPageAgreeEmail from './MyPageAgreeEmail';
import MyPageExternalLogin from './MyPageExternalLogin';

function MyPageProfileList() {
    const [userEmail, setUserEmail] = React.useState('');
    const [nickname, setNickname] = React.useState('');
    const [nicknameEditedTime, setNicknameEditedTime] = React.useState(new Date());

    React.useEffect(() => {
        (async () => {
            const response = await window.api.kaoList.mypages.myPage();

            if (response.item) {
                const { email, nickname, nicknameEditedDateTime } = response.item;
                setUserEmail(email ?? '');
                setNickname(nickname ?? '');
                setNicknameEditedTime(nicknameEditedDateTime ?? new Date());
            }
        })();
    }, []);

    return (
        <div className='bottom-right-box-shadow mypage-item-wrapper'>
            <MyPageThumbnail />
            <MyPageEmailAddress email={userEmail} />
            <MyPageNickname nickname={nickname} lastModified={nicknameEditedTime.toString()} />
            <MyPageChangePassword />
            <MyPageAgreeEmail />
            <MyPageExternalLogin />
        </div>
    )
}

export default React.memo(MyPageProfileList)