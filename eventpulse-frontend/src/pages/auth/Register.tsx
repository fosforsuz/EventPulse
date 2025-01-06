import {Form, Input, Button} from "antd";
import {AuthLayout} from "../../layouts/AuthLayout";
import logo from "../../assets/eventPulseTransparent.svg";


const Register = () => {

    const onFinish = (values: unknown) => {
        console.log("Form Values:", values);
    }

    return (
        <AuthLayout>
            <div className="card w-1/5">

                {/* Logo */}
                <div className="flex flex-col items-center mb-4 sm:mb-6">
                    <img src={logo} alt="Event Pulse Logo" width={300} className="mb-2"/>
                    <p className="text-sm text-gray-500">Feel the Pulse of Your Events</p>
                </div>

                {/* Heading */}
                <h1 className="text-danger text-2xl font-bold mb-4 text-center">Register</h1>

                {/* Register Form */}
                <Form
                    name="login"
                    layout="vertical"
                    initialValues={{remember: false}}
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
                        <Input placeholder="Enter your email" className="rounded-lg"/>
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
                        <Input placeholder="Enter your name" className="rounded-lg"/>
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
                        <Input.Password placeholder="Enter your password" className="rounded-lg"/>
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
                        <Input.Password placeholder="Confirm your password" className="rounded-lg"/>
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
            </div>
        </AuthLayout>
    )
}

export default Register;