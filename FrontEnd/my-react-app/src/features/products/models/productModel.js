export const createProductModel = ({
    id = null,
    name = "",
    sku = "",
    categoryId = 0,
    purchasePrice = 0,
    salePrice = 0,
    stock = 0,
    status = "Active"
} = {}) => {

    return {
        id,
        name,
        sku,
        categoryId,
        purchasePrice,
        salePrice,
        stock,
        status
    };
};