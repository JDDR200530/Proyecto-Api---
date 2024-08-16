import { useState } from "react";

export const CreateOrder = () => {
    const [isOpen, setIsOpen] = useState(false);  
    return (
    <div className="container w-10/12 p-4 mx-20 bg-gray-50 rounded-lg shadow-md ">
      <h2 className="text-lg font-bold mb-4">Clientes</h2>
      <div className="pl-10">
        <div className="mb-4 ">
          <label className="block font-semibold text-gray-700 ">
            Emisor <span className="text-red-500">*</span>
          </label>
          <div className="flex ">
            <select className="mt-1 block w-10/12 px-4 pl-3 pr-10 py-2  border-gray-300 focus:outline-none  focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm rounded-md">
              <option>Haz clic para elegir</option>
              {/* Add more options here */}
            </select>
            <a
              href="#"
              className="ml-2 text-blue-600 hover:text-blue-800 text-sm py-2"
            >
              + Crear
            </a>
          </div>
        </div>

        <div>
          <label className="block  font-semibold text-gray-700">
            Destinatario <span className="text-red-500">*</span>
          </label>
          <div className="flex items-center">
            <select className="mt-1 block w-10/12 px-4 pl-3 pr-10 py-2  border-gray-300 focus:outline-none  focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm rounded-md">
              <option>Haz clic para elegir</option>
              {/* Add more options here */}
            </select>
            <a
              href="#"
              className="ml-2 text-blue-600 hover:text-blue-800 text-sm"
            >
              + Crear
            </a>
          </div>
        </div>
      </div>
    </div>
    );
};
