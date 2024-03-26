const Question = ({ question, index, userAnswer, handleUserAnswer, handleUserMultipleAnswers, showResult }) => {
    const handleChange = (value) => {
      console.log("Handling radio change:", value);
      handleUserAnswer(index, value);
    };
  
    const handleCheckboxChange = (choiceIndex, checked) => {
      console.log("Handling checkbox change:", choiceIndex, checked);
      handleUserMultipleAnswers(index, choiceIndex, checked);
    };
  
    return (
      <div className="question">
        <p>{question.question}</p>
        <div className="choices">
          {question.choices.map((choice, choiceIndex) => (
            <div key={choiceIndex} className="choice">
              {question.type === "mcq" ? (
                <label>
                  <input
                    type="radio"
                    value={choiceIndex}
                    checked={userAnswer === choiceIndex}
                    onChange={() => handleChange(choiceIndex)}
                    disabled={showResult}
                  />
                  {choice}
                </label>
              ) : (
                <label>
                  <input
                    type="checkbox"
                    value={choiceIndex}
                    checked={userAnswer && userAnswer.includes(choiceIndex)}
                    onChange={(e) => handleCheckboxChange(choiceIndex, e.target.checked)}
                    disabled={showResult}
                  />
                  {choice}
                </label>
              )}
            </div>
          ))}
        </div>
      </div>
    );
  };
  
  export default Question;