const Button = ({
    children,
    type = "button",
    onClick,
    variant = "primary",
    loading = false,
    disabled = false
}) => {

    return (
        <button
            type={type}
            onClick={onClick}
            disabled={disabled || loading}
            className={`btn btn-${variant}`}
        >
            {loading ? "Saving..." : children}
        </button>
    );
};

export default Button;