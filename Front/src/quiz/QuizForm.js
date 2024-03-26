import React, { useRef, useState } from 'react';
import { useDispatch } from 'react-redux';
import { addQuiz } from '../redux/quizSlice';
import { togglePaginationButtons } from '../redux/UiInteractionSlice';
import QuestionInput from './QuestionInput';
import NavigationButtons from './NavigationButtons';
import SubmitButton from './SubmitButton';

const QuizForm = () => {
    const [numberOfQuestions, setNumberOfQuestions] = useState(0);
    const [questions, setQuestions] = useState([]);
    const [currentPage, setCurrentPage] = useState(0);
    const quizName = useRef('');
    const quizDescription = useRef('');
    const quizInstructions = useRef('');
    const dispatch = useDispatch();
    const [Head, setHead] = useState(true);

    const handleNumQuestionsChange = (e) => {
        if (!isNaN(numberOfQuestions) && numberOfQuestions >= 0) {
            setQuestions(Array(numberOfQuestions).fill().map(() => ({
                type: 'mcq', 
                question: '',
                choices: ['', '', '', ''],
                correctAnswer: 0,
                selectedAnswers: [], 
                answer: '',
            })));
        } else {
            setQuestions([]);
        }
        setHead(false);
        dispatch(togglePaginationButtons());
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        
        const quizInfo = {
            quizName: quizName.current,
            quizInstructions: quizInstructions.current,
            quizDescription: quizDescription.current,
        };

        try {
            const response = await fetch('https://localhost:44338/api/Exams/CreateExam/1', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(quizInfo),
            });

            if (!response.ok) {
                throw new Error('Failed to add quiz');
            }

            const data = await response.json();
            console.log(data); // Log response data

            dispatch(addQuiz([quizInfo, ...questions]));
            setQuestions([]);
        } catch (error) {
            console.error('Error adding quiz:', error);
            // Handle error, show error message to user, etc.
        }
    };
    return (
        <>
            <div className="create-quiz mx-auto p-8 bg-white rounded-md">
                {/* <h2 className="text-2xl font-semibold mb-4">Create Quiz</h2> */}
                {Head && 
                <form onSubmit={handleSubmit}>
                    <div className=''>
                        <label className='font-bold'>Name</label>
                        <input 
                        ref={quizName} 
                        type='text' 
                        placeholder='Exam Name' 
                        onChange={(e)=>{quizName.current = (e.target.value)}}
                        className="mb-4 h-10 w-1/4 p-2 block rounded-md border border-gray-300 focus:outline-none focus:ring focus:ring-blue-200"
                    />
                    <label className='font-bold'>Descriptione</label>
                    <input 
                        ref={quizDescription} 
                        type='text' 
                        placeholder='Exam Description' 
                        onChange={(e)=>{quizDescription.current = (e.target.value)}}
                        className="mb-4 h-16 p-2 w-full rounded-md border border-gray-300 focus:outline-none focus:ring focus:ring-blue-200"
                    />
                    <label className='font-bold'>Instructions</label>
                    <input 
                        ref={quizInstructions} 
                        type='text' 
                        placeholder='Exam Instructions' 
                        onChange={(e)=>{quizInstructions.current = (e.target.value)}}
                        className="mb-4 h-16 p-2 w-full rounded-md border border-gray-300 focus:outline-none focus:ring focus:ring-blue-200"
                    />
                    <label className='font-bold'>Date</label>
                    <input 
                        type='text' 
                        placeholder='Date' 
                        className="mb-4 h-16 p-2 w-full rounded-md border border-gray-300 focus:outline-none focus:ring focus:ring-blue-200"
                    />
                    <label className='font-bold'>Time</label>
                    <input 
                        type='text' 
                        placeholder='Time' 
                        className="mb-4 h-16 p-2 w-full rounded-md border border-gray-300 focus:outline-none focus:ring focus:ring-blue-200"
                    />
                    </div>
                    <div className='flex justify-between'>

                        <div className='flex'>
                        <div className=''>
                            <label className='block font-bold'>Points</label>
                            <input 
                            type='number' 
                            placeholder='Total Points' 
                            className="mr-2 w-1/2 p-2 rounded-md border border-gray-300 focus:outline-none focus:ring focus:ring-blue-200"
                        />
                        </div>
                      <div className=''>
                        <label className='block font-bold'>Total Q.</label>
                        <input 
                        type="number" 
                        id="numQuestions" 
                        placeholder='Total Questions' 
                        onChange={(e)=>{setNumberOfQuestions(parseInt(e.target.value))}} 
                        required 
                        className="mr-2 w-1/2 p-2 rounded-md border border-gray-300 focus:outline-none focus:ring focus:ring-blue-200"
                    />
                      </div>
                        </div>
                      <button 
                        type="submit" 
                        onClick={handleNumQuestionsChange} 
                        className="bg-[#fa5757] mt-4 text-white px-4 py-2 rounded-md hover:bg-[#ee5151] "
                    >
                        Generate Questions
                    </button>
                    </div>
                    
                </form>}
                <QuestionInput questions={questions} setQuestions={setQuestions} currentPage={currentPage} />
                {numberOfQuestions > 0 && <NavigationButtons currentPage={currentPage} setCurrentPage={setCurrentPage} numberOfQuestions={numberOfQuestions} /> }
                <SubmitButton handleSubmit={handleSubmit} questions={questions} />
            </div>
        </>
    );
};

export default QuizForm;
