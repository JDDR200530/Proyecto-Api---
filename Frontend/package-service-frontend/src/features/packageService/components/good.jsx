import { useState} from "react";
import { AddPackage } from "./AddPackage";
import { createOrder } from "../../../shared/actions/post/post";
import { showErrorAlert, showSuccessAlert } from "../../../utils/functions";

export const AddOrder = () => {
  const [orderForm, setOrderForm] = useState({
    senderName: "",
    address: "",
    receiverName: "",
    
  
  });

  // Manejo de cambios en los inputs
  const handleChange = (e) => {
    setOrderForm({
      ...orderForm,
      [e.target.name]: e.target.value,
    });
  };



  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      console.log(orderForm);
      
      const response = await createOrder(orderForm);
      
      showSuccessAlert();
      console.log("Orden creada Correctamente:", response.data);
    } catch (error) {
      showErrorAlert();
      console.error("Error creating order:", error);

    }
  };

  return (
    <div className="container w-8/12 p-4 mx-20 bg-gray-50 rounded-lg shadow-md ">
      <h2 className="text-lg font-bold m-8">Crear Orden</h2>
      <form onSubmit={handleSubmit}>
        <div className="pl-20">
          <div className="mb-4">
            <label className="block font-semibold text-gray-700 ">
              Emisor <span className="text-red-500">*</span>
            </label>
            <div className="flex">
              <input
                name="senderName"
                placeholder="Emisor"
                value={orderForm.senderName}
                onChange={handleChange}
                required
                className="mt-1 block w-10/12 px-4 pl-3 pr-10 py-2 border-gray-300 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm rounded-md"
              />
            </div>
          </div>

          <div className="mb-4">
            <label className="block font-semibold text-gray-700">
              Destinatario <span className="text-red-500">*</span>
            </label>
            <div className="flex items-center">
              <input
                name="receiverName"
                placeholder="Destinatario"
                value={orderForm.receiverName}
                onChange={handleChange}
                required
                className="mt-1 block w-10/12 px-4 pl-3 pr-10 py-2 border-gray-300 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm rounded-md"
              />
            </div>
          </div>

          <div className="mb-4">
            <label className="block font-semibold text-gray-700">
              Dirección Destino<span className="text-red-500">*</span>
            </label>
            <div className="flex items-center">
              <input
                name="address"
                placeholder="Dirección"
                value={orderForm.address}
                onChange={handleChange}
                required
                className="mt-1 block w-10/12 px-4 pl-3 pr-10 py-2 border-gray-300 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm rounded-md"
              />
            </div>
          </div>
        </div>

        {/* Componente de Paquetes */}
        <AddPackage  />

        {/* Botón Guardar */}
        <div className="flex justify-center mt-8">
          <button
            type="submit"
            className="px-4 py-2 bg-indigo-500 text-white font-semibold rounded-md shadow-sm hover:bg-indigo-600 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
          >
            Guardar Datos
          </button>
        </div>
      </form>
    </div>
  );
};
