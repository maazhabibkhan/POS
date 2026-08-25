import { apiService } from "../../../api/apiService";

const PRODUCT_URL = "/Product";

export const getProducts = () => {
    return apiService.get(PRODUCT_URL);
};

export const getProduct = (id) => {
    return apiService.get(`${PRODUCT_URL}/${id}`);
};

export const createProduct = (product) => {
    return apiService.post(PRODUCT_URL, product);
};

export const updateProduct = (id, product) => {
    return apiService.put(`${PRODUCT_URL}/${id}`, product);
};

export const deleteProduct = (id) => {
    return apiService.delete(`${PRODUCT_URL}/${id}`);
};