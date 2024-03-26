import React, { useState } from 'react';
import Header from './Header';
import Hero from './Hero';
import Footer from './Footer';
import Content from './Content';
import Login from './Login';
import { useSelector } from 'react-redux';

const Home = () => {
    const showLogin = useSelector(store => store.UiInteraction.showLoginForm);

    return (
        <div style={{ display: 'flex', flexDirection: 'column', minHeight: '100vh' }}>
            <Header />
            <div style={{ flex: 1 }}>
                {showLogin ? <Login /> : (
                    <>
                        <Hero />
                        <Content />
                    </>
                )}
            </div>
            <Footer />
        </div>
    );
}

export default Home;
