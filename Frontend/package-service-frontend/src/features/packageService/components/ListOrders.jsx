import { useEffect, useState } from "react";
import { useOrders } from "../hooks";
import { ShowOrderData } from "./ShowOrderData";
import { formatDate } from "../../../utils/format-date";
export const ListOrder = () => {
  
  const { orders, loadOrders } = useOrders();

  const [fetching, setFetching] = useState(true);

  useEffect(() => {
    if (fetching) {
      loadOrders();
      setFetching(false);
    }
  }, [fetching, loadOrders]);

  // Debugging: Check if orders data is available
  console.log(orders);

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-2xl font-bold mb-4">List of Orders</h1>
      <div className="overflow-x-auto">
        <table className="table-auto w-full text-gray-700 rounded-lg">
          <thead className="bg-gray-200 rounded-lg">
            <tr>
              <th className="px-4 py-2 text-center">Order ID</th>
              <th className="px-4 py-2 text-center">Date</th>
              
              <th className="px-4 py-2 text-center">Sender Name</th>
              <th className="px-4 py-2 text-center">Receiver Name</th>
              <th className="px-4 py-2 text-center">Address</th>
              <th className="px-4 py-2 text-center">Details</th>
            </tr>
          </thead>
          <tbody className="bg-gray-100">
  {orders?.data?.length > 0 ? (
    orders.data.map((order) => (
      <tr key={order.OrderId}>  {/* Usando OrderId como clave única */}
        <td className="border px-4 text-center py-2">{order.orderId}</td>
        <td className="border px-4 text-center py-2 ">{formatDate(order.orderDate)}</td>
        <td className="border px-4 text-center py-2">{order.senderName}</td>
        <td className="border px-4 text-center py-2">{order.receiverName}</td>
        <td className="border px-4 text-center py-2">{order.address}</td>
        <td className="border px-4 text-center py-2">
          <ShowOrderData
          
          
          Order={order} />
        </td>
      </tr>
    ))
  ) : (
    <tr>
      <td colSpan="5" className="border px-4 py-2 text-center">
        No Orders Available
      </td>
    </tr>
  )}
</tbody>
        </table>
      </div>
    </div>
// Show Orders


  );
};
