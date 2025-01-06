import { Button, Form, Input } from "antd";
import React from "react";

interface RegisterFormProps {
  onFinish: (values: Record<string, string>) => void;
}

export const RegisterForm: React.FC<RegisterFormProps> = ({ onFinish }) => {
  return (
    <Form
      name="login"
      layout="vertical"
      initialValues={{ remember: false }}
      onFinish={onFinish}
      autoComplete="off"
    >
      {/*Email Field*/}
      <Form.Item
        label="Email"
        name="Email"
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

      {/*Name Field*/}
      <Form.Item
        label="Name"
        name="Name"
        rules={[
          {
            required: true,
            message: "Please input your name!",
          },
        ]}
      >
        <Input placeholder="Enter your name" className="rounded-lg" />
      </Form.Item>

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

      {/*Submit Button*/}
      <Form.Item>
        <Button
          type="primary"
          htmlType="submit"
          className="w-full bg-green-500 hover:bg-green-600 transition duration-300 rounded-lg"
        >
          Register
        </Button>
      </Form.Item>
    </Form>
  );
};
