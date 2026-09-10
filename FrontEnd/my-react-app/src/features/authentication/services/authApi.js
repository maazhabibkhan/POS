import { apiService } from "../../../api/apiService";

export const login = async (loginData) => {
    return await apiService.post("/api/Auth/login", loginData);
};

export const register = async (registerData) => {
    return await apiService.post("/api/Auth/register", registerData);
};
