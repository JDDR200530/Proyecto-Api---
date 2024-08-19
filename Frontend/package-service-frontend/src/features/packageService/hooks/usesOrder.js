import { useState, useCallback } from "react";
import { getOrdersList, editOrder, deleteOrder } from "../../../shared/actions/post/post";

export const useOrders = () => {
    const [orders, setOrders] = useState({ data: [] });
    const [loading, setLoading] = useState(false);
    const [loaded, setLoaded] = useState(false);
    const loadOrders = useCallback(async () => {
        if (loaded) return;

        try {
            setLoading(true);
            const result = await getOrdersList();
            setOrders(result);
            setLoaded(true);
        } catch (error) {
            console.error("Error loading orders:", error);
        } finally {
            setLoading(false);
        }
    }, [loaded]);

    const updateOrder = useCallback(async (orderId, updatedData) => {
        try {
            setLoading(true);
            const response = await editOrder(orderId, updatedData);
            if (response && response.Data) {
                setOrders((prevState) => {
                    const updatedOrders = prevState.data.map((order) =>
                        order.id === orderId ? { ...order, ...response.Data } : order
                    );
                    return { data: updatedOrders };
                });
            }
        } catch (error) {
            console.error("Error updating order:", error);
        } finally {
            setLoading(false);
        }
    }, []);

    const removeOrder = useCallback(async (orderId) => {
        try {
            setLoading(true);
            await deleteOrder(orderId);

            setOrders((prevState) => {
                const updatedOrders = prevState.data.filter((order) => order.id !== orderId);
                return { data: updatedOrders };
            });
        } catch (error) {
            console.error("Error deleting order:", error);
        } finally {
            setLoading(false);
        }
    }, []);
    

    return {
        orders,
        loadOrders,
        updateOrder,
        removeOrder,
        loading,
    };
};
