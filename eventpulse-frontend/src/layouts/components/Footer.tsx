import React from "react";

const Footer: React.FC = () => {
    return (
        <footer className="bg-gray-800 text-gray-200 py-8 w-full">
            <div className="container mx-auto px-4 text-center">
                <p>© 2025 EventPulse. All rights reserved.</p>
                <p className="mt-2">
                    <a href="/privacy" className="text-blue-400 hover:underline">
                        Privacy Policy&nbsp;
                    </a>
                    <a href="/terms" className="text-blue-400 hover:underline">
                        Terms of Use
                    </a>
                </p>
            </div>
        </footer>
    )
}

export default Footer;