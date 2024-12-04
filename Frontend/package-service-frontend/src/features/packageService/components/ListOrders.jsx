import { useEffect, useState } from "react";
import { useOrders } from "../hooks/usesOrder";
import { ShowOrderData } from "./ShowOrderData";
import { formatDate } from "../../../utils/format-date";

export const ListOrder = () => {
  const { orders, loadOrders, loading } = useOrders();
  const [searchId, setSearchId] = useState(""); // Estado para almacenar el ID de búsqueda
  const [filteredOrders, setFilteredOrders] = useState([]);

  // Cargar órdenes al iniciar el componente
  useEffect(() => {
    loadOrders();
  }, [loadOrders]);

  // Filtrar las órdenes en base al ID buscado
  useEffect(() => {
    if (!loading && orders?.data) {
      const filtered = searchId
        ? orders.data.filter((order) =>
            order.orderId?.toLowerCase().includes(searchId.toLowerCase())
          )
        : orders.data;
      setFilteredOrders(filtered);
    }
  }, [orders, searchId, loading]);

  const handleSearchChange = (e) => {
    setSearchId(e.target.value);
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-2xl font-bold mb-4">List of Orders</h1>
      <div className="mb-4">
        <input
          type="text"
          value={searchId}
          onChange={handleSearchChange}
          placeholder="Search by Order ID"
          className="px-4 py-2 border border-gray-300 rounded-lg"
        />
      </div>
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
            {filteredOrders.length > 0 ? (
              filteredOrders.map((order) => (
                <tr key={order.orderId}>
                  <td className="border px-4 text-center py-2">{order.orderId}</td>
                  <td className="border px-4 text-center py-2">
                    {order.orderDate ? formatDate(order.orderDate) : "N/A"}
                  </td>
                  <td className="border px-4 text-center py-2">{order.senderName || "N/A"}</td>
                  <td className="border px-4 text-center py-2">{order.receiverName || "N/A"}</td>
                  <td className="border px-4 text-center py-2">{order.address || "N/A"}</td>
                  <td className="border px-4 text-center py-2">
                    <ShowOrderData Order={order} />
                  </td>
                </tr>
              ))
            ) : (
              <tr>
                <td colSpan="6" className="border px-4 py-2 text-center">
                  No Orders Available
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
};
