const Select = ({
    label,
    name,
    value,
    options = [],
    onChange,
    error,
    disabled = false,
    required = false
}) => {

    return (
        <div className="form-group">

            {label && (
                <label htmlFor={name}>
                    {label}

                    {required && " *"}
                </label>
            )}

            <select
                id={name}
                name={name}
                value={value}
                onChange={onChange}
                disabled={disabled}
                aria-invalid={Boolean(error)}
            >

                <option value="">
                    Select {label}
                </option>

                {options.map((option) => (
                    <option
                        key={option.value}
                        value={option.value}
                    >
                        {option.label}
                    </option>
                ))}

            </select>

            {error && (
                <span className="error-message">
                    {error}
                </span>
            )}

        </div>
    );
};

export default Select;