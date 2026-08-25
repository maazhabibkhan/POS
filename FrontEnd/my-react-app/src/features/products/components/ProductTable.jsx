import DataTable from "../../../components/table/DataTable";

import { getProductColumns } from "../config/productColumns";

const ProductTable = ({
    products,
    onEdit,
    onDelete,
    loading
}) => {

    const columns = getProductColumns({
        onEdit,
        onDelete
    });

    return (
        <DataTable
            columns={columns}
            data={products}
            loading={loading}
            emptyMessage="No products found"
        />
    );
};

export default ProductTable;