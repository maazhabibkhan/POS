import Input from "../../../components/common/Input";
import Select from "../../../components/common/Select";
import Button from "../../../components/common/Button";

import { PRODUCT_STATUS } from "../constants/productConstants";


const ProductForm = ({
    product,
    categories,
    onChange,
    onSubmit,
    errors,
    loading
}) => {

    const statusOptions = Object.values(PRODUCT_STATUS).map((status) => ({
        value: status,
        label: status
    }));


    return (
        <form onSubmit={onSubmit}>

            <Input
                label="Product Name"
                name="name"
                value={product.name}
                onChange={onChange}
                error={errors.name}
                required
            />

            <Input
                label="SKU"
                name="sku"
                value={product.sku}
                onChange={onChange}
                error={errors.sku}
                required
            />

            <Select
                label="Category"
                name="categoryId"
                value={product.categoryId}
                onChange={onChange}
                options={categories}
                error={errors.categoryId}
                required
            />

            <Input
                label="Purchase Price"
                name="purchasePrice"
                type="number"
                value={product.purchasePrice}
                onChange={onChange}
                error={errors.purchasePrice}
                required
            />

            <Input
                label="Sale Price"
                name="salePrice"
                type="number"
                value={product.salePrice}
                onChange={onChange}
                error={errors.salePrice}
                required
            />

            <Input
                label="Stock"
                name="stock"
                type="number"
                value={product.stock}
                onChange={onChange}
                error={errors.stock}
                required
            />

            <Select
                label="Status"
                name="status"
                value={product.status}
                onChange={onChange}
                options={statusOptions}
                error={errors.status}
                required
            />

            <Button
                type="submit"
                loading={loading}
            >
                Save Product
            </Button>

        </form>
    );
};


export default ProductForm;