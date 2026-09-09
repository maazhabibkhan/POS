const DB_NAME = "SmallPOSDB";
const DB_VERSION = 1;
const PRODUCT_STORE = "products";

export const openDatabase = () => {
    return new Promise((resolve, reject) => {
        const request = indexedDB.open(DB_NAME, DB_VERSION);

        request.onupgradeneeded = (event) => {
            const db = event.target.result;

            if (!db.objectStoreNames.contains(PRODUCT_STORE)) {
                db.createObjectStore(PRODUCT_STORE, {
                    keyPath: "id"
                });
            }
        };

        request.onsuccess = () => {
            resolve(request.result);
        };

        request.onerror = () => {
            reject(request.error);
        };
    });
};

export const saveProducts = async (products) => {
    const db = await openDatabase();

    return new Promise((resolve, reject) => {
        const transaction = db.transaction(
            PRODUCT_STORE,
            "readwrite"
        );

        const store = transaction.objectStore(PRODUCT_STORE);

        products.forEach((product) => {
            store.put(product);
        });

        transaction.oncomplete = () => {
            resolve();
        };

        transaction.onerror = () => {
            reject(transaction.error);
        };
    });
};

export const getProductsFromDb = async () => {
    const db = await openDatabase();

    return new Promise((resolve, reject) => {
        const transaction = db.transaction(
            PRODUCT_STORE,
            "readonly"
        );

        const store = transaction.objectStore(PRODUCT_STORE);
        const request = store.getAll();

        request.onsuccess = () => {
            resolve(request.result);
        };

        request.onerror = () => {
            reject(request.error);
        };
    });
};

export const saveProductToDb = async (product) => {
    const db = await openDatabase();

    return new Promise((resolve, reject) => {
        const transaction = db.transaction(
            PRODUCT_STORE,
            "readwrite"
        );

        const store = transaction.objectStore(PRODUCT_STORE);

        store.put(product);

        transaction.oncomplete = () => {
            resolve();
        };

        transaction.onerror = () => {
            reject(transaction.error);
        };
    });
};

export const deleteProductFromDb = async (id) => {
    const db = await openDatabase();

    return new Promise((resolve, reject) => {
        const transaction = db.transaction(
            PRODUCT_STORE,
            "readwrite"
        );

        const store = transaction.objectStore(PRODUCT_STORE);

        store.delete(id);

        transaction.oncomplete = () => {
            resolve();
        };

        transaction.onerror = () => {
            reject(transaction.error);
        };
    });
};
