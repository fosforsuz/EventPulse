import { AuthLayout } from "../../layouts/AuthLayout";
import { useParams } from "react-router-dom";
import { Card } from "../../components/auth/Card";
import { Logo } from "../../components/auth/Logo";
import Heading from "../../components/auth/Heading";
import { PasswordResetForm } from "../../components/forms/PasswordResetForm";

export const PasswordReset = () => {
  const { token } = useParams<{ token: string }>();

  const handlePasswordReset = (values: unknown) => {
    console.log("Received values of form: ", values);
    console.log(token);
  };

  return (
    <AuthLayout>
      <Card>
        <Logo />
        <Heading title="Password Reset" />
        <PasswordResetForm onFinish={handlePasswordReset} />
      </Card>
    </AuthLayout>
  );
};
