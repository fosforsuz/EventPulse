import { Button, Form, Input } from "antd";
import React from "react";

interface PasswordResetFormProps {
  onFinish: (values: Record<string, string>) => void;
}

export const PasswordResetForm: React.FC<PasswordResetFormProps> = ({
  onFinish,
}) => {
  return (
    <Form
      name="passwordReset"
      layout="vertical"
      initialValues={{ remember: false }}
      autoComplete="off"
      onFinish={onFinish}
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
  );
};
