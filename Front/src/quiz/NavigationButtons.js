import React, { useEffect } from 'react';

const NavigationButtons = ({ currentPage, setCurrentPage, numberOfQuestions }) => {
    const goToNextPage = () => {
        setCurrentPage((page) => page + 1);
    }

    const goToPrevPage = () => {
        setCurrentPage((page) => page - 1);
    }
    console.log("currentPage:", currentPage);
    console.log("numberOfQuestions:", numberOfQuestions);
    console.log("Math.ceil(numberOfQuestions / 5):", Math.ceil(numberOfQuestions / 5));

    useEffect(() => {
        console.log("currentPage:", currentPage);
        console.log("numberOfQuestions:", numberOfQuestions);
        console.log("Math.ceil(numberOfQuestions / 5):", Math.ceil(numberOfQuestions / 5));
    }, [currentPage, numberOfQuestions]);

    return (
        <div className="flex justify-between m-2">
            {numberOfQuestions && currentPage < Math.ceil(numberOfQuestions / 5) && (
                <button onClick={goToNextPage} className="bg-[#fa5757] hover:bg-[#eb5252] text-white px-4 py-2 rounded-md focus:outline-none focus:ring focus:ring-blue-200">
                    Next
                </button>
            )}
             {currentPage > 0 && (
                <button onClick={goToPrevPage} className="bg-[#fa5757] hover:bg-[#eb5252] text-white px-4 py-2 rounded-md focus:outline-none focus:ring focus:ring-blue-200">
                    Prev
                </button>
            )}
        </div>
    );
};

export default NavigationButtons;
