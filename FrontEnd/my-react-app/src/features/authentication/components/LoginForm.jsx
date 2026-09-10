import { useState } from "react";

import useAuth from "../hooks/useAuth";

const LoginForm = () => {

    const { login } = useAuth();

    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");
    const [loading, setLoading] = useState(false);

    const handleSubmit = async (event) => {
        event.preventDefault();

        setError("");
        setLoading(true);

        try {
            await login(username, password);

            window.location.href = "/products";
        } catch (error) {
            if (error.response?.status === 401) {
                setError("Invalid username or password.");
            } else {
                setError("Unable to login. Please try again.");
            }
        } finally {
            setLoading(false);
        }
    };

    return (
        <form onSubmit={handleSubmit}>

            <div>
                <label>
                    Username
                </label>

                <input
                    type="text"
                    value={username}
                    onChange={(event) =>
                        setUsername(event.target.value)
                    }
                    placeholder="Enter username"
                    required
                />
            </div>

            <div>
                <label>
                    Password
                </label>

                <input
                    type="password"
                    value={password}
                    onChange={(event) =>
                        setPassword(event.target.value)
                    }
                    placeholder="Enter password"
                    required
                />
            </div>

            {error && (
                <p>
                    {error}
                </p>
            )}

            <button
                type="submit"
                disabled={loading}
            >
                {loading
                    ? "Logging in..."
                    : "Login"}
            </button>

        </form>
    );
};

export default LoginForm;
