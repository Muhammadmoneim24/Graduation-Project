const Footer = () => {
    return (
        <footer className="bg-gray-900 text-white py-10">
            <div className="container mx-auto">
                <div className="flex flex-col md:flex-row justify-between">
                    <div className="mb-8 md:mb-0">
                        <h3 className="text-lg font-bold mb-4">About Us</h3>
                        <p className="text-sm">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed aliquet urna ut lorem commodo, ut faucibus quam ullamcorper.</p>
                    </div>
                    <div className="mb-8 md:mb-0">
                        <h3 className="text-lg font-bold mb-4">Quick Links</h3>
                        <ul className="text-sm">
                            <li className="mb-2"><a href="#">Home</a></li>
                            <li className="mb-2"><a href="#">About</a></li>
                            <li className="mb-2"><a href="#">Services</a></li>
                            <li className="mb-2"><a href="#">Contact</a></li>
                        </ul>
                    </div>
                    <div className="mb-8 md:mb-0">
                        <h3 className="text-lg font-bold mb-4">Contact Us</h3>
                        <p className="text-sm">123 Main Street, New York, NY 10030</p>
                        <p className="text-sm">Email: info@example.com</p>
                        <p className="text-sm">Phone: +1 123 456 7890</p>
                    </div>
                </div>
                <div className="border-t border-gray-800 mt-8 pt-6 text-sm text-center">
                    <p>&copy; 2024 All rights reserved. Designed by Academix</p>
                </div>
            </div>
        </footer>
    );
}

export default Footer;
