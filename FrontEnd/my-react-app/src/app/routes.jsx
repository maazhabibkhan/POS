import {
    BrowserRouter,
    Routes,
    Route,
    Navigate,
} from "react-router-dom";

import Products from "../features/products/pages/Products";
import Login from "../features/authentication/pages/Login";


const ProtectedRoute = ({ children }) => {

    const token = localStorage.getItem("token");

    if (!token) {
        return (
            <Navigate
                to="/login"
                replace
            />
        );
    }

    return children;
};


const AppRoutes = () => {

    return (
        <BrowserRouter>

            <Routes>

                <Route
                    path="/login"
                    element={
                        <Login />
                    }
                />

                <Route
                    path="/products"
                    element={
                        <ProtectedRoute>
                            <Products />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="*"
                    element={
                        <Navigate
                            to="/products"
                            replace
                        />
                    }
                />

            </Routes>

        </BrowserRouter>
    );
};


export default AppRoutes;
