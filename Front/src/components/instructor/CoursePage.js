import React, { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import Sidebar from './Sidebar';
import Header from '../Header';
import Footer from '../Footer';
import AddLecture from './AddLecture';
import { toggleShowAddLectureForm } from '../../redux/UiInteractionSlice';

const CoursePage = () => {
    const [showAddExamForm, setShowAddExamForm] = useState(false);
    const [showAddAssignmentForm, setShowAddAssignmentForm] = useState(false);
    const showAddLectureForm = useSelector(store => store.UiInteraction.showAddLectureForm);

    const courseInfo = useSelector(store => store.addCourse);
    const dispatch = useDispatch();
    
    return (
        <div className='flex flex-col min-h-screen'>
            <Header />
            <div className='flex flex-1 justify-center items-center'>
                <Sidebar />
                <div className='w-3/4 h-screen m-auto relative overflow-y-auto'>
                    {/* Centering the AddLecture component */}
                    <div className='flex justify-center items-center h-full'>
                        {showAddLectureForm && <AddLecture />}
                    </div>
                </div>
            </div>
            <div className='absolute bottom-4 right-16'>
                <div className='flex justify-center items-center gap-2'>
                    <button
                        className='bg-[#18A9EA] hover:bg-[#4b9cc2] text-white font-bold py-2 px-4 rounded-full'
                        onClick={() => setShowAddExamForm(!showAddExamForm)}
                    >
                        {showAddExamForm ? 'Close' : '+ Exam'}
                    </button>
                    <button
                        className='bg-[#18A9EA] hover:bg-[#4b9cc2] text-white font-bold py-2 px-4 rounded-full'
                        onClick={() => dispatch(toggleShowAddLectureForm())}
                    >
                        {showAddLectureForm ? 'Close' : '+ Lecture'}
                    </button>
                    <button
                        className='bg-[#18A9EA] hover:bg-[#4b9cc2] text-white font-bold py-2 px-4 rounded-full'
                        onClick={() => setShowAddAssignmentForm(!showAddAssignmentForm)}
                    >
                        {showAddAssignmentForm ? 'Close' : '+ Assignment'}
                    </button>
                </div>
            </div>
            <Footer />
        </div>
    );
};

export default CoursePage;
