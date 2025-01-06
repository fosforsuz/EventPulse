import { Button, Form, Input } from "antd";
import { AuthLayout } from "../layouts/AuthLayout";
import { useParams } from "react-router-dom";
import logo from "../assets/eventPulseTransparent.svg";

export const PasswordReset = () => {
  const { token } = useParams<{ token: string }>(); 

  const handlePasswordReset = (values: unknown) => {
    console.log("Received values of form: ", values);
    console.log(token);
  };

  return (
    <AuthLayout>
      <div className="card w-1/5">
        {/* Logo */}
        <div className="flex flex-col items-center mb-4 sm:mb-6">
          <img src={logo} alt="Event Pulse Logo" width={300} className="mb-2" />
          <p className="text-sm text-gray-500">Feel the Pulse of Your Events</p>
        </div>

        {/* Heading */}
        <h1 className="text-danger text-2xl font-bold mb-4 text-center">
          Password Reset
        </h1>

        {/*Password Reset Form*/}
        <Form
          name="passwordReset"
          layout="vertical"
          initialValues={{ remember: false }}
          autoComplete="off"
          onFinish={handlePasswordReset}
        >
          {/*Password Field*/}
          <Form.Item
            label="Password"
            name="Password"
            rules={[
              {
                required: true,
                message: "Please input your password!",
              },
            ]}
          >
            <Input.Password
              placeholder="Enter your password"
              className="rounded-lg"
            />
          </Form.Item>

          {/*Confirm Password Field*/}
          <Form.Item
            label="Confirm Password"
            name="Confirm Password"
            rules={[
              {
                required: true,
                message: "Please input your password!",
              },
            ]}
          >
            <Input.Password
              placeholder="Confirm your password"
              className="rounded-lg"
            />
          </Form.Item>

          {/* Submit Button */}
          <Form.Item>
            <Button
              type="primary"
              htmlType="submit"
              className="w-full bg-green-500 hover:bg-green-600 transition duration-300 rounded-lg"
            >
              Reset Password
            </Button>
          </Form.Item>
        </Form>
      </div>
    </AuthLayout>
  );
};
