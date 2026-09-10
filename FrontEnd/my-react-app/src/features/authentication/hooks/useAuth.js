import { useState } from "react";

import { login as loginApi } from "../services/authApi";

import {
    AUTH_TOKEN_KEY,
    AUTH_USER_KEY,
} from "../constants/authConstants";

const useAuth = () => {

    const [user, setUser] = useState(() => {
        const savedUser = localStorage.getItem(AUTH_USER_KEY);

        return savedUser
            ? JSON.parse(savedUser)
            : null;
    });

    const login = async (username, password) => {
        const response = await loginApi({
            username,
            password,
        });

        localStorage.setItem(
            AUTH_TOKEN_KEY,
            response.token
        );

        const userData = {
            userId: response.userId,
            username: response.username,
            roleId: response.roleId,
            roleName: response.roleName,
        };

        localStorage.setItem(
            AUTH_USER_KEY,
            JSON.stringify(userData)
        );

        setUser(userData);

        return response;
    };

    const logout = () => {
        localStorage.removeItem(AUTH_TOKEN_KEY);
        localStorage.removeItem(AUTH_USER_KEY);
        setUser(null);
    };

    const isAuthenticated = Boolean(
        localStorage.getItem(AUTH_TOKEN_KEY)
    );

    const isAdmin = user?.roleName === "Admin";

    return {
        user,
        login,
        logout,
        isAuthenticated,
        isAdmin,
    };
};

export default useAuth;
