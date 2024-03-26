import React from 'react';
import Sidebar from './Sidebar';
import Header from '../Header';
import Home from '../Home';
import Hero from '../Hero';
import Content from '../Content';
import Footer from '../Footer';
import ContentView from './ContentView';

const InstructorUI = () => {
  return (
    <div className="flex flex-col min-h-screen">
      <Header />
      <div className="flex flex-1">
        <Sidebar />
        <div className="flex-1">
         <ContentView />
        </div>
      </div>
      <Footer />
    </div>
  );
};

export default InstructorUI;
