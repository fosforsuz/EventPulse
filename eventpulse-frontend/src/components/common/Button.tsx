import {Button as AntdButton} from "antd";
import React from "react";

interface ButtonProps {
    text: string;
    type: "primary" | "default" | "dashed" | "link" | "text";
    htmlType: "button" | "submit" | "reset" | undefined;
    onClick: () => void | undefined;
    className: string | undefined;
}

const Button: React.FC<ButtonProps> = ({text, type, htmlType, onClick, className}) => {

    let buttonStyle = "w-full  transition duration-300 rounded-lg"

    if (type == "primary")
        buttonStyle += " bg-green-500 hover:bg-green-600"
    else if (type == "default")
        buttonStyle += " bg-gray-500 hover:bg-gray-600"
    else if (type == "dashed")
        buttonStyle += " bg-blue-500 hover:bg-blue-600"
    else if (type == "link")
        buttonStyle += " bg-red-500 hover:bg-red-600"
    else if (type == "text")
        buttonStyle += " bg-yellow-500 hover:bg-yellow-600"

    if (className)
        buttonStyle += " " + className

    return (
        <AntdButton
            type={type}
            htmlType={htmlType}
            onClick={onClick}
            className={buttonStyle}
        >
            {text}
        </AntdButton>
    );
}

export default Button;