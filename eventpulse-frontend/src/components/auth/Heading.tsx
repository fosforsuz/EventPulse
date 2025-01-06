import React from "react";

interface HeadingProps {
    title: string;
}

const Heading: React.FC<HeadingProps> = ({title}) => {
    return <h1 className="text-danger text-2xl font-bold mb-4 text-center">{title}</h1>;
};

export default Heading;
