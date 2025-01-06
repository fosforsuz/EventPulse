import { Button, Form, Input } from "antd";

interface PasswordResetRequestFormProps {
  onFinish: (values: Record<string, string>) => void;
}

export const PasswordResetRequestForm: React.FC<
  PasswordResetRequestFormProps
> = ({ onFinish }) => {
  return (
    <Form
      name="passwordReset"
      layout="vertical"
      initialValues={{ remember: false }}
      autoComplete="off"
      onFinish={onFinish}
    >
      {/* Email Field */}
      <Form.Item
        label="Email"
        name="email"
        rules={[
          {
            required: true,
            type: "email",
            message: "Please input a valid email!",
          },
        ]}
      >
        <Input placeholder="Enter your email" className="rounded-lg" />
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
  );
};
