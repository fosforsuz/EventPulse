export const confirmPassword = (password: string, confirmPassword: string): boolean => {
    return password === confirmPassword;
}

export const validatePassword = (password: string): boolean => {
    // Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one number and one special character
    return /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/.test(password);
}

