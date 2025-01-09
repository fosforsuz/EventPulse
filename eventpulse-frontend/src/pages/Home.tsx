import {MainLayout as Layout} from "../layouts/MainLayout.tsx";
import {Input, Button} from "antd";
import React from "react";

const recentEvents = [
    {
        id: 1,
        title: "React Konferansı 2025",
        date: "10 Ocak 2025",
        location: "İstanbul",
        description: "A deep dive into the latest trends in React and frontend development.",
        image: "https://via.placeholder.com/300x200",
    },
    {
        id: 2,
        title: "JavaScript Workshop",
        date: "15 Ocak 2025",
        location: "Ankara",
        description: "Learn modern JavaScript concepts and hands-on coding practices.",
        image: "https://via.placeholder.com/300x200",
    },
    {
        id: 3,
        title: "UI/UX Tasarım Atölyesi",
        date: "20 Ocak 2025",
        location: "İzmir",
        description: "Enhance your design skills with practical UI/UX exercises.",
        image: "https://via.placeholder.com/300x200",
    },
    {
        id: 4,
        title: "React Konferansı 2025",
        date: "10 Ocak 2025",
        location: "İstanbul",
        description: "A deep dive into the latest trends in React and frontend development.",
        image: "https://via.placeholder.com/300x200",
    },
    {
        id: 5,
        title: "JavaScript Workshop",
        date: "15 Ocak 2025",
        location: "Ankara",
        description: "Learn modern JavaScript concepts and hands-on coding practices.",
        image: "https://via.placeholder.com/300x200",
    },
    {
        id: 6,
        title: "UI/UX Tasarım Atölyesi",
        date: "20 Ocak 2025",
        location: "İzmir",
        description: "Enhance your design skills with practical UI/UX exercises.",
        image: "https://via.placeholder.com/300x200",
    },
    {
        id: 7,
        title: "React Konferansı 2025",
        date: "10 Ocak 2025",
        location: "İstanbul",
        description: "A deep dive into the latest trends in React and frontend development.",
        image: "https://via.placeholder.com/300x200",
    },
    {
        id: 8,
        title: "JavaScript Workshop",
        date: "15 Ocak 2025",
        location: "Ankara",
        description: "Learn modern JavaScript concepts and hands-on coding practices.",
        image: "https://via.placeholder.com/300x200",
    },
    {
        id: 9,
        title: "UI/UX Tasarım Atölyesi",
        date: "20 Ocak 2025",
        location: "İzmir",
        description: "Enhance your design skills with practical UI/UX exercises.",
        image: "https://via.placeholder.com/300x200",
    },
    {
        id: 10,
        title: "React Konferansı 2025",
        date: "10 Ocak 2025",
        location: "İstanbul",
        description: "A deep dive into the latest trends in React and frontend development.",
        image: "https://via.placeholder.com/300x200",
    },
    {
        id: 11,
        title: "JavaScript Workshop",
        date: "15 Ocak 2025",
        location: "Ankara",
        description: "Learn modern JavaScript concepts and hands-on coding practices.",
        image: "https://via.placeholder.com/300x200",
    },
    {
        id: 12,
        title: "UI/UX Tasarım Atölyesi",
        date: "20 Ocak 2025",
        location: "İzmir",
        description: "Enhance your design skills with practical UI/UX exercises.",
        image: "https://via.placeholder.com/300x200",
    },
];

const Home: React.FC = () => {
    return (
        <Layout>
            <HeroSection/>
            <RecentEvents/>
        </Layout>
    );
};

