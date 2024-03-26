import { Link } from "react-router-dom";
import { hero_img } from "../utils/constants";

const Hero = () => {
    return (
        <div className="bg-white text-black py-20" id="landing">
            <div className="container mx-auto flex items-center justify-between">
                <div className="w-1/2">
                    <div className="max-w-lg">
                        <h1 className="text-4xl font-bold mb-4"><span className="text-green-400">Studying</span> Online is now much easier</h1>
                        <p className="text-lg mb-8">Acadmix is an interesting platform that will teach you in a more interactive way.</p>
                        <div className="flex space-x-4">
                            <Link to='/login'>
                                <button className="bg-blue-500 hover:bg-blue-600 text-white px-6 py-2 rounded">Log In</button>
                            </Link>
                            <Link to='/signup'>
                                <button className="bg-green-500 hover:bg-green-600 text-white px-6 py-2 rounded">Sign Up</button>
                            </Link>
                        </div>
                    </div>
                </div>
                <div className="w-1/2">
                    <img src={hero_img} alt="" className="w-full" />
                </div>
            </div>
        </div>
    );
}

export default Hero;
