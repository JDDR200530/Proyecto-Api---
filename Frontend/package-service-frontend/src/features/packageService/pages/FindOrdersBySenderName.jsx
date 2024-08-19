import { useParams, useNavigate } from "react-router-dom";
import { useOrders } from "../hooks/usesOrder";
import { useEffect, useState } from "react";

export const FindOrdersBySenderName = () => {
  const { senderName } = useParams(); // Obtiene el parámetro 'senderName' de la URL
  const navigate = useNavigate(); // Hook para la navegación
  const { orders, loadOrders, loading } = useOrders();
  const [filteredOrders, setFilteredOrders] = useState([]);

  useEffect(() => {
    const fetchOrders = async () => {
      try {
        await loadOrders();
      } catch (error) {
        console.error("Error al cargar las órdenes:", error);
      }
    };

    fetchOrders();
  }, [loadOrders]);

  useEffect(() => {
    if (!loading && orders?.data?.length > 0) {
      const foundOrders = orders.data.filter(
        (order) => order.senderName.toLowerCase() === senderName.trim().toLowerCase()
      );
      setFilteredOrders(foundOrders);
    }
  }, [orders, loading, senderName]);

  if (loading) {
    return <div>Loading...</div>;
  }

  if (filteredOrders.length === 0) {
    return (
      <div>
        <h1>No se encontraron órdenes para el remitente ingresado</h1>
        <button
          onClick={() => navigate('/')}
          className="mt-4 bg-gray-500 text-white px-4 py-2 rounded hover:bg-gray-700"
        >
          Regresar al Portal
        </button>
      </div>
    );
  }

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-2xl font-bold mb-4">Órdenes del Remitente</h1>
      {filteredOrders.map((order) => (
        <div key={order.orderId} className="mb-4 p-4 border rounded-lg">
          <p><strong>Orden Id: </strong>{order.orderId}</p>
          <p><strong>Fecha de Orden: </strong>{order.orderDate}</p>
          <p><strong>Nombre del Remitente: </strong>{order.senderName}</p>
          <p><strong>Nombre del Destinatario: </strong>{order.receiverName}</p>
          <p><strong>Dirección: </strong>{order.address}</p>
        </div>
      ))}
      <button
        onClick={() => navigate('/portal')}
        className="mt-4 bg-gray-500 text-white px-4 py-2 rounded hover:bg-gray-700"
      >
        Regresar al Portal
      </button>
    </div>
  );
};
