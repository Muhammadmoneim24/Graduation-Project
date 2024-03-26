import React, { useRef } from 'react';
import { useDispatch } from 'react-redux';
import { toggleShowAddLectureForm } from '../../redux/UiInteractionSlice';

const AddLecture = () => {
    const nameRef = useRef(null);
    const fileRef = useRef(null);
    const dispatch = useDispatch();

    const handleAddLecture = async (e) => {
        e.preventDefault();
        
        const lectureInfo = new FormData();
        lectureInfo.append('name', nameRef.current.value);
        lectureInfo.append('file', fileRef.current.files[0]);

        try {
            const response = await fetch('https://localhost:44338/api/Lectures/2', {
                method: 'POST',
                body: lectureInfo,
            });

            if (!response.ok) {
                throw new Error('Failed to add lecture');
            }

            const data = await response.json();
            console.log(data); // Log response data
        } catch (error) {
            console.error('Error adding lecture:', error);
            // Handle error, show error message to user, etc.
        }
        
        // Clear form fields
        nameRef.current.value = '';
        fileRef.current.value = '';
        
        // Close the form
        dispatch(toggleShowAddLectureForm());
    };    
    return (
        <div className='flex justify-center items-center h-full'>
            <div className='max-w-lg w-full bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4 relative'>
                <button className="absolute -top-2 right-0 m-2 text-gray-500 text-3xl" onClick={() => dispatch(toggleShowAddLectureForm())}>X</button>
                <h1 className='text-2xl font-bold mb-6 text-center'>Add Lecture</h1>
                <form onSubmit={handleAddLecture}>
                    <div className='mb-4'>
                        <label className='block text-gray-700 text-sm font-bold mb-2' htmlFor='lectureName'>Lecture Name</label>
                        <input
                            className='shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline'
                            id='lectureName'
                            type='text'
                            placeholder='Enter lecture name'
                            ref={nameRef}
                            required
                        />
                    </div>
                    <div className='mb-4'>
                        <label className='block text-gray-700 text-sm font-bold mb-2' htmlFor='lectureFile'>Upload File</label>
                        <input
                            ref={fileRef}
                            className='shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline'
                            id='lectureFile'
                            type='file'
                            accept='.pdf, .doc, .docx, .ppt, .pptx'
                            required
                        />
                    </div>
                    <div className='flex justify-center'>
                        <button
                            className='bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-full mt-4'
                            type='submit'
                        >
                            Submit
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default AddLecture;
