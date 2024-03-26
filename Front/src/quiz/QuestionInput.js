import React from 'react';

const QuestionInput = ({ questions, setQuestions, currentPage }) => {
    const handleQuestionChange = (e, index) => {
        const newQuestions = [...questions];
        newQuestions[index].question = e.target.value;
        setQuestions(newQuestions);
    };

    const handleTypeChange = (e, index) => {
        const newQuestions = [...questions];
        newQuestions[index].type = e.target.value;
        // Reset choices when type changes
        if (e.target.value !== 'mcq' && e.target.value !== 'multiple') {
            newQuestions[index].choices = [];
        }
        setQuestions(newQuestions);
    };

    const handleChoiceChange = (e, questionIndex, choiceIndex) => {
        const newQuestions = [...questions];
        newQuestions[questionIndex].choices[choiceIndex] = e.target.value;
        setQuestions(newQuestions);
    };
    const handleCorrectAnswerChange = (choiceIndex, questionIndex) => {
        const newQuestions = [...questions];
        newQuestions[questionIndex].correctAnswer = choiceIndex;
        setQuestions(newQuestions);
    };
    const handleSelectedAnswerChange = (e, questionIndex, choiceIndex) => {
        const newQuestions = [...questions];
        const selectedAnswers = newQuestions[questionIndex].selectedAnswers;
        if (e.target.checked) {
            selectedAnswers.push(choiceIndex);
        } else {
            const index = selectedAnswers.indexOf(choiceIndex);
            if (index !== -1) {
                selectedAnswers.splice(index, 1);
            }
        }
        newQuestions[questionIndex].selectedAnswers = selectedAnswers;
        setQuestions(newQuestions);
    };

    const handleRemoveQuestion = (index) => {
        const newQuestions = [...questions];
        newQuestions.splice(index + currentPage * 5, 1);
        setQuestions(newQuestions);
    };

    return (
        <>
            {questions.slice(currentPage * 5, (currentPage + 1) * 5).map((question, index) => (
                <div key={index} className="relative question  p-4 rounded-md mb-4">
                    <label htmlFor={`question-${index}`} className="block text-2xl font-bold text-[#fa5757] text-center mb-4">Question {index + 1 + currentPage * 5}</label>
                    <h1 className="font-bold text-[#fa5757]">1. Question:</h1>
                    <button className="absolute top-1 right-3 text-3xl rounded-full " onClick={() => handleRemoveQuestion(index)}>X</button>

                    <label className="block mt-2 font-bold ">Type:</label>
                    <select
                        value={question.type}
                        onChange={(e) => handleTypeChange(e, index + currentPage * 5)}
                        required
                        className="w-full border border-gray-300 rounded-md p-2 my-4 focus:outline-none focus:ring focus:ring-blue-200"
                    >
                        <option value="mcq">Multiple Choice</option>
                        <option value="multiple">Select Multiple Answers</option>
                        <option value="write">Write-in Answer</option>
                    </select>

                    <div className='my-3 relative'>
                        <div className='flex justify-between items-center my-6'>
                            <label className='m-2 font-bold'>Question Text:</label>
                            <button className='p-2 mr-2 bg-[#fa5757] text-white rounded-md hover:bg-red-700'>Add Photo</button>
                        </div>
                        <input
                            type="text"
                            id={`question-${index}`}
                            value={question.question}
                            onChange={(e) => handleQuestionChange(e, index + currentPage * 5)}
                            required
                            className="w-full h-32 border border-gray-300 rounded-md p-2 mt-1 mb-4 focus:outline-none focus:ring focus:ring-blue-200"
                        />
                        <div className="absolute inset-x-0 bottom-0 h-px bg-gray-300"></div>
                    </div>

                    {question.type === 'mcq' && (
                        <>
                            <label className='my-4 font-bold text-[#fa5757]'>2. Options:</label>
                            {question.choices.map((choice, choiceIndex) => (
                                <div key={choiceIndex} className='my-4'>
                                    <div className='flex justify-between items-center'>
                                        <div className='flex justify-start items-center ml-1'>
                                            <input
                                                type="radio"
                                                name={`choice-${choiceIndex}`}
                                                checked={question.correctAnswer === choiceIndex}
                                                onChange={(e) => handleCorrectAnswerChange(choiceIndex, index)}
                                                className="mr-2"
                                            />
                                            <label className='m-2 font-bold'>Option {choiceIndex + 1}</label>
                                        </div>
                                        <div className='flex justify-between items-center mr-2'>
                                            <button className='w-6 h-6 mr-2 border-solid border-2  rounded-md hover:bg-red-700'>+</button>
                                            <button className='w-6 h-6 ml-2 border-solid border-2 rounded-md hover:bg-red-700'>-</button>
                                        </div>
                                    </div>
                                    <div key={choiceIndex} className="flex items-center ">
                                        <input
                                            type="text"
                                            value={choice}
                                            onChange={(e) => handleChoiceChange(e, index + currentPage * 5, choiceIndex)}
                                            required
                                            className="border h-16 border-gray-300 rounded-md p-2 flex-grow focus:outline-none focus:ring focus:ring-blue-200"
                                        />
                                    </div>
                                </div>
                            ))}
                        </>
                    )}
                    {question.type === 'multiple' && (
                        <>
                            <label className="block mt-2">Choices:</label>
                            {question.choices.map((choice, choiceIndex) => (
                                <div key={choiceIndex} className="flex items-center mt-1">
                                    <input
                                        type="checkbox"
                                        checked={question.selectedAnswers.includes(choiceIndex)}
                                        onChange={(e) => handleSelectedAnswerChange(e, index + currentPage * 5, choiceIndex)}
                                        className="mr-2"
                                    />
                                    <input
                                        type="text"
                                        value={choice}
                                        onChange={(e) => handleChoiceChange(e, index + currentPage * 5, choiceIndex)}
                                        required
                                        className="border border-gray-300 rounded-md p-2 flex-grow focus:outline-none focus:ring focus:ring-blue-200"
                                    />
                                </div>
                            ))}
                        </>
                    )}
                </div>
            ))}
        </>
    );
};

export default QuestionInput;
