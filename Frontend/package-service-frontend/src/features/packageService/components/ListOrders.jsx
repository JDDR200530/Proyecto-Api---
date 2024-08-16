export const ListOrder = () => {
  const orders = [
    {
      OrderId: "dcde359e-ffcc-426b-96ba-77ab507c0946",
      SenderName: "Carlos Pineda",
      Address: "Santa Rosa de Copan",
      ReciverName: "Juan Perez",
    },
    {
      OrderId: "7e4d2f82-cb9f-4b67-8257-09a2f3fba4c9",
      SenderName: "Ana Morales",
      Address: "Tegucigalpa",
      ReciverName: "Luis Fernández",
    },
    {
      OrderId: "3f2c1d7f-5f0e-4c80-b06e-0d41f5bde7f5",
      SenderName: "Marta López",
      Address: "San Pedro Sula",
      ReciverName: "Jorge Martinez",
    },
    {
      OrderId: "d1e67c21-737f-487b-bb5a-7872d568e582",
      SenderName: "Pedro Gómez",
      Address: "La Ceiba",
      ReciverName: "María Rodríguez",
    },
    {
      OrderId: "8a4f2d85-8030-4d7d-bb76-f0a1b4b3e5d8",
      SenderName: "Sofia Castillo",
      Address: "Choluteca",
      ReciverName: "Roberto García",
    },
  ];

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-2xl font-bold mb-4">List of Orders</h1>
      <div className="overflow-x-auto">
        <table className="table-auto w-full bg-orange-200 text-gray-700 rounded-lg">
          <thead>
            <tr>
              <th className="px-4 py-2">Order ID</th>
              <th className="px-4 py-2">Sender Name</th>
              <th className="px-4 py-2">Receiver Name</th>
              <th className="px-4 py-2">Address</th>
            </tr>
          </thead>
          <tbody>
            {orders.map((order) => (
              <tr key={order.OrderId}>
                <td className="border px-4 py-2">{order.OrderId}</td>
                <td className="border px-4 py-2">{order.SenderName}</td>
                <td className="border px-4 py-2">{order.ReciverName}</td>
                <td className="border px-4 py-2">{order.Address}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default ListOrder;
