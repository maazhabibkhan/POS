const Input = ({
    label,
    name,
    type = "text",
    value,
    placeholder = "",
    onChange,
    error
}) => {

    return (
        <div className="form-group">

            <label>
                {label}
            </label>

            <input
                name={name}
                type={type}
                value={value}
                placeholder={placeholder}
                onChange={onChange}
            />

            {error && (
                <span className="error-message">
                    {error}
                </span>
            )}

        </div>
    );
};

export default Input;