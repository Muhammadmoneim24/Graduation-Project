import { useRef, useState } from "react";
import validation from '../utils/formValidation';
import { useNavigate } from "react-router";

const Login = () =>{
    const [message, setMessage] = useState(null); 
    const [isLogin, setIsLogin] = useState(true); // State to manage whether it's login or register mode
    const email = useRef(null);
    const password = useRef(null);
    const firstName = useRef(null);
    const lastName = useRef(null);
    const userName = useRef(null);
    const confirmPassword = useRef(null);
    const userType = useRef(null);
    const navigate = useNavigate();

    const handleSubmit = async (event) => {
        event.preventDefault();
        if (isLogin) {
            // Handle login
            setMessage(validation(email.current.value , password.current.value));
            if(message) return;

            try {
                const response = await fetch('https://localhost:44338/api/Auth/Login', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({
                        email: email.current.value,
                        password: password.current.value
                    })
                });

                if (!response.ok) {
                    const errorData = await response.json();
                    throw new Error(errorData.message);
                }

                

                // Login successful
                const data = await response.json();

                // Store token and role in localStorage
                localStorage.setItem('token', data.token);
                localStorage.setItem('role', data.roles[0]);
    
                // Redirect based on role
                if (data.roles[0] === 'Instructor') {
                    // Redirect to instructor UI
                    navigate('/instructor')
                } else if (data.roles[0] === 'Student') {
                    // Redirect to student UI
                    window.location.href = '/student';
                }
                // Handle further actions (e.g., redirecting to another page, setting tokens in local storage, etc.)
                console.log(data);
            } catch (error) {
                setMessage(error.message);
            }
        } else {
            // Handle registration
            setMessage(validation(email.current.value , password.current.value));
            if(message) return;

            // Additional validation for registration fields
            // You can add more validation rules as needed

            try {
                const response = await fetch('https://localhost:44338/api/Auth/Register', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({
                        firstName: firstName.current.value,
                        lastName: lastName.current.value,
                        email: email.current.value,
                        password: password.current.value,
                        //confirmPassword: confirmPassword.current.value,
                        userName: userName.current.value,
                        role: userType.current.value
                    })
                });

                if (!response.ok) {
                    const errorData = await response.json();
                    throw new Error(errorData.message);
                }

                // Registration successful
                const data = await response.json();
                // Handle further actions (e.g., redirecting to another page, setting tokens in local storage, etc.)
                console.log(data);
            } catch (error) {
                setMessage(error.message);
            }
        }
    }
       console.log(userType.current) 
    const toggleForm = () => {
        setIsLogin(!isLogin);
    }

    return(
        <>
            <div className="fixed top-[40%] left-1/2 transform -translate-x-1/2 -translate-y-1/2">
                <form className="bg-white rounded-lg shadow-md p-8 w-[600px] w-full" onSubmit={handleSubmit}>
                    <div className="flex flex-col space-y-4">
                        {!isLogin && (
                            <>
                                <input type="text" placeholder="First Name" className="border border-gray-300 rounded-lg px-4 py-2" name="firstname" ref={firstName} required/>
                                <input type="text" placeholder="Last Name" className="border border-gray-300 rounded-lg px-4 py-2" name="lastname" ref={lastName} required/>
                                <input type="text" placeholder="User Name" className="border border-gray-300 rounded-lg px-4 py-2" name="username" ref={userName} required/>
                                <input type="password" placeholder="Confirm Password" className="border border-gray-300 rounded-lg px-4 py-2" name="confirmpassword" ref={confirmPassword} required/>
                                <select name="role" className="border border-gray-300 rounded-lg px-4 py-2" ref={userType} required >
                                    <option value="admin">Instructor</option>
                                    <option value="assistant">Assistant</option>
                                    <option value="student">Student</option>
                                </select>
                            </>
                        )}
                        <input type="email" name="email" placeholder="Email" className="border border-gray-300 rounded-lg px-4 py-2" ref={email} required/>
                        <input type="password" name="password" className="border border-gray-300 rounded-lg px-4 py-2" placeholder="Password" ref={password} required/>
                        <p className="text-red-500">{message}</p>
                        <button className="bg-blue-500 text-white px-4 py-2 rounded-lg hover:bg-blue-700">{isLogin ? "Sign In" : "Sign Up"}</button>
                        <p className="text-gray-500 text-sm text-center">
                            {isLogin ? (
                                <>New User? <span onClick={toggleForm} className="text-blue-500 cursor-pointer">Sign Up Now.</span></>
                            ) : (
                                <>Already Have An Account? <span onClick={toggleForm} className="text-blue-500 cursor-pointer">Sign In</span></>
                            )}
                        </p>
                    </div>
                </form>
            </div>
        </>
    );
}

export default Login;
