import {configureStore} from '@reduxjs/toolkit'
import UiInteractionSlice from "./UiInteractionSlice";
import  addCourseSlice  from './addCourseSlice';
import quizReducer from './quizSlice';
import lectureReducer from './addLectureSlice';
const store = configureStore({
    reducer: {
        UiInteraction: UiInteractionSlice,
        addCourse: addCourseSlice,
        quiz: quizReducer,
        addLecture: lectureReducer,
    }
})

export default store;