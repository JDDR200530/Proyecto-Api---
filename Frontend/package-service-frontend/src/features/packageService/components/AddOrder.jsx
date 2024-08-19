import { useState } from "react";
import { GiWeightScale } from "react-icons/gi";
import { createOrder, createPackages } from "../../../shared/actions/post/post";
import { showErrorAlert, showSuccessAlert } from "../../../utils/functions";
import { useNavigate } from "react-router-dom";

export const AddOrder = () => {
  const navigate = useNavigate();
  const [orderForm, setOrderForm] = useState({
    senderName: "",
    address: "",
    receiverName: "",
    packages: [], // Agregar los paquetes en el estado
  });

  // Manejo de cambios en los inputs
  const handleChange = (e) => {
    setOrderForm({
      ...orderForm,
      [e.target.name]: e.target.value,
    });
  };

  // Manejo de los cambios en los paquetes
  const handleAddPackage = () => {
    setOrderForm((prevForm) => ({
      ...prevForm,
      packages: [
        ...prevForm.packages,
        { id: prevForm.packages.length + 1, weight: "" },
      ],
    }));
  };

  const handleRemovePackage = (id) => {
    setOrderForm((prevForm) => ({
      ...prevForm,
      packages: prevForm.packages.filter((pkg) => pkg.id !== id),
    }));
  };

  const handleWeightChange = (id, value) => {
    setOrderForm((prevForm) => ({
      ...prevForm,
      packages: prevForm.packages.map((pkg) =>
        pkg.id === id ? { ...pkg, weight: value } : pkg
      ),
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (orderForm.packages.length === 0) {
      showErrorAlert('Debe haber al menos un paquete.');
      return;
    }

    try {
      console.log(orderForm);
      
      // Crear la orden
      const response = await createOrder(orderForm);
      console.log(response);
      
      const orderId = response.data.orderId;
      console.log(orderId);
      

      // Crear los paquetes
      const packagePromises = orderForm.packages.map((pkg) =>
        createPackages(orderId, pkg.weight) 
      );
 
      Promise.all(packagePromises);

      showSuccessAlert('¡Orden creada con éxito!');
      navigate('/portal')
      console.log("Orden y paquetes creados correctamente");
    } catch (error) {
      showErrorAlert('Error al crear la orden');
      console.error("Error creando la orden:", error);
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

        {/* Paquetes Section */}
        <div className="w-full space-x-8">
          <div className="flex-1 p-4 bg-gray-200 rounded-md">
            <h3 className="text-md font-bold mb-4">Paquetes</h3>

            {orderForm.packages.map((pkg, index) => (
              <div
                key={pkg.id}
                className="flex items-start justify-between p-4 rounded mb-4"
              >
                <div className="bg-slate-100 p-4 rounded-md flex-grow mr-4">
                  <label className="text-md font-medium block mb-2">
                    Datos del Paquete {index + 1}
                  </label>

                  <div className="flex items-center space-x-3 mb-4">
                    <p className="font-semibold text-gray-700">Peso lbs (libras)</p>
                    <GiWeightScale className="w-6 h-6 text-gray-500" />
                    <input
                      className="rounded block px-4 py-2 border-gray-300 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
                      placeholder="Introduce el peso"
                      value={pkg.weight}
                      onChange={(e) => handleWeightChange(pkg.id, e.target.value)}
                    />
                    <p>cotización</p>
                  </div>
                  <button
                    className="bg-red-500 text-white px-6 py-2 rounded-md hover:bg-red-600"
                    onClick={() => handleRemovePackage(pkg.id)}
                  >
                    Eliminar Paquete
                  </button>
                </div>
              </div>
            ))}

            <button
              type="button"
              className="bg-blue-500 text-white px-6 py-2 rounded-md mt-4 hover:bg-blue-600"
              onClick={handleAddPackage}
            >
              Agregar Paquete
            </button>
          </div>
        </div>

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

