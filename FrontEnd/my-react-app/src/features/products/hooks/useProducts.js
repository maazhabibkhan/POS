import { useEffect, useState } from "react";

import {
    getProducts,
    createProduct,
    updateProduct,
    deleteProduct
} from "../services/productApi";

import {
    saveProducts,
    getProductsFromDb,
    saveProductToDb,
    deleteProductFromDb
} from "../../../db/indexedDb";

import { createProductModel } from "../models/productModel";
import { validateProduct } from "../validation/productValidation";

import { handleApiError } from "../../../api/apiErrorHandler";

const useProducts = () => {

    const [products, setProducts] = useState([]);

    const [product, setProduct] = useState(
        createProductModel()
    );

    const [filters, setFilters] = useState({
        search: "",
        categoryId: "",
        status: ""
    });

    const [errors, setErrors] = useState({});

    const [loading, setLoading] = useState(false);

    const [apiError, setApiError] = useState(null);

    const loadProducts = async () => {
        try {
            setLoading(true);
            setApiError(null);

            const data = await getProducts();

            await saveProducts(data);

            const productsFromDb = await getProductsFromDb();

            setProducts(productsFromDb);

        } catch (error) {
            setApiError(handleApiError(error));

        } finally {
            setLoading(false);
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        const validationErrors = validateProduct(product);

        setErrors(validationErrors);

        if (Object.keys(validationErrors).length > 0) {
            return false;
        }

        try {
            setLoading(true);
            setApiError(null);

            if (product.id) {
                const updatedProduct = await updateProduct(
                    product.id,
                    product
                );

                await saveProductToDb(updatedProduct);

            } else {
                const createdProduct = await createProduct(product);

                await saveProductToDb(createdProduct);
            }

            const productsFromDb = await getProductsFromDb();

            setProducts(productsFromDb);

            resetProduct();

            return true;

        } catch (error) {
            setApiError(handleApiError(error));

            return false;

        } finally {
            setLoading(false);
        }
    };

    const handleDelete = async (id) => {
        try {
            setLoading(true);
            setApiError(null);

            await deleteProduct(id);

            await deleteProductFromDb(id);

            const productsFromDb = await getProductsFromDb();

            setProducts(productsFromDb);

        } catch (error) {
            setApiError(handleApiError(error));

        } finally {
            setLoading(false);
        }
    };

    const handleProductChange = (e) => {
        const { name, value } = e.target;

        setProduct((prevProduct) => ({
            ...prevProduct,
            [name]: value
        }));
    };

    const handleFilterChange = (e) => {
        const { name, value } = e.target;

        setFilters((prevFilters) => ({
            ...prevFilters,
            [name]: value
        }));
    };

    const resetProduct = () => {
        setProduct(createProductModel());
        setErrors({});
    };

    useEffect(() => {
        loadProducts();
    }, []);

    const filteredProducts = products.filter((product) => {
        const searchText = filters.search.toLowerCase();

        const matchesSearch =
            !filters.search ||
            product.name.toLowerCase().includes(searchText) ||
            product.sku.toLowerCase().includes(searchText);

        const matchesStatus =
            !filters.status ||
            product.status === filters.status;

        return matchesSearch && matchesStatus;
    });

    return {
        products,
        filteredProducts,
        product,
        filters,
        errors,
        loading,
        apiError,

        handleProductChange,
        handleFilterChange,
        handleSubmit,
        handleDelete,

        resetProduct,
        setProduct
    };
};

export default useProducts;
