import { useState } from "react";
import { useNavigate } from "react-router-dom";

export const Login = () => {
  const [form, setForm] = useState({
    email: "",
    password: "",
  });

  const navigate = useNavigate();

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault(); 
    console.log("Form submitted:", form);

    // Llamada a la Api para verifica Datos

    
    navigate("/portal");
  };

  return (
    <div className="container w-full justify-items-center">
      {/* Main Form */}
      <div className="max-w-2xl p-8 bg-white shadow-lg rounded-lg m-auto">
        <h1 className="text-2xl font-bold mb-4">Login</h1>
        <form className="grid gap-4 mb-8" onSubmit={handleSubmit}>
          <div className="flex-none">
            <label className="block text-gray-700">
              Correo<span className="text-red-500"> *</span>
            </label>
            <input
              type="email"
              name="email"
              value={form.email}
              onChange={handleChange}
              placeholder="Correo Electrónico"
              className="mt-1 block w-full p-2 border border-gray-300 rounded"
            />

            <label className="block text-gray-700 mt-4">
              Contraseña<span className="text-red-500"> *</span>
            </label>
            <input
              type="password"
              name="password"
              value={form.password}
              onChange={handleChange}
              placeholder="Contraseña"
              className="mt-1 block w-full p-2 border border-gray-300 rounded"
            />

            <div className="justify-items-end mt-4">
              <button
                type="submit"
                className="text-white mt-2 bg-orange-500 p-4 rounded-md"
              >
                Iniciar Sesión
              </button>
            </div>
          </div>
        </form>
      </div>
    </div>
  );
};

