import React from "react";
import {Form, Input, Button, Checkbox} from "antd";
import urlEndpoints from "../../constants/urlEndpoints.ts";

interface LoginFormProps {
    onFinish: (values: Record<string, string>) => void;
}

const LoginForm: React.FC<LoginFormProps> = ({onFinish}) => (
    <Form
        name="login"
        layout="vertical"
        initialValues={{remember: false}}
        onFinish={onFinish}
        autoComplete="off"
    >
        <Form.Item
            label="Email"
            name="email"
            rules={[
                {required: true, type: "email", message: "Please input a valid email!"},
            ]}
        >
            <Input placeholder="Enter your email" className="rounded-lg"/>
        </Form.Item>

        <Form.Item
            label="Password"
            name="password"
            rules={[{required: true, message: "Please input your password!"}]}
        >
            <Input.Password placeholder="Enter your password" className="rounded-lg"/>
        </Form.Item>

        <div className="flex items-center justify-between mb-4">
            <Form.Item name="remember" valuePropName="checked" noStyle>
                <Checkbox className="text-gray-600 hover:text-gray-800 transition">
                    Remember me
                </Checkbox>
            </Form.Item>
            <a
                href={urlEndpoints.passwordReset}
                className="text-blue-500 hover:underline text-sm"
            >
                Forgot Password?
            </a>
        </div>

        <Form.Item>
            <Button
                type="primary"
                htmlType="submit"
                className="w-full bg-green-500 hover:bg-green-600 transition duration-300 rounded-lg"
            >
                Login
            </Button>
        </Form.Item>
    </Form>
);

export default LoginForm;
