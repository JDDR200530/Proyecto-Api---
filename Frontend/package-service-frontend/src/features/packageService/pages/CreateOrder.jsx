

export const CreateOrder = () => {
     
    return (
      
    <div className="container w-8/12 p-4 mx-20 bg-gray-50 rounded-lg shadow-md ">

      <h2 className="text-lg font-bold m-8">Crear Orden</h2>
      {/* Inicio de Datos */}
      <div className="pl-20">
        <div className="mb-4 ">
          <label className="block font-semibold text-gray-700 ">
            Emisor <span className="text-red-500">*</span>
          </label>
          <div className="flex ">
        
              <input className="mt-1 block w-10/12 px-4 pl-3 pr-10 py-2  border-gray-300 focus:outline-none  focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm rounded-md"></input>
              {/* Add more options here */}
           
          </div>
        </div>

        <div className="mb-4 ">
          <label className="block  font-semibold text-gray-700">
            Destinatario <span className="text-red-500">*</span>
          </label>
          <div className="flex items-center">
          <input className="mt-1 block w-10/12 px-4 pl-3 pr-10 py-2  border-gray-300 focus:outline-none  focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm rounded-md"></input>

          </div>


        </div>

        <div className="mb-4 ">
          <label className="block  font-semibold text-gray-700">
            Dirección Destino<span className="text-red-500">*</span>
          </label>
          <div className="flex items-center">
          <input className="mt-1 block w-10/12 px-4 pl-3 pr-10 py-2  border-gray-300 focus:outline-none  focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm rounded-md"></input>

          </div>


        </div>

        <div className="mb-4 ">
          <label className="block  font-semibold text-gray-700">
            Cantidad de Paquetes <span className="text-red-500">*</span>
          </label>
          <div className="flex items-center">
          <input className="mt-1 block w-10/12 px-4 pl-3 pr-10 py-2  border-gray-300 focus:outline-none  focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm rounded-md border-black-500 "></input>

          </div>


        </div>
      


      </div>
          {/* Button Save Data */}
            <div className="flex justify-center mt-8">
                  <button
                  type= "submit"
                  className="px-4 py-2 bg-indigo-500 text-white font-semibold rounded-md shadow-sm hover:bg-indigo-600 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                  >
                    Guardar Datos
                  </button>
            </div>

    </div>
    );
};
