import React from 'react';
import MainLayout from '../../layouts/MainLayout/MainLayout';
import MainSection from '../../components/MainSection';
import MyPageProfileList from '../../components/MyPageProfileList/MyPageProfileList';
import MyPageActivityList from '../../components/MyPageActivityList/MyPageActivityList';
import './MyPage.scss';

function MyPage() {
    const handleSave = () => {
        window.location.reload();
    }

    return (
        <MainLayout className='gray-theme'>
            <MainSection>
                <div className='mypage-wrapper'>
                    <MyPageProfileList />
                    <MyPageActivityList />
                    <button onClick={handleSave}>저장</button>
                </div>
            </MainSection>
        </MainLayout>
    )
}

export default React.memo(MyPage);