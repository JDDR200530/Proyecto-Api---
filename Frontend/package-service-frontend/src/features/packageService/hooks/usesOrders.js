import { useState } from "react"
import { getOrdersList } from "../../../shared/actions/posts/posts";

export const useOrders = () => {
  const [posts, setOrders] = useState({});
  const [isLoading, setIsLoading] = useState (false);
  const loadOrder = async (searchTerm, page) => {
    setIsLoading(true);
    const result = await getOrdersList(searchTerm, page);
    setOrders(result);
    setIsLoading(false);
  } 

  return {
    // Properties
    posts,
    isLoading,
    // Methods
    loadOrder,
  }
}
