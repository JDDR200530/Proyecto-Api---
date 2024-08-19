import { useState } from "react";
import { createOrder } from "../../../shared/actions/post/post"; 

export const useOrders = () => {
  const [orders, setOrders] = useState({});  

  const createOrders = async (orderData) => {  
    try {
      const result = await createOrder(orderData);  
      setOrders(result);  
    } catch (error) {
      console.error("Error creating order:", error);  
    }
  };

  return {
    orders,
    createOrders,  // Devuelve la función para que pueda ser llamada desde los componentes
  };
};
