import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";

import Products from "../features/products/pages/Products";


const AppRoutes = () => {

    return (
        <BrowserRouter>

            <Routes>

                <Route
                    path="/products"
                    element={<Products />}
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