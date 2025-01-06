import urlEndpoints from "../../constants/urlEndpoints.ts";

export const LoginFooter = () => {
    return (
        <p className="text-center text-gray-500 mt-4 text-sm">
            Don't have an account?{" "}
            <a href={urlEndpoints.register} className="text-blue-500 underline hover:text-blue-600">
                Sign up now
            </a>
        </p>
    );
};
