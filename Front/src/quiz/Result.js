import React from "react";

const Result = ({ score, totalQuestions }) => {
  return (
    <div className="result">
      <h3>Your Score: {score} / {totalQuestions-1}</h3>
    </div>
  );
};

export default Result;
