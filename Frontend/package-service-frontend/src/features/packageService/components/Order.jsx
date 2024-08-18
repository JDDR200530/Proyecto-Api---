// src/features/packageService/pages/Orders.jsx
import { useEffect, useState } from 'react';
import { PackageService } from '../../../config/api/packageServiceApi';
import ListOrder from '../components/ListOrder'; // Ajusta la ruta si es necesario

const Orders = () => {
  const [orders, setOrders] = useState([]);

  useEffect(() => {
    PackageService.get('/orders')
      .then(response => {
        console.log(response.data);  // Verifica la estructura de los datos aquí
        setOrders(response.data);
      })
      .catch(error => {
        console.error('Error al obtener las órdenes:', error);
      });
  }, []);

  return (
    <div>
      <h1>Órdenes</h1>
      <ListOrder orders={orders} />
    </div>
  );
};

export default Orders;
