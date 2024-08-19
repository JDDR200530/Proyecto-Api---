import { useState } from "react";

export const CreateClient = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [formData, setFormData] = useState({
    identificacion: "",
    nombre: "",
    telefono: "",
    email: "",
    direccion: "",
    direccionAdicional: "",
  });

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleModalToggle = () => {
    setIsModalOpen(!isModalOpen);
  };

  return (
    <div className="container w-full justify-items-center">
      {/* Main Form */}
      <div className="max-w-4xl p-8 bg-white shadow-lg rounded-lg m-auto">
        <h1 className="text-2xl font-bold mb-4">Login</h1>
        <div className="grid grid-cols-2 gap-4 mb-8">
          <div>
            <label className="block text-gray-700">
              Correo<span className="text-red-500"> *</span>
            </label>
            <input
              type="text"
              name="correo"
              value={formData.email}
              onChange={handleChange}
              placeholder="Correo Electrónico"
              className="mt-1 block w-full p-2 border border-gray-300 rounded"
            ></input>
            <button className="text-blue-500 mt-2" onClick={handleModalToggle}>
              + Crear
            </button>
          </div>
        </div>
      </div>

      {/* Modal */}
      {isModalOpen && (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-gray-900 bg-opacity-50">
          <div className="bg-white p-8 rounded-lg shadow-lg w-1/2">
            <h2 className="text-2xl font-bold mb-4">Crear Cliente</h2>
            <div className="grid grid-cols-1 gap-4">
              <div>
                <label className="block text-gray-700">Identificación</label>
                <input
                  type="text"
                  name="identificacion"
                  value={formData.identificacion}
                  onChange={handleChange}
                  placeholder="Identificación"
                  className="mt-1 block w-full p-2 border border-gray-300 rounded"
                />
              </div>
              <div>
                <label className="block text-gray-700">Nombre *</label>
                <input
                  type="text"
                  name="nombre"
                  value={formData.nombre}
                  onChange={handleChange}
                  placeholder="Nombre"
                  className="mt-1 block w-full p-2 border border-gray-300 rounded"
                />
              </div>
              <div>
                <label className="block text-gray-700">Teléfono</label>
                <input
                  type="text"
                  name="telefono"
                  value={formData.telefono}
                  onChange={handleChange}
                  placeholder="Teléfono"
                  className="mt-1 block w-full p-2 border border-gray-300 rounded"
                />
              </div>
              <div>
                <label className="block text-gray-700">Email</label>
                <input
                  type="email"
                  name="email"
                  value={formData.email}
                  onChange={handleChange}
                  placeholder="Email"
                  className="mt-1 block w-full p-2 border border-gray-300 rounded"
                />
              </div>
            </div>
            <div className="mt-6 flex justify-end space-x-4">
              <button
                className="bg-red-500 text-white py-2 px-4 rounded"
                onClick={handleModalToggle}
              >
                Cancelar
              </button>
              <button
                className="bg-green-500 text-white py-2 px-4 rounded"
                onClick={handleModalToggle}
              >
                Crear
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};
