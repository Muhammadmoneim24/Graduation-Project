import React from 'react';

const LectureCard = ({ lecture }) => {
    const { name, 
        lecFile
         } = lecture;
    console.log(lecture,'lec');
    const handleDownload = () => {

        const bytes = new Uint8Array(lecFile);


        const blob = new Blob([bytes], { type: 'application/pdf' });


        const url = window.URL.createObjectURL(blob);


        const anchor = document.createElement('a');
        anchor.href = url; // Set the URL
        anchor.download = name; // Set the downloaded file name


        document.body.appendChild(anchor);


        anchor.click();


        document.body.removeChild(anchor);


        window.URL.revokeObjectURL(url);
    };

    console.log(lecFile);

    return (
        <div className='w-72 h-auto rounded-xl overflow-hidden shadow-md m-4 bg-white hover:shadow-lg cursor-pointer'>
            <div className="relative flex flex-col text-gray-700 bg-white shadow-md bg-clip-border rounded-xl w-full">
                <div className="p-6">
                    <h5 className="block mb-2 font-sans text-xl font-semibold leading-snug text-blue-gray-900 text-center">
                        {name}
                    </h5>
                    <div className='mb-4 flex justify-center'>
                        <button className='bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-full' onClick={handleDownload}>
                            Download File
                        </button>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default LectureCard;
