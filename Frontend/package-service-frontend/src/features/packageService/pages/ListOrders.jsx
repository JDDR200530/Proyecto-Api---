import { useState } from "react";
export const ListOrder = () => {
  const [orders, setOrders]= useState ([
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
  ]);
   const handleDeleteOrder = (OrderId) => {
      setOrders(orders.filter((order) => order.OrderId !== OrderId))
   }
  return (
    <div className="container mx-auto p-4 md:px-6 py-12">
      <h1 className="text-2xl font-bold mb-6">Lista de Ordenes</h1>
      <div className="grip gap-4 md:gap-6">
        {orders.map((order) =>
        (
            <div key={order.OrderId} className="bg-background border border-muted rounded-lg
            p-4 md:p-6 flex items-center justify-between" >
              <div className="grid gap-1">
                <p className="text-muted-foreground">{order.SenderName}</p>
                <p className="text-muted-foreground">{order.ReciverName}</p>
                <p className="text-muted-foreground">{order.Address}</p>
              </div>
              <div className="flex items-center gap-4">
                <p className="font-medium">${order.total.toFixed(2)}</p>

              </div>

            </div>
        ))}
        </div>
        </div>
  );
};

