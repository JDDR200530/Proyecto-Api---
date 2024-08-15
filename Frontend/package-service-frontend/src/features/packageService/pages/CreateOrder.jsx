import { useState } from "react";

export const CreateOrder = () => {
  const [formData, setFormData] = useState({
    identificacion: "",
    nombre: "",
    telefono: "",
    email: "",
    direccion: "",
    descripcion: "",
    peso: "",
    emisor: "",
    destinatario: "",
  });

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  return (
    <div className="max-w-4xl mx-auto p-8 bg-white shadow-lg rounded-lg">
      {/* Sección de Cliente */}
      <div>
        <h2 className="text-2xl font-bold mb-4">Crear Cliente</h2>
        <div className="grid grid-cols-2 gap-4 mb-8">
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
      </div>

      {/* Sección de Dirección */}
      <div>
        <h2 className="text-2xl font-bold mb-4">Dirección</h2>
        <div className="grid grid-cols-2 gap-4 mb-8">
          <div>
            <label className="block text-gray-700">Dirección</label>
            <input
              type="text"
              name="direccion"
              value={formData.direccion}
              onChange={handleChange}
              placeholder="Dirección"
              className="mt-1 block w-full p-2 border border-gray-300 rounded"
            />
          </div>
          <div>
            <label className="block text-gray-700">Dirección Adicional</label>
            <input
              type="text"
              name="direccionAdicional"
              value={formData.direccionAdicional}
              onChange={handleChange}
              placeholder="Dirección Adicional"
              className="mt-1 block w-full p-2 border border-gray-300 rounded"
            />
          </div>
        </div>
      </div>

      {/* Sección de Clientes */}
      <div>
        <h2 className="text-2xl font-bold mb-4">Clientes</h2>
        <div className="grid grid-cols-2 gap-4 mb-8">
          <div>
            <label className="block text-gray-700">Emisor *</label>
            <select
              name="emisor"
              value={formData.emisor}
              onChange={handleChange}
              className="mt-1 block w-full p-2 border border-gray-300 rounded"
            >
              <option value="">Haz clic para elegir</option>
              <option value="luis">Luis</option>
              <option value="ing_plar_luis">Ing. Pilar Luis</option>
            </select>
          </div>
          <div>
            <label className="block text-gray-700">Destinatario</label>
            <select
              name="destinatario"
              value={formData.destinatario}
              onChange={handleChange}
              className="mt-1 block w-full p-2 border border-gray-300 rounded"
            >
              <option value="">Haz clic para elegir</option>
              <option value="luis_arguello">Luis Argüello</option>
              <option value="luis_mata">Luis Mata</option>
            </select>
          </div>
        </div>
      </div>

      {/* Sección de Descripción */}
      <div>
        <h2 className="text-2xl font-bold mb-4">Descripciones</h2>
        <div className="grid grid-cols-2 gap-4 mb-8">
          <div>
            <label className="block text-gray-700">Descripción *</label>
            <textarea
              name="descripcion"
              value={formData.descripcion}
              onChange={handleChange}
              placeholder="Descripción"
              className="mt-1 block w-full p-2 border border-gray-300 rounded h-24"
            />
          </div>
          <div>
            <label className="block text-gray-700">Peso en Kilos *</label>
            <input
              type="number"
              name="peso"
              value={formData.peso}
              onChange={handleChange}
              placeholder="Peso en Kilos"
              className="mt-1 block w-full p-2 border border-gray-300 rounded"
            />
          </div>
        </div>
      </div>

      <button
        className="bg-green-500 hover:bg-green-600 text-white font-bold py-2 px-4 rounded"
        onClick={() => alert(" enviado")}
      >
        Enviar
      </button>
    </div>
  );
};



