import React, { useState } from 'react';
import Header from '../components/Header';
import Sidebar from '../components/instructor/Sidebar';
import Footer from '../components/Footer';
import { useSelector } from 'react-redux';

const QuestionsPerPage = 5;

const Quiz = () => {
  const quizData = useSelector((state) => state.quiz);
  const [currentPage, setCurrentPage] = useState(0);
  const [userAnswers, setUserAnswers] = useState(Array(quizData.length - 1).fill(null));
  const [showResult, setShowResult] = useState(false);

  const handleUserAnswer = (index, value) => {
    if (!showResult) {
      const updatedUserAnswers = [...userAnswers];
      updatedUserAnswers[index] = value;
      setUserAnswers(updatedUserAnswers);
    }
  };

  const handleUserMultipleAnswers = (index, choiceIndex, checked) => {
    if (!showResult) {
      const updatedUserAnswers = [...userAnswers];
      if (!updatedUserAnswers[index]) {
        updatedUserAnswers[index] = [];
      }
      if (checked) {
        updatedUserAnswers[index].push(choiceIndex);
      } else {
        updatedUserAnswers[index] = updatedUserAnswers[index].filter((item) => item !== choiceIndex);
      }
      setUserAnswers(updatedUserAnswers);
    }
  };

  const calculateScore = () => {
    let score = 0;
    for (let i = 1; i < quizData.length; i++) {
      const question = quizData[i];
      const answer = userAnswers[i - 1];
      if (question.type === 'mcq' && answer === question.correctAnswer) {
        score++;
      } else if (question.type === 'multiple' && arraysEqual(answer, question.selectedAnswers)) {
        score++;
      }
    }
    return score;
  };

  const arraysEqual = (arr1, arr2) => {
    if (arr1.length !== arr2.length) return false;
    for (let i = 0; i < arr1.length; i++) {
      if (arr1[i] !== arr2[i]) return false;
    }
    return true;
  };

  const handleNext = () => {
    if (!showResult && currentPage < Math.ceil((quizData.length - 1) / QuestionsPerPage) - 1) {
      setCurrentPage(currentPage + 1);
    }
  };

  const handlePrev = () => {
    if (!showResult && currentPage > 0) {
      setCurrentPage(currentPage - 1);
    }
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    setShowResult(true);
  };
  console.log(quizData[0]);
  const renderQuestions = () => {
    const startIndex = currentPage * QuestionsPerPage + 1;
    const endIndex = Math.min(startIndex + QuestionsPerPage, quizData.length);
  
    return quizData.slice(startIndex, endIndex).map((question, index) => (
      <div key={index} className="question p-4 border-2 my-4 border-gray-200">
        <label className='text-lg font-bold p-2'>Question {index+1}</label>
        <p className="font-bold m-2 text-center">{question.question}</p>
        <div className="choices grid grid-cols-2 gap-4 my-2">
          {question.choices.map((choice, choiceIndex) => (
            <div key={choiceIndex} className="choice my-2 flex justify-center items-center mb-2">
              {question.type === "mcq" ? (
                <label className="cursor-pointer">
                  <input
                    type="radio"
                    value={choiceIndex}
                    checked={userAnswers[startIndex + index - 1] === choiceIndex}
                    onChange={() => handleUserAnswer(startIndex + index - 1, choiceIndex)}
                    disabled={showResult}
                    className="mr-2"
                  />
                  {choice}
                </label>
              ) : (
                <label className="cursor-pointer">
                  <input
                    type="checkbox"
                    value={choiceIndex}
                    checked={userAnswers[startIndex + index - 1] && userAnswers[startIndex + index - 1].includes(choiceIndex)}
                    onChange={(e) => handleUserMultipleAnswers(startIndex + index - 1, choiceIndex, e.target.checked)}
                    disabled={showResult}
                    className="mr-2"
                  />
                  {choice}
                </label>
              )}
            </div>
          ))}
        </div>
      </div>
    ));
  };
  

  return (
    <div className="flex flex-col min-h-screen">
      <Header />
      <div className="flex flex-1 justify-center items-center">
        <Sidebar />
        <div className="w-2/4 h-screen m-auto relative overflow-y-auto">
          <div className="quiz mx-auto p-8 bg-white rounded-md">
            <h2 className="text-2xl font-semibold mb-4">Quiz</h2>
            <div className="mb-4 flex justify-between items-center">
            
            <table className="text-[#fa5757] font-bold mb-4 border-2 w-full">
      <tbody>
        <tr>
          <td className="w-3/4 border-r-2">
            <table>
              <tbody>
                <tr className=''>
                  <td className=" pr-4">Name: {quizData[0].quizName}</td>
                </tr>
                <tr className=''>
                  <td className=" pr-4">Description: {quizData[0].quizDescription}</td>
                </tr>
                <tr className=''>
                  <td className=" pr-4">Instruction: {quizData[0].quizInstructions}</td>
                </tr>
                <div className='w-full'></div>
                <tr className=''>
                  <td className=" pr-4">Total Questions: {quizData.length - 1}</td>
                </tr>
                {/* </div> */}
                {/* You may need to calculate the total points based on quizData */}
                <tr>
                  <td className=" pr-4">Total Points:</td>
                  <td>{/* Calculate total points */}</td>
                </tr>
              </tbody>
            </table>
          </td>
          <td className="w-1/4">
            <h2 className='text-center'>Time</h2>
          </td>
        </tr>
      </tbody>
    </table>
            </div>
            <form onSubmit={handleSubmit}>
              {renderQuestions()}
              <div className="flex justify-between mt-4">
                <button type="button" onClick={handlePrev} disabled={currentPage === 0 || showResult} className="bg-gray-200 text-gray-600 px-4 py-2 rounded-md focus:outline-none">
                  Previous
                </button>
                <button type="button" onClick={handleNext} disabled={currentPage === Math.ceil((quizData.length - 1) / QuestionsPerPage) - 1 || showResult} className="bg-gray-200 text-gray-600 px-4 py-2 rounded-md focus:outline-none">
                  Next
                </button>
                {!showResult && currentPage === Math.ceil((quizData.length - 1) / QuestionsPerPage) - 1 && (
                  <button type="submit" className="bg-[#fa5757] text-white px-4 py-2 rounded-md hover:bg-[#eb5252] focus:outline-none">
                    Submit Quiz
                  </button>
                )}
              </div>
            </form>
            {showResult && (
              <div className="result mt-4">
                <h3>Your Score: {calculateScore()} / {quizData.length - 1}</h3>
              </div>
            )}
          </div>
        </div>
      </div>
      <Footer />
    </div>
  );
};

export default Quiz;
