import { useState } from "react";

import useProducts from "../hooks/useProducts";

import ProductForm from "../components/ProductForm";
import ProductTable from "../components/ProductTable";
import ProductFilters from "../components/ProductFilters";

import Modal from "../../../components/common/Modal";
import Button from "../../../components/common/Button";


function Products() {

    const {
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
    } = useProducts();


    const [showForm, setShowForm] = useState(false);


    const handleAddProduct = () => {

        resetProduct();

        setShowForm(true);

    };


    const handleCloseForm = () => {

        resetProduct();

        setShowForm(false);

    };


    const handleEdit = (product) => {

        setProduct(product);

        setShowForm(true);

    };


    const handleProductSubmit = async (e) => {

        const success = await handleSubmit(e);

        if (success) {
            setShowForm(false);
        }

    };


    return (
        <div className="products-page">

            <div className="page-header">

                <div>

                    <h1>
                        Products
                    </h1>

                    <p>
                        Manage your products and inventory
                    </p>

                </div>


                <Button onClick={handleAddProduct}>
                    Add Product
                </Button>

            </div>


            {apiError && (
                <div className="error-message">
                    {apiError.message}
                </div>
            )}


            <ProductFilters
                filters={filters}
                onChange={handleFilterChange}
                categories={[]}
            />


            <ProductTable
                products={filteredProducts}
                onEdit={handleEdit}
                onDelete={handleDelete}
                loading={loading}
            />


            {showForm && (

                <Modal
                    title={
                        product.id
                            ? "Edit Product"
                            : "Add Product"
                    }
                    onClose={handleCloseForm}
                >

                    <ProductForm
                        product={product}
                        categories={[]}
                        onChange={handleProductChange}
                        onSubmit={handleProductSubmit}
                        errors={errors}
                        loading={loading}
                    />

                </Modal>

            )}

        </div>
    );
}


export default Products;