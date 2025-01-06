import { Register } from "../../types/authTypes"; // Importing the Register type
import apiEndpoints from "../apiEndpoints"; // API endpoint configuration
import axios from "../axios"; // Axios instance
import { isAxiosError } from "axios"; // Axios error type guard

// Helper function for handling API calls
const handleApiRequest = async (
  request: () => Promise<Response>
): Promise<Response> => {
  try {
    const response = await request(); // Execute the API request
    return response; // Return the response data
  } catch (error: unknown) {
    // Handle errors consistently
    if (isAxiosError(error)) {
      throw error.response?.data || "An unexpected error occurred";
    } else {
      throw new Error("An unknown error occurred. Please try again later.");
    }
  }
};

// Login function
export const login = (username: string, password: string): Promise<Response> => {
  return handleApiRequest(() =>
    axios.post(apiEndpoints.auth.login, { username, password })
  );
};

// Register function
export const register = (registerModel: Register): Promise<Response> => {
  return handleApiRequest(() =>
    axios.post(apiEndpoints.auth.register, registerModel)
  );
};

// Forgot Password function
export const forgotPassword = (email: string): Promise<Response> => {
  return handleApiRequest(() =>
    axios.post(apiEndpoints.auth.forgotPassword, { email })
  );
};

// Reset Password function
export const resetPassword = (
  token: string,
  password: string
): Promise<Response> => {
  return handleApiRequest(() =>
    axios.post(apiEndpoints.auth.resetPassword, { token, password })
  );
};

// Send Verification Email function
export const sendVerificationEmail = (userId: number): Promise<Response> => {
  return handleApiRequest(() =>
    axios.post(apiEndpoints.auth.sendVerificationEmail, { id: userId })
  );
};

// Verify Email function
export const verifyEmail = (token: string): Promise<Response> => {
  return handleApiRequest(() =>
    axios.post(apiEndpoints.auth.verifyEmail, { token })
  );
};
