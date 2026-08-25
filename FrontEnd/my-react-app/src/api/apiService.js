import axiosClient from "./axiosClient";

export const apiService = {

    get: async (url, config = {}) => {
        const response = await axiosClient.get(url, config);
        return response.data;
    },

    post: async (url, data = {}, config = {}) => {
        const response = await axiosClient.post(url, data, config);
        return response.data;
    },

    put: async (url, data = {}, config = {}) => {
        const response = await axiosClient.put(url, data, config);
        return response.data;
    },

    delete: async (url, config = {}) => {
        const response = await axiosClient.delete(url, config);
        return response.data;
    }

};