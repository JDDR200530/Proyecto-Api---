import { useState } from "react";
import { getOrdersList , createOrder, getPackageList} from "../../../shared/actions/post/post";

export const useOrders = () => {
  const [orders, setOrder] = useState({});  // Inicializado con data como array vacío
  const [packages, setPackages] = useState({});  // Inicializado con data como array vacío

  const loadOrders = async () => {
    try {
      const result = await getOrdersList();
      setOrder(result);  // Asegúrate de que `result` contenga la estructura correcta
    } catch (error) {
      console.error("Error loading orders:", error);
    }
  }

  const loadPackages = async () => {
    try {
      const result = await getPackageList();
      setPackages(result);  // Asegúrate de que `result` contenga la estructura correcta
    } catch (error) {
      console.error("Error loading orders:", error);
    }
  }

  const createOrders = async (orderData) => {  // Recibe los datos de la orden como parámetro
    try {

      const result = await createOrder(orderData);
      console.log(result)  // Llamar correctamente a la función `createOrder`
    // Asigna el resultado a la variable de estado
   
      return result;
    } catch (error) {
      console.error("Error creating order:", error);  // Mensaje correcto para la creación de órdenes
    }
  };

  return {
    orders,
    packages,
    loadOrders,
    createOrders,
    loadPackages,
  };
  
};