import { createSlice } from "@reduxjs/toolkit";


const addLectureSlice = createSlice({
    name: 'addLectureSlice',
    initialState:[],
    reducers: {
        addLecture: (state , action) =>{
            state.push(action.payload);
        }
    }
})

export const {addLecture} = addLectureSlice.actions;
export default addLectureSlice.reducer;