import { AuthLayout } from "../../layouts/AuthLayout.tsx";
import { Card } from "../../components/auth/Card.tsx";
import { Logo } from "../../components/auth/Logo.tsx";
import Heading from "../../components/auth/Heading.tsx";
import { PasswordResetRequestForm } from "../../components/forms/PasswordResetRequestForm.tsx";

const PasswordResetRequest = () => {
  const onFinish = (values: unknown) => {
    console.log("Form Values:", values);
  };

  return (
    <AuthLayout>
      <Card>
        <Logo />
        <Heading title="Password Reset" />
        <PasswordResetRequestForm onFinish={onFinish} />
      </Card>
    </AuthLayout>
  );
};

export default PasswordResetRequest;
