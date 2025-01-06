import React, {createContext, useContext, useState} from "react";
import {ConfigProvider} from "antd";
import {themes} from "./index.ts"
import {ThemeContextType} from "../types/themeContextProps.ts";

const ThemeContext = createContext<ThemeContextType | undefined>(undefined);

export const ThemeProvider: React.FC<{ children: React.ReactNode }> = ({children}) => {
    const [theme, setTheme] = useState<ThemeContextType["theme"]>("light");

    return (
        <ThemeContext.Provider value={{theme, setTheme}}>
            <ConfigProvider theme={themes[theme]}>
                {children}
            </ConfigProvider>
        </ThemeContext.Provider>
    );
};

export const useTheme = () => {
    const context = useContext(ThemeContext);

    if (!context) {
        throw new Error("useTheme must be used within a ThemeProvider");
    }

    return context;
}