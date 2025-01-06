import { useState } from "react";
import { AuthLayout } from "../../layouts/AuthLayout";
import { Button, Card, Spin } from "antd";
import { Logo } from "../../components/auth/Logo";
import Heading from "../../components/auth/Heading";

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
      <Card>
        <Logo />
        <Heading title="Verify Email" />

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
      </Card>
    </AuthLayout>
  );
};
