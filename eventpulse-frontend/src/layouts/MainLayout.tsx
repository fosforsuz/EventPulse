import React from "react";
import {Layout} from "antd";
import "../styles/mainLayout.css";
import {BaseLayoutProps} from "../types/baseLayoutProps";
import Navbar from "./components/Navbar";
import Footer from "./components/Footer.tsx";

const {Content} = Layout;

export const MainLayout: React.FC<BaseLayoutProps> = ({children}) => {
    return (
        <Layout className="main-layout">
            <Navbar/>
            <Content className="main-content">{children}</Content>
            <Footer/>
        </Layout>
    );
};
