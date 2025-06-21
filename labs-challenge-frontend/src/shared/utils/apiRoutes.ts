const baseURL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:8080';

export const ApiRoutes = {
  login: `${baseURL}/api/Authentication/login`,
  registerUser: `${baseURL}/api/Authentication/register`,
  validateToken: `${baseURL}/api/Authentication/validate-token`,
};