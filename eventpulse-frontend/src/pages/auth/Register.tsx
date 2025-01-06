import { AuthLayout } from "../../layouts/AuthLayout";
import { Card } from "../../components/auth/Card";
import { Logo } from "../../components/auth/Logo";
import Heading from "../../components/auth/Heading";
import { RegisterForm } from "../../components/forms/RegisterForm";

const Register = () => {
  const onFinish = (values: unknown) => {
    console.log("Form Values:", values);
  };

  return (
    <AuthLayout>
      <Card>
        <Logo />
        <Heading title="Register" />
        <RegisterForm onFinish={onFinish} />
      </Card>
    </AuthLayout>
  );
};

export default Register;
