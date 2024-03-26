import { createSlice } from "@reduxjs/toolkit";


const UiInteractionSlice = createSlice({
    name: 'UiInteraction',
    initialState:{
        showLoginForm: false,
        isSidebarOpen: true,
        showAddCourseForm: false,
        showAddLectureForm: false,
        showPaginationButtons: false,
        courseID:0,
    },
    reducers: {
        showLoginForm: (state) =>{
            state.showLoginForm = !state.showLoginForm;
        },
        toggleSidebar: (state) =>{
            state.isSidebarOpen = !state.isSidebarOpen
        },
        toggleShowAddCourseForm: (state) =>{
            state.showAddCourseForm = !state.showAddCourseForm;
        },
        toggleShowAddLectureForm: (state) =>{
            state.showAddLectureForm = !state.showAddLectureForm;
        },
        togglePaginationButtons: (state)=>{
            state.showPaginationButtons = !state.showPaginationButtons;
        },
        setCourseID: (state , action)=>{
            state.courseID = action.payload;
        }
        
    }
})

export const {showLoginForm,toggleSidebar,
     toggleShowAddCourseForm,togglePaginationButtons,toggleShowAddLectureForm ,setCourseID} = UiInteractionSlice.actions;
export default UiInteractionSlice.reducer;