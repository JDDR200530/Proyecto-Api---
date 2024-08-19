import { packageApi } from "../../../config/api";

// Obtener la lista de órdenes
export const getOrdersList = async () => {
    try {
        const { data } = await packageApi.get(`/orders`);
        return data;
    } catch (error) {
        console.error(error);
        return error.response;
    }
};

// Editar una orden por ID
export const editOrder = async (orderId, updatedData) => {
    try {
       
        const { address, receiverName } = updatedData;
        const response = await packageApi.put(`/orders/${orderId}`, { address, receiverName });
        
        

        return response.data;
    } catch (error) {
        console.error("Error updating order:", error);
        return error.response? error.response.data :{error:"Error desconocido"};
    }
};

// Borrar una orden por ID
export const deleteOrder = async (orderId) => {
    try {
        const { data } = await packageApi.delete(`/orders/${orderId}`);
        return data;
    } catch (error) {
        console.error(error);
        return error.response;
    }
};
export const getPackageList = async () => {


    try {
        const {data} = await packageApi.get(`/packages`,);
        return data;
    
    } catch (error) {
        console.error(error);
        return error.response;
    }
}

export const createOrder =  async (formData) => {


    try {
        const {data} = await packageApi.post(`/orders`, formData);

        
        return data;
    
    } catch (error) {
        console.error(error);
        return error.response;
    }
}





export const createOrderWithPackages = async (orderData) => {
  try {
    // Crear la orden primero
    const orderResponse = await packageApi.post('/orders', orderData);
    const orderId = orderResponse.data.data.orderId; // Obtiene el ID de la orden creada

    if (!orderId) {
      throw new Error('No se pudo obtener el ID de la orden');
    }

    // Crear los paquetes asociados a la orden
    const packageIds = await Promise.all(
      orderData.packages.map(async (pkg) => {
        // Crear cada paquete y asociarlo a la orden
        const response = await packageApi.post('/packages', {
          orderId,
          weight: pkg.weight, // Accede al peso del paquete directamente
        });
        return response.data.id; // Asegúrate de que la respuesta contiene el ID del paquete
      })
    );

    console.log('Paquetes creados:', packageIds);

    return packageIds;

  } catch (error) {
    console.error('Error al crear la orden o los paquetes:', error);
    throw error; // Vuelve a lanzar el error para que el manejador de errores en el componente lo pueda capturar
  }
};


export const createPackages = async (orderId, packageWeight) => {
    try {
    
      // Crear el paquete asociado a la orden
      const response = await packageApi.post('/packages', {
        orderId,  // Incluye el ID de la orden
        packageWeight    // Incluye el peso del paquete
      });
  
      console.log('Paquete creado:', response.data); // Muestra la respuesta en consola
  
      return response.data; // Devuelve los datos de la respuesta
  
    } catch (error) {
      console.error('Error al crear el paquete:', error);
      throw error; // Lanza el error para que el manejador de errores en el componente lo pueda capturar
    }
  };

  


