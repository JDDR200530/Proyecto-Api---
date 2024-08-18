import { useState } from "react";
import { getOrdersList } from "../../../shared/actions/post/post";

export const useOrders = () => {
  const [orders, setOrder] = useState({ data: [] });  // Inicializado con data como array vacío

  const loadOrders = async () => {
    try {
      const result = await getOrdersList();
      setOrder(result);  // Asegúrate de que `result` contenga la estructura correcta
    } catch (error) {
      console.error("Error loading orders:", error);
    }
  }

  return {
    orders,
    loadOrders,
  };
};
