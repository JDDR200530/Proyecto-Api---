import { useState, useEffect } from "react";
import { GiWeightScale } from "react-icons/gi";

export const AddPackage = ({ onPackagesChange }) => {
  const [packages, setPackages] = useState([{ id: 1, weight: "" }]);

  useEffect(() => {
    onPackagesChange();
  }, [onPackagesChange]);

  const handleAddPackage = () => {
    const newPackage = { id: packages.length + 1, weight: "" };
    setPackages([...packages, newPackage]);
  };

  const handleRemovePackage = (id) => {
    if (id !== 1) {
      setPackages(packages.filter((pkg) => pkg.id !== id));
    }
  };

  const handleWeightChange = (id, value) => {
    setPackages(
      packages.map((pkg) =>
        pkg.id === id ? { ...pkg, weight: value } : pkg
      )
    );
  };

  return (
    <div className="w-full space-x-8">
      {/* Paquetes Section */}
      <div className="flex-1 p-4 bg-gray-200 rounded-md">
        <h3 className="text-md font-bold mb-4">Paquetes</h3>

        {packages.map((pkg, index) => (
          <div
            key={pkg.id}
            className="flex items-start justify-between p-4 rounded mb-4"
          >
            {/* Paquetes Data Section */}
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
              {pkg.id !== 1 && (
                <button
                  className="bg-red-500 text-white px-6 py-2 rounded-md hover:bg-red-600"
                  onClick={() => handleRemovePackage(pkg.id)}
                >
                  Eliminar Paquete
                </button>
              )}
            </div>
          </div>
        ))}

        {/* Añadir Paquetes */}
        <button
          className="bg-blue-500 text-white px-6 py-2 rounded-md mt-4 hover:bg-blue-600"
          onClick={handleAddPackage}
        >
          Agregar Paquete
        </button>
      </div>
    </div>
  );
};
