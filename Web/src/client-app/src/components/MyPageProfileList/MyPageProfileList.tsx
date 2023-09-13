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
            const response = await window.api.kaoList.mypages.myPageProfile();
            
            if (response && response.resource) {
                const { email, nickname, nicknameEditedDateTime } = response.resource;
                setUserEmail(email ?? '');
                setNickname(nickname ?? '');
                setNicknameEditedTime(nicknameEditedDateTime ?? new Date());
            }
        })();
    }, []);
    

    React.useEffect(() => {
        console.log("useremail: ", userEmail);
    }, [userEmail]);

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