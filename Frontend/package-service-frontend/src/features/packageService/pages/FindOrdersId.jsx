import { useParams, useNavigate } from "react-router-dom";
import { useOrders } from "../hooks/usesOrder";
import { useEffect, useState } from "react";
import EditOrder from "./EditOrder"; // Importa el componente EditOrder

export const FindOrdersId = () => {
  const { orderId } = useParams(); // Obtiene el parámetro 'orderId' de la URL
  const navigate = useNavigate(); // Hook para la navegación
  const { orders, loadOrders, removeOrder, loading } = useOrders(); // Obtén removeOrder del hook
  const [order, setOrder] = useState(null);
  const [isEditing, setIsEditing] = useState(false); // Estado para controlar la edición

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
      const foundOrder = orders.data.find(
        (o) => o.orderId.toLowerCase() === orderId.trim().toLowerCase()
      );
      setOrder(foundOrder);
    }
  }, [orders, loading, orderId]);

  const handleEditClick = () => {
    setIsEditing(true); // Muestra el formulario de edición
  };

  const handleDelete = async () => {
    try {
      await removeOrder(orderId); // Elimina la orden usando el hook
      alert(`Orden con ID ${orderId} eliminada exitosamente.`);
      navigate('/portal'); // Redirige al portal después de eliminar
    } catch (error) {
      console.error("Error al eliminar la orden:", error);
      alert("Hubo un error al intentar eliminar la orden.");
    }
  };

  if (loading && !order) {
    return <div>Loading...</div>;
  }

  if (!order) {
    return (
      <div>
        <h1>Orden No encontrada</h1>
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
      <h1 className="text-2xl font-bold mb-4">Detalles de la Orden</h1>

      {!isEditing ? (
        <>
          <p><strong>Orden Id: </strong>{order.orderId}</p>
          <p><strong>Fecha de Orden: </strong>{order.orderDate}</p>
          <p><strong>Nombre del Remitente: </strong>{order.senderName}</p>
          <p><strong>Nombre del Destinatario: </strong>{order.receiverName}</p>
          <p><strong>Dirección: </strong>{order.address}</p>
          <div className="flex space-x-4 mt-4">
            <button
              onClick={handleEditClick}
              className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-700"
            >
              Editar
            </button>
            <button
              onClick={handleDelete}
              className="bg-red-500 text-white px-4 py-2 rounded hover:bg-red-700"
            >
              Eliminar
            </button>
          </div>
        </>
      ) : (
        <EditOrder
          orderId={order.orderId}
          initialReceiverName={order.receiverName}
          initialAddress={order.address}
        />
      )}
      <button
        onClick={() => navigate('/portal')}
        className="mt-4 bg-gray-500 text-white px-4 py-2 rounded hover:bg-gray-700"
      >
        Regresar al Portal
      </button>
    </div>
  );
};