const HeroSection: React.FC = () => {
    return (
        <section
            className="relative w-5/6 mt-16 bg-gradient-to-br from-blue-100 via-white to-blue-200 py-20 rounded-3xl shadow-xl">
            <div className="container mx-auto text-center">
                <h1 className="text-5xl font-bold text-gray-800 mb-6">
                    Explore Latest Events
                </h1>
                <p className="text-gray-600 mb-8 text-lg">
                    Check out the latest events and plan your next adventure with EventPulse.
                </p>

                {/* Search Bar */}
                <div className="flex justify-center items-center gap-4">
                    <Input
                        placeholder="Search by location or event name"
                        size="large"
                        className="w-3/5 rounded-lg shadow-md focus:ring-2 focus:ring-blue-500 focus:outline-none"
                    />
                    <Button
                        type="primary"
                        size="large"
                        className="rounded-lg bg-blue-600 hover:bg-blue-700 text-white px-8 py-3 transition"
                    >
                        Search
                    </Button>
                </div>

                {/* Popular Categories */}
                <div className="mt-8">
                    <span className="text-gray-500">Popular: </span>
                    <button className="text-blue-600 hover:underline mx-2">Conferences</button>
                    <button className="text-blue-600 hover:underline mx-2">Workshops</button>
                    <button className="text-blue-600 hover:underline mx-2">Concerts</button>
                </div>
            </div>
        </section>
    );
};

const RecentEvents: React.FC = () => {
    return (
        <section className="relative w-5/6 py-8 rounded-b-3xl">
            <div className="container mx-auto px-4">
                <h2 className="text-2xl font-semibold text-gray-800 mb-8 text-center">
                    Recent Events
                </h2>
                <div className="grid grid-cols-1 md:grid-cols-4 gap-8">
                    {recentEvents.map((event) => (
                        <div
                            key={event.id}
                            className="relative bg-white shadow-lg rounded-lg overflow-hidden transform transition duration-300 hover:scale-105 hover:shadow-xl"
                        >
                            {/* Görsel */}
                            <div className="relative">
                                <img
                                    src={event.image}
                                    alt={event.title}
                                    className="w-full h-56 object-cover transition-transform duration-300 hover:scale-110"
                                />
                                {/* Kategori */}
                                <span
                                    className="absolute top-2 left-2 bg-blue-600 text-white text-xs font-semibold px-3 py-1 rounded-full shadow-md">
                                      Workshop
                                    </span>
                            </div>

                            {/* Etkinlik Detayları */}
                            <div className="p-6">
                                <h3 className="text-xl font-bold text-gray-800 mb-2">{event.title}</h3>

                                {/* Description */}
                                <p className="text-gray-600 text-sm mb-4">{event.description}</p>

                                {/* Tarih ve Lokasyon */}
                                <div className="flex items-center text-gray-600 text-sm mb-2">
                                    <svg
                                        xmlns="http://www.w3.org/2000/svg"
                                        className="h-5 w-5 mr-2 text-blue-600"
                                        fill="none"
                                        viewBox="0 0 24 24"
                                        stroke="currentColor"
                                    >
                                        <path
                                            strokeLinecap="round"
                                            strokeLinejoin="round"
                                            strokeWidth="2"
                                            d="M8 7V3m8 4V3m-9 9h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z"
                                        />
                                    </svg>
                                    {event.date}
                                </div>
                                <div className="flex items-center text-gray-600 text-sm">
                                    <svg
                                        xmlns="http://www.w3.org/2000/svg"
                                        className="h-5 w-5 mr-2 text-blue-600"
                                        fill="none"
                                        viewBox="0 0 24 24"
                                        stroke="currentColor"
                                    >
                                        <path
                                            strokeLinecap="round"
                                            strokeLinejoin="round"
                                            strokeWidth="2"
                                            d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2z"
                                        />
                                    </svg>
                                    {event.location}
                                </div>

                                {/* Buton */}
                                <button
                                    className="mt-4 w-full bg-blue-600 text-white py-2 rounded-lg flex items-center justify-center hover:bg-blue-700 transition shadow-md"
                                >
                                    <svg
                                        xmlns="http://www.w3.org/2000/svg"
                                        className="h-5 w-5 mr-2"
                                        viewBox="0 0 20 20"
                                        fill="currentColor"
                                    >
                                        <path
                                            fillRule="evenodd"
                                            d="M10 18a8 8 0 100-16 8 8 0 000 16zm1-11a1 1 0 10-2 0v3a1 1 0 001 1h2a1 1 0 100-2h-1V7z"
                                            clipRule="evenodd"
                                        />
                                    </svg>
                                    See Details
                                </button>
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        </section>
    );
};

export default Home;
