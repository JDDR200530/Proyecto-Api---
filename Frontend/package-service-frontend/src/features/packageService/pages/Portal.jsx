import { Link } from "react-router-dom";

export const Portal = () => {
  return (
    // Ordenes 
    <div className="flex w-11/12  p-8  m-auto justify-between container  mx-20">
        {/* Mostrar Datos y Crear Orden*/}
      <div className="bg-orange-200 text-gray-700 flex w-10/12 p-4 m-4 shadow-lg rounded-lg flex-col">
        <div className="p-4">
          <label className="block text-gray-900 text-lg font-bold mb-4 ">
             Datos Ordenes & Creación de ordenes
          </label>
          <div className="pr-6">
            <div className="justify-between flex">
              <p className="font-medium">Ordenes Totales</p>
              <p>total</p>
            </div>
          </div>
          <div className="pr-6">
            <div className="justify-between flex">
              <p className="font-medium">Ordenes en espera de Pago</p>
              <p>total</p>
            </div>
          </div>
          <div className="pr-6">
            <div className="justify-between flex">
              <p className="font-medium">Ordenes Actuales</p>
              <p>total</p>
            </div>
          </div>
         <div className="flex justify-between py-4">
            <div className=" bg-orange-500 hover:bg-green-700  text-white font-bold rounded w-max mt-2 p-4 shadow-sm">
                <Link to={"/createorder"}>Crear Nueva Orden</Link>
            </div>
            <div className=" bg-orange-500 hover:bg-green-700  text-white font-bold rounded w-max mt-2 p-4 shadow-sm">
                <Link to={"/createorder"}>Listar Todas las Ordenes</Link>
            </div>
         </div>
         
        </div>
      </div>
      {/* Fin Datos y Crear Orden*/}
      {/* Buscar Datos */}
      <div className="bg-orange-200 text-gray-700 flex w-10/12 p-4 m-4 shadow-lg rounded-lg flex-col">
        <div className="p-4">
          <label className="block text-gray-900 text-lg font-bold mb-4 ">
            Buscar Ordenes
          </label>
          <div className="pr-6 py-4">
            <div className="justify-between flex ">
              <p className="font-medium">Buscar Orden - Numero Guía</p>
              <input className="bg-orange-100 rounded-sm focus:outline">
              </input>
            </div>
          </div>
          <div className="pr-6 py-4">
            <div className="justify-between flex ">
              <p className="font-medium">Buscar Ordenes - Cliente</p>
              <input className="bg-orange-100 rounded-sm focus:outline">
              </input>
            </div>
          </div>
        

          <div className=" bg-orange-500 hover:bg-green-700  text-white font-bold rounded w-max mt-2 p-4 shadow-sm">

            <Link to={"/listorder"}>Buscar</Link>
          </div>
        </div>
      </div>
      {/* Fin Buscar Datos */}
    </div>
    // fin de ordenes 

    // Inicio Buscar Orden 

  );
};
