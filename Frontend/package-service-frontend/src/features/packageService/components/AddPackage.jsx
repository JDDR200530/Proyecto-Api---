

export const AddPackage = () => {
  return (
    <div>
        <h3 className="text-md font-bold m-16">Datos del Paquete</h3>
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

    </div>
    
  )
}
