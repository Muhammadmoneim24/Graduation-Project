import { createSlice } from "@reduxjs/toolkit";


const addCourseSlice = createSlice({
    name: 'addCourseSlice',
    initialState:[],
    reducers: {
        addCourse: (state , action) =>{
            state.push(action.payload);
        }
    }
})

export const {addCourse} = addCourseSlice.actions;
export default addCourseSlice.reducer;