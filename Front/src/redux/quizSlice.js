
import { createSlice } from '@reduxjs/toolkit';


const quizSlice = createSlice({
  name: 'quiz',
  initialState: [],
  reducers: {
    addQuiz: (state, action) => {
      state.push(...action.payload);
    },
    
    submitQuiz: (state) =>{
      return [];
    },
    updateAnswer: (state,action) =>{
      const { index, answer: choiceIndex } = action.payload;
      state.selectedAnswer.push({ index, answer: choiceIndex })
    }
  }
});

export const { addQuiz, clearQuiz,submitQuiz,updateAnswer } = quizSlice.actions;

export default quizSlice.reducer;
