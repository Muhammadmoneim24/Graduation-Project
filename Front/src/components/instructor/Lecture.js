import React, { useState, useEffect } from 'react';
import Sidebar from './Sidebar';
import Header from '../Header';
import LectureCard from './LectureCard';
import Footer from '../Footer';

const Lecture = () => {
    const [lectures, setLectures] = useState([]);

    useEffect(() => {
        // Fetch data from the API
        const fetchData = async () => {
            try {
                const response = await fetch('https://localhost:44338/api/Lectures/1');
                if (!response.ok) {
                    throw new Error('Failed to fetch data');
                }
                const data = await response.json();
                setLectures([...lectures,data]);
            } catch (error) {
                console.error('Error fetching data:', error);
                // Handle error, show error message to user, etc.
            }
        };

        fetchData(); // Call the fetchData function when the component mounts
    }, []); // Pass an empty dependency array to useEffect to ensure it runs only once

    return (
        <div className='flex flex-col min-h-screen'>
            <Header />
            <div className='flex flex-1 justify-center items-center'>
                <Sidebar />
                <div className='w-3/4 h-screen m-auto relative overflow-y-auto'>
                    <div className='flex h-full p-6'>
                        <div className='flex justify-center items-start flex-wrap'>
                            {lectures.map((lecture, index) => (
                                <LectureCard key={index} lecture={lecture} />
                            ))}
                        </div>
                    </div>
                </div>
            </div>
            <Footer />
        </div>
    );
};

export default Lecture;
