import {useState} from 'react';
import { useDispatch } from "react-redux";
import { navLogo } from "../utils/constants";
import { Link, useLocation } from "react-router-dom";
import { showLoginForm, toggleSidebar } from "../redux/UiInteractionSlice";
import { hamburgerMenuIcon } from "../utils/constants";

const Header = () => {
    const location = useLocation();
    const isHome = location.pathname === '/';
    const [showHamburgerMenu , setShowHamburgerMenu] = useState(!isHome)
    const dispatch = useDispatch();
    const handleSideBar = () =>{
        dispatch(toggleSidebar())
    }
    const handleClick = () => {
        dispatch(showLoginForm());
    };


    return (
        <div className="grid grid-cols-12 border-b-2  shadow-sm font-bold items-center p-2">
            <div className="flex col-span-1 items-center justify-center">
{               showHamburgerMenu && <img onClick={handleSideBar} className="bg-transparent cursor-pointer h-10" alt="hamburgericon" src={hamburgerMenuIcon} />
}                <Link>
                    <img src={navLogo} alt="navlogo.." className="w-20 h-auto ml-2" />
                </Link>
            </div>
            <div className="col-span-10 md:block text-center">
                <ul className="flex justify-center items-center space-x-20">
                    <li><Link className="hover:text-[#18a9ea]">Home</Link></li>
                    <li><Link className="hover:text-[#18a9ea]">Contact</Link></li>
                    <li><Link className="hover:text-[#18a9ea]">About Us</Link></li>
                </ul>
            </div>
            <div className="flex col-span-1 justify-center">
                <button onClick={handleClick} className="bg-[#18a9ea] text-white m-2 p-3 w-28 rounded-md">Login</button>
            </div>
        </div>
    );
};

export default Header;
