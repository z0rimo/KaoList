import React from 'react';
import Footer from '../../components/Footer';
import Header from '../../components/Header';
import './MainLayout.scss';

function MainLayout(props: React.HTMLAttributes<HTMLDivElement>) {
    const { children, ...rest } = props;

    return (
        <div id="main-layout" {...rest}>
            <Header />
            {children}
            <Footer />
        </div>
    )
}


export default React.memo(MainLayout);