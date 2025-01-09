import React, {useState} from "react";

const menuItems = [
    {label: "Home", href: "/"},
    {label: "About", href: "/about"},
    {label: "Services", href: "/services"},
    {label: "Contact", href: "/contact"},
];

const Navbar: React.FC = () => {
    const [isMobileMenuOpen, setIsMobileMenuOpen] = useState(false);

    const getActiveClass = (href: string) =>
        window.location.pathname === href
            ? "text-blue-600 border-b-4 border-blue-600 hover:border-blue-600 transition-colors"
            : "text-gray-800 hover:text-blue-500 hover:bg-gray-200 transition-colors";

    return (
        <nav className="h-20 bg-white border-b border-gray-200 fixed w-full z-20 top-0 start-0 shadow-lg">
            <div className="max-w-screen flex flex-wrap items-center justify-between mx-auto p-4">
                {/* Logo */}
                <a href="/" className="flex items-center space-x-3">
          <span className="self-center text-2xl font-semibold text-gray-800">
            EventPulse
          </span>
                </a>

                {/* Right Section: Buttons */}
                <div className="flex md:order-2 space-x-3">
                    <button
                        className="text-white bg-blue-600 hover:bg-blue-700 focus:ring-4 focus:ring-blue-300 rounded-lg px-4 py-2"
                    >
                        Explore Events
                    </button>

                    <button
                        type="button"
                        onClick={() => setIsMobileMenuOpen((prev) => !prev)}
                        className="inline-flex items-center p-2 w-10 h-10 justify-center text-sm text-gray-500 rounded-lg md:hidden hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-gray-200"
                        aria-label="Toggle navigation"
                    >
                        <span className="sr-only">Open main menu</span>
                        <svg
                            className="w-5 h-5"
                            xmlns="http://www.w3.org/2000/svg"
                            fill="none"
                            viewBox="0 0 17 14"
                            aria-hidden="true"
                        >
                            <path
                                stroke="currentColor"
                                strokeLinecap="round"
                                strokeLinejoin="round"
                                strokeWidth="2"
                                d="M1 1h15M1 7h15M1 13h15"
                            />
                        </svg>
                    </button>
                </div>

                {/* Navigation Menu */}
                <div
                    className={`${
                        isMobileMenuOpen ? "block" : "hidden"
                    } md:flex md:w-auto items-center justify-between w-full`}
                    id="navbar-sticky"
                >
                    <ul className="flex flex-col p-4 md:p-0 mt-4 font-medium border border-gray-100 rounded-lg bg-gray-50 md:space-x-8 md:flex-row md:mt-0 md:border-0 md:bg-white">
                        {menuItems.map((item) => (
                            <li key={item.label}>
                                <a
                                    href={item.href}
                                    className={`block py-2 px-3 rounded ${getActiveClass(
                                        item.href
                                    )}`}
                                    aria-current={window.location.pathname === item.href ? "page" : undefined}
                                >
                                    {item.label}
                                </a>
                            </li>
                        ))}
                    </ul>
                </div>
            </div>
        </nav>
    );
};

export default Navbar;
