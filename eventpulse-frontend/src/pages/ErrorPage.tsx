import React from "react";
import {Button} from "antd";
import {useNavigate} from "react-router-dom";

const ErrorPage: React.FC = () => {
    const navigate = useNavigate();

    const goToHome = () => {
        navigate("/");
    };

    return (
        <div className="min-h-screen flex flex-col items-center justify-center bg-gray-100 text-center">
            {/* Error Heading */}
            <h1 className="text-6xl font-bold text-red-500 mb-4">404</h1>
            <h2 className="text-2xl font-semibold text-gray-800 mb-2">
                Oops! Page Not Found
            </h2>
            <p className="text-gray-600 mb-8">
                The page you are looking for doesn't exist or has been moved.
            </p>

            {/* Go Back Button */}
            <Button
                type="primary"
                size="large"
                className="bg-blue-500 hover:bg-blue-600 transition duration-300 rounded-lg"
                onClick={goToHome}
            >
                Go to Homepage
            </Button>
        </div>
    );
};

export default ErrorPage;