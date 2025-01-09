import {MainLayout as Layout} from "../layouts/MainLayout.tsx";
import React from "react";


const recentEvents = [
    {
        id: 1,
        title: "React Konferansı 2025",
        date: "10 Ocak 2025",
        location: "İstanbul",
        image: "https://via.placeholder.com/300x200", // Etkinlik resmi için placeholder
    },
    {
        id: 2,
        title: "JavaScript Workshop",
        date: "15 Ocak 2025",
        location: "Ankara",
        image: "https://via.placeholder.com/300x200",
    },
    {
        id: 3,
        title: "UI/UX Tasarım Atölyesi",
        date: "20 Ocak 2025",
        location: "İzmir",
        image: "https://via.placeholder.com/300x200",
    },
];


const SampleComponent: React.FC = () => {
    return (
        <Layout>
            <div className="bg-gray-50">
                {/* Hero Section */}
                <section className="bg-white py-16">
                    <div className="container mx-auto px-4 text-center">
                        <h1 className="text-4xl font-bold text-gray-800 mb-4">
                            Son Etkinlikleri Keşfedin
                        </h1>
                        <p className="text-gray-600 mb-8">
                            EventPulse ile en güncel etkinliklere göz atın ve bir sonraki
                            maceranızı planlayın.
                        </p>
                        <div className="flex justify-center">
                            <input
                                type="text"
                                placeholder="Lokasyona göre ara"
                                className="border rounded-lg px-4 py-2 w-1/2"
                            />
                            <button className="bg-blue-600 text-white px-6 py-2 rounded-lg ml-2">
                                Ara
                            </button>
                        </div>
                    </div>
                </section>

                {/* Recent Events */}
                <section className="py-16">
                    <div className="container mx-auto px-4">
                        <h2 className="text-2xl font-semibold text-gray-800 mb-8 text-center">
                            Son Eklenen Etkinlikler
                        </h2>
                        <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
                            {recentEvents.map((event) => (
                                <div
                                    key={event.id}
                                    className="bg-white shadow-md rounded-lg overflow-hidden"
                                >
                                    <img
                                        src={event.image}
                                        alt={event.title}
                                        className="w-full h-40 object-cover"
                                    />
                                    <div className="p-4">
                                        <h3 className="text-lg font-bold text-gray-800">
                                            {event.title}
                                        </h3>
                                        <p className="text-gray-600">{event.date}</p>
                                        <p className="text-gray-600">{event.location}</p>
                                        <button className="text-blue-600 hover:underline mt-4">
                                            Detayları Gör
                                        </button>
                                    </div>
                                </div>
                            ))}
                        </div>
                    </div>
                </section>

                {/* Footer */}
                <footer className="bg-gray-800 text-gray-200 py-8">
                    <div className="container mx-auto px-4 text-center">
                        <p>© 2025 EventPulse. Tüm hakları saklıdır.</p>
                        <p className="mt-2">
                            <a href="/privacy" className="text-blue-400 hover:underline">
                                Gizlilik Politikası
                            </a>{" "}
                            |{" "}
                            <a href="/terms" className="text-blue-400 hover:underline">
                                Kullanım Şartları
                            </a>
                        </p>
                    </div>
                </footer>
            </div>
        </Layout>
    );
};

export default SampleComponent;
