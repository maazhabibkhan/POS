const DataTable = ({
    columns,
    data = [],
    loading = false,
    emptyMessage = "No data found"
}) => {

    if (loading) {
        return (
            <div className="table-loading">
                Loading...
            </div>
        );
    }


    return (
        <div className="table-container">

            <table>

                <thead>

                    <tr>

                        {columns.map((column) => (
                            <th key={column.key}>
                                {column.label}
                            </th>
                        ))}

                    </tr>

                </thead>


                <tbody>

                    {data.length === 0 ? (

                        <tr>

                            <td colSpan={columns.length}>
                                {emptyMessage}
                            </td>

                        </tr>

                    ) : (

                        data.map((row) => (

                            <tr key={row.id}>

                                {columns.map((column) => (

                                    <td key={column.key}>

                                        {column.render
                                            ? column.render(row)
                                            : row[column.key]
                                        }

                                    </td>

                                ))}

                            </tr>

                        ))

                    )}

                </tbody>

            </table>

        </div>
    );
};


export default DataTable;