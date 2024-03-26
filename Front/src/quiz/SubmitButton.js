import React from 'react';

const SubmitButton = ({ handleSubmit, questions }) => {
    // Check if questions is not undefined and has a truthy length property
    const shouldRenderSubmitButton = questions && questions.length > 0;

    return (
        <>
            {shouldRenderSubmitButton && (
                <button 
                    type="submit" 
                    onClick={handleSubmit} 
                    className="bg-[#fa5757] text-white px-4 py-2 rounded-md hover:bg-blue-600 focus:outline-none focus:ring focus:ring-blue-200"
                >
                    Submit Quiz
                </button>
            )}
        </>
    );
};

export default SubmitButton;
