export interface ThemeContextType {
    theme: 'light' | 'dark' | 'custom';
    setTheme: (theme: 'light' | 'dark' | 'custom') => void;
}