import { useState } from "react";
import { AuthLayout } from "../../layouts/AuthLayout";
import logo from "../assets/eventPulseTransparent.svg";
import { Button, Spin } from "antd";

export const VerifyEmail = () => {
  const [loading, setLoading] = useState<boolean>(false);

  const handleVerifyEmail = () => {
    setLoading(true);
    setTimeout(() => {
      setLoading(false);
    }, 3000);
  };

  return (
    <AuthLayout>
      <div className="card w-full max-w-md p-6 bg-white shadow-lg rounded-lg">
        {/* Logo */}
        <div className="flex flex-col items-center mb-6">
          <img src={logo} alt="Event Pulse Logo" width={200} className="mb-2" />
          <p className="text-sm text-gray-500">Feel the Pulse of Your Events</p>
        </div>

        {/* Heading */}
        <h1 className="text-red-500 text-2xl font-bold mb-4 text-center">
          Verify Your Email
        </h1>

        {/* Description */}
        <p className="text-center text-gray-600 mb-6">
          Click the button below to verify your email. Once verified, you can
          log in to your account.
        </p>

        {/* Verify Button */}
        <Button
          type="primary"
          className="w-full bg-green-500 hover:bg-green-600 transition duration-300 rounded-lg"
          onClick={handleVerifyEmail}
          disabled={loading}
        >
          {loading ? <Spin size="small" /> : "Verify Email"}
        </Button>
      </div>
    </AuthLayout>
  );
};
