import { useEffect, useState } from "react";

import {
    getProducts,
    createProduct,
    updateProduct,
    deleteProduct
} from "../services/productApi";

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


    // =========================
    // GET PRODUCTS
    // =========================

    const loadProducts = async () => {

        try {

            setLoading(true);
            setApiError(null);

            const data = await getProducts();

            setProducts(data);

        } catch (error) {

            setApiError(
                handleApiError(error)
            );

        } finally {

            setLoading(false);

        }
    };


    // =========================
    // CREATE / UPDATE PRODUCT
    // =========================

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

                await updateProduct(
                    product.id,
                    product
                );

                setProducts((prevProducts) =>
                    prevProducts.map((item) =>
                        item.id === product.id
                            ? product
                            : item
                    )
                );

            

            } else {

                await createProduct(
                    product
                );

                setProducts((prevProducts) => [
                    ...prevProducts,
                    product
                ]);

            }


            resetProduct();

            return true;

        } catch (error) {

            setApiError(
                handleApiError(error)
            );

            return false;

        } finally {

            setLoading(false);

        }
    };


    // =========================
    // DELETE PRODUCT
    // =========================

    const handleDelete = async (id) => {

        try {

            setLoading(true);
            setApiError(null);

            await deleteProduct(id);

            setProducts((prevProducts) =>
                prevProducts.filter(
                    (product) => product.id !== id
                )
            );

        } catch (error) {

            setApiError(
                handleApiError(error)
            );

        } finally {

            setLoading(false);

        }
    };


    // =========================
    // PRODUCT CHANGE
    // =========================

    const handleProductChange = (e) => {

        const { name, value } = e.target;

        setProduct((prevProduct) => ({
            ...prevProduct,
            [name]: value
        }));

    };


    // =========================
    // FILTER CHANGE
    // =========================

    const handleFilterChange = (e) => {

        const { name, value } = e.target;

        setFilters((prevFilters) => ({
            ...prevFilters,
            [name]: value
        }));

    };


    // =========================
    // RESET PRODUCT
    // =========================

    const resetProduct = () => {

        setProduct(
            createProductModel()
        );

        setErrors({});

    };


    // =========================
    // LOAD PRODUCTS ON PAGE LOAD
    // =========================

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


            return (
                matchesSearch &&
                matchesStatus
            );

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