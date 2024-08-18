// src/features/packageService/components/ListOrder.jsx
const ListOrder = ({ orders = [] }) => {
  const formatDate = (dateString) => {
    const date = new Date(dateString);
    return date.toISOString();  // Puedes ajustar el formato según sea necesario
  };

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-2xl font-bold mb-4">List of Orders</h1>
      <div className="overflow-x-auto">
        <table className="table-auto w-full bg-orange-200 text-gray-700 rounded-lg">
          <thead>
            <tr>
              <th className="px-4 py-2">Order ID</th>
              <th className="px-4 py-2">Order Date</th>
              <th className="px-4 py-2">Sender Name</th>
              <th className="px-4 py-2">Receiver Name</th>
              <th className="px-4 py-2">Address</th>
            </tr>
          </thead>
          <tbody>
            {orders.length > 0 ? (
              orders.map((order) => (
                <tr key={order.orderId}>
                  <td className="border px-4 py-2 text-center">{order.orderId}</td>
                  <td className="border px-4 py-2 text-center">{formatDate(order.orderDate)}</td>
                  <td className="border px-4 py-2 text-center">{order.senderName}</td>
                  <td className="border px-4 py-2 text-center">{order.receiverName}</td>
                  <td className="border px-4 py-2 text-center">{order.address}</td>
                </tr>
              ))
            ) : (
              <tr>
                <td colSpan="5" className="text-center">No orders available</td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default ListOrder;
