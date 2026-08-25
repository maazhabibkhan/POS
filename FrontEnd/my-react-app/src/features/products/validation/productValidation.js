export const validateProduct = (product) => {

    const errors = {};


    if (!product.name.trim()) {
        errors.name = "Product name is required";
    }


    if (!product.sku.trim()) {
        errors.sku = "SKU is required";
    }


    if (product.categoryId) {
        errors.categoryId = "Category is required";
    }


    if (product.purchasePrice < 0) {
        errors.purchasePrice = "Purchase price cannot be negative";
    }


    if (product.salePrice < 0) {
        errors.salePrice = "Sale price cannot be negative";
    }


    if (product.stock < 0) {
        errors.stock = "Stock cannot be negative";
    }


    if (!product.status) {
        errors.status = "Status is required";
    }


    return errors;
};