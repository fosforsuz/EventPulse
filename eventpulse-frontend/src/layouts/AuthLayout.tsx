import {BaseLayoutProps} from "../types/baseLayoutProps";
import {Layout} from "antd";
import React from "react";

const {Content} = Layout;

export const AuthLayout: React.FC<BaseLayoutProps> = ({children}) => {
    return (
        <Layout className="auth-layout">
            <Content className="auth-content">
                {children}
            </Content>
        </Layout>
    );
};