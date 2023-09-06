import React from 'react';
import MainLayout from '../../layouts/MainLayout/MainLayout';
import MainSection from '../../components/MainSection';
import MyPageProfileList from '../../components/MyPageProfileList/MyPageProfileList';
import MyPageActivityList from '../../components/MyPageActivityList/MyPageActivityList';
import './MyPage.scss';

function MyPage() {
    return (
        <MainLayout>
            <MainSection>
                <div className='mypage-wrapper'>
                    <MyPageProfileList />
                    <MyPageActivityList />
                </div>
            </MainSection>
        </MainLayout>
    )
}

export default React.memo(MyPage);