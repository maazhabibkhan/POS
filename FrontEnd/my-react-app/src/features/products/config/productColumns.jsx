import Button from "../../../components/common/Button";

export const getProductColumns = ({
    onEdit,
    onDelete
}) => [

    {
        key: "name",
        label: "Product"
    },

    {
        key: "sku",
        label: "SKU"
    },

    {
        key: "categoryId",
        label: "Category"
    },

    {
        key: "purchasePrice",
        label: "Purchase Price"
    },

    {
        key: "salePrice",
        label: "Sale Price"
    },

    {
        key: "stock",
        label: "Stock"
    },

    {
        key: "status",
        label: "Status"
    },

    {
        key: "actions",
        label: "Actions",

        render: (product) => (
            <div className="table-actions">

                <Button
                    variant="secondary"
                    onClick={() => onEdit(product)}
                >
                    Edit
                </Button>

                <Button
                    variant="danger"
                    onClick={() => onDelete(product.id)}
                >
                    Delete
                </Button>

            </div>
        )
    }

];