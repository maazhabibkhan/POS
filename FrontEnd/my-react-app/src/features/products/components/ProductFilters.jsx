import Input from "../../../components/common/Input";
import Select from "../../../components/common/Select";
import { PRODUCT_STATUS } from "../constants/productConstants";

const ProductFilters = ({
    filters,
    categories,
    onChange
}) => {

    const statusOptions = Object.values(PRODUCT_STATUS).map((status) => ({
        value: status,
        label: status
    }));

    return (
        <div className="product-filters">

            <Input
                label="Search"
                name="search"
                value={filters.search}
                placeholder="Search by name Or SKU"
                onChange={onChange}
            />

            <Select
                label="Category"
                name="categoryId"
                value={filters.categoryId}
                onChange={onChange}
                options={categories}
            />

            <Select
                label="Status"
                name="status"
                value={filters.status}
                onChange={onChange}
                options={statusOptions}
            />

        </div>
    );
};

export default ProductFilters;