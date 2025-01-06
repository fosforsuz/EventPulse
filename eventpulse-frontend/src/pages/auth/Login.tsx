import React from "react";
import {AuthLayout} from "../../layouts/AuthLayout";
import Heading from "../../components/auth/Heading.tsx";
import {Logo} from "../../components/auth/Logo.tsx";
import LoginForm from "../../components/forms/LoginForm.tsx";
import {LoginFooter} from "../../components/footers/LoginFooter.tsx";


const Login: React.FC = () => {
    const onFinish = (values: Record<string, string>) => {
        console.log("Form Values:", values);
    };

    return (
        <AuthLayout>
            <div className="card w-1/5">
                <Logo/>
                <Heading title="Login"/>
                <LoginForm onFinish={onFinish}/>
                <LoginFooter/>
            </div>
        </AuthLayout>
    );
};

export default Login;
