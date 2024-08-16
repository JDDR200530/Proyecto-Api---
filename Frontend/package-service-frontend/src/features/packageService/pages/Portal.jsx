

export const Portal = () => {
  return (
    <div className="flex w-11/12 p-8  m-auto justify-between container  mx-20">
        <div className="bg-white text-cyan-700 flex w-10/12 p-4 m-4 shadow-lg rounded-md flex-col">
        <label className="block text-gray-900 text-lg font-bold mb-4 ">
            Ordenes
          </label>

          <p>Ordenes Totales</p>  
          <p>Ordenes en espera de Pago</p>  
          <p>Ordenes Actuales</p>  


        </div>
        <div className="bg-cyan-700 flex w-10/12 p-4 m-4 shadow-lg rounded-sm ">
            Que onda
        </div>
    </div>
  )
}
