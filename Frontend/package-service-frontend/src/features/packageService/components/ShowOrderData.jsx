import { useState, useRef } from "react";
import { FaWindowClose, FaPrint } from "react-icons/fa";
import { useReactToPrint } from "react-to-print";

export const ShowOrderData = ({
  orderId,
  
}) => {
   
        
  const [isModalOpen, setIsModalOpen] = useState(false);
  const componentRef = useRef();
  const handlePrint = useReactToPrint({
    content: () => componentRef.current,
  });

  const handleModalToggle = () => {
    setIsModalOpen(!isModalOpen);
  };

  return (
    <div className="relative">
      {/* Modal */}
      {isModalOpen && (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-gray-900 bg-opacity-50">
          {/* Modal Container */}
          <div
            className="relative max-w-[700px] max-h-[90vh] bg-white rounded-lg shadow-md p-6 overflow-auto print:max-w-full print:max-h-full print:w-full print:h-auto print:p-8"
            ref={componentRef}
          >
            {/* Botón de Imprimir */}
            <button
              className="absolute top-4 left-4 text-blue-600 text-2xl bg-white w-max h-max print:hidden"
              onClick={handlePrint}
            >
              <FaPrint />
            </button>

            {/* Botón de Cerrar */}
            <button
              className="absolute top-4 right-4 text-red-600 text-2xl bg-white w-max h-max print:hidden"
              onClick={handleModalToggle}
            >
              <FaWindowClose />
            </button>

            {/* Contenido de la Factura */}
            <div className="m-6">
              {/* Header */}
              <header className="mb-4">
                <div className="flex justify-between items-center">
                  <img src="/Logo.svg" alt="Logo" className="h-12" />
                  <div className="text-right">
                    <h1 className="text-xl font-bold">Factura</h1>
                    
                    <p className="text-xs">Fecha de Emisión: 17/08/2024</p>
                  </div>
                </div>
              </header>

              {/* Info Section */}
              <section className="mb-4 grid grid-cols-2 gap-2 text-sm">
                {/* Nombre empresa Info */}
                <div>
                  <h3 className="font-semibold">De:</h3>
                  <p>Envios Dynamo</p>
                  <p>Santa Rosa de Copán, Honduras</p>
                  <p>Email: soporte@enviosdynamo.com</p>
                </div>

                {/* Client Info */}
                <div>
                  <h3 className="font-semibold">Para:</h3>
                  {/* Sender */}
                  <p>{order.SenderName}</p>
                  <p>Calle Real 456</p>
                </div>
              </section>
              {/* INFORMACIóN DE LOS PAQUETES */}
              <h2 className="text-sm font-bold">Orden: <span className="font-semibold"> {orderId}  key={generateId()} </span></h2>   
              {/* Tabla de Paquetes*/}
              <section>
                <table className="w-full text-left border-separate border-spacing-y-2 text-sm">
                  <thead>
                    <tr className="bg-gray-200">
                     <th className="p-1 text-right">N°</th>   
                      <th className="p-1">Descripción</th>
                      <th className="p-1 text-right">Peso (lbs)</th>
                      <th className="p-1 text-right">Precio</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr className="bg-gray-50">
                     <td className="p-1 text-right">1</td>   
                      <td className="p-1">Paquete</td>
                      <td className="p-1 text-right">100</td>
                      <td className="p-1 text-right">lps 50.00</td>
                      
                    </tr>
                    {/* Más filas... */}
                  </tbody>
                  <tfoot>
                    <tr>
                      <td colSpan="3" className="p-1 text-right font-semibold">
                        Subtotal
                      </td>
                      <td className="p-1 text-right">lps 95.00</td>
                    </tr>
                    <tr>
                      <td colSpan="3" className="p-1 text-right font-semibold">
                        Impuestos (15%)
                      </td>
                      <td className="p-1 text-right">lps 14.25</td>
                    </tr>
                    <tr>
                      <td colSpan="3" className="p-1 text-right font-bold">
                        Total
                      </td>
                      <td className="p-1 text-right">lps 109.25</td>
                    </tr>
                  </tfoot>
                </table>
              </section>

              {/* Footer */}
              <footer className="mt-4 text-xs text-center text-gray-600">
                <p>Gracias por su compra!</p>
                <p>
                  Si tiene alguna pregunta, no dude en contactarnos a
                  soporte@enviosdynamo.com
                </p>
              </footer>
            </div>
          </div>
        </div>
      )}

      {/* Main Button */}
      <button className="text-blue-500 mt-2" onClick={handleModalToggle}>
        + PDF
      </button>
    </div>
  );
};


