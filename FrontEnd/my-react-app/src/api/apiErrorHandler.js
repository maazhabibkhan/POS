export const handleApiError = (error) => {

    if (error.response) {

        return {
            status: error.response.status,
            message:
                error.response.data?.message ||
                "Backend is connected Something went wrong"
        };

    }

    if (error.request) {

        return {
            status: null,
            message: "Unable to connect to the server"
        };

    }

    return {
        status: null,
        message: error.message || "Something went wrong"
    };
};