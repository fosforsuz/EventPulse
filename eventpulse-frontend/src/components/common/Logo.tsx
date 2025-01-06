import logo from "../../assets/eventPulseTransparent.svg";

export const Logo = () => {
    return (
        <div className="flex flex-col items-center mb-4 sm:mb-6">
            <img src={logo} alt="Event Pulse Logo" width={300} className="mb-2"/>
            <p className="text-sm text-gray-500">Feel the Pulse of Your Events</p>
        </div>
    )
}
