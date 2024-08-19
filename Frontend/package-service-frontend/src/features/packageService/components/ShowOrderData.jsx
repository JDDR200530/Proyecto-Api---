import { useState, useRef, useEffect } from "react";
import { FaWindowClose, FaPrint } from "react-icons/fa";
import { useReactToPrint } from "react-to-print";
import { formatDate } from "../../../utils/format-date";
import { useOrders } from "../hooks";

export const ShowOrderData = ({ Order }) => {
  const { packages, loadPackages } = useOrders();
  const [fetching, setFetching] = useState(true);
  const [filteredPackages, setFilteredPackages] = useState([]);

  useEffect(() => {
    if (fetching) {
      loadPackages();
      setFetching(false);
    }
  }, [fetching, loadPackages]);

  useEffect(() => {
    if (packages.data) {
      // Filtra los paquetes para obtener solo los que corresponden al orderId
      const foundPackages = packages.data.filter(
        (pkg) => pkg.orderId.toLowerCase() === Order.orderId.trim().toLowerCase()
      );
      setFilteredPackages(foundPackages);
    }
  }, [packages.data, Order.orderId]);

  const [isModalOpen, setIsModalOpen] = useState(false);
  const componentRef = useRef();
  const handlePrint = useReactToPrint({
    content: () => componentRef.current,
  });

  const handleModalToggle = () => {
    setIsModalOpen(!isModalOpen);
  };

  // Calcular el subtotal
  const calculateSubtotal = () => {
    return filteredPackages.reduce((total, pkg) => total + pkg.price, 0);
  };

  // Calcular el total con impuestos
  const calculateTotal = () => {
    const subtotal = calculateSubtotal();
    const taxes = subtotal * 0.15; // Impuestos del 15%
    return subtotal + taxes;
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
                <div className="flex justify-between items-right">
                  <div>
                    <img src="/Logo.svg" alt="Logo" className="h-12" />
                    <p className="text-left font-bold">Envios Dynamo</p>
                    <p className="text-left text-sm font-thin">Santa Rosa de Copán, Honduras</p>
                    <p className="text-left text-xs">Email: soporte@enviosdynamo.com</p>
                  </div>
                  <div className="text-right">
                    <h1 className="text-xl font-bold">Factura</h1>
                    <p className="text-xs">{formatDate(Order.orderDate)}</p>
                  </div>
                </div>
              </header>

              {/* Info Section */}
              <section className="mb-4 mt-2 grid grid-cols-2 gap-2 text-sm">
                <div>
                  <h3 className="font-semibold">
                    De:<p>{Order.senderName}</p>
                  </h3>
                  <p>Santa Rosa de Copán, Honduras</p>
                </div>
                <div>
                  <h3 className="font-semibold">
                    Para:<p>{Order.receiverName}</p>
                  </h3>
                  <p>{Order.address}</p>
                </div>
              </section>

              {/* INFORMACIÓN DE LOS PAQUETES */}
              <h2 className="text-sm font-bold">
                Orden: <span className="font-semibold"> {Order.orderId} </span>
              </h2>
              
              {/* Tabla de Paquetes */}
              <section>
                <table className="w-full text-left border-separate border-spacing-y-2 text-sm">
                  <thead>
                    <tr className="bg-gray-200">
                      <th className="p-1 text-right">Id</th>
                      <th className="p-1">Descripción</th>
                      <th className="p-1 text-right">Peso (lbs)</th>
                      <th className="p-1 text-right">Precio</th>
                    </tr>
                  </thead>
                  <tbody>
                    {filteredPackages.length > 0 ? (
                      filteredPackages.map((pkg) => (
                        <tr className="bg-gray-50" key={pkg.id}>
                          <td className="p-1 text-right">{pkg.id}</td>
                          <td className="p-1">{pkg.description || "Paquete"}</td>
                          <td className="p-1 text-right">{pkg.packageWeight || "N/A"}</td>
                          <td className="p-1 text-right">{pkg.price || "N/A"}</td>
                        </tr>
                      ))
                    ) : (
                      <tr>
                        <td colSpan="4" className="text-center">
                          No hay paquetes disponibles
                        </td>
                      </tr>
                    )}
                  </tbody>
                  <tfoot>
                    <tr>
                      <td colSpan="3" className="p-1 text-right font-semibold">
                        Subtotal
                      </td>
                      <td className="p-1 text-right">Lps {calculateSubtotal().toFixed(2)}</td>
                    </tr>
                    <tr>
                      <td colSpan="3" className="p-1 text-right font-semibold">
                        Impuestos (15%)
                      </td>
                      <td className="p-1 text-right">Lps {(calculateSubtotal() * 0.15).toFixed(2)}</td>
                    </tr>
                    <tr>
                      <td colSpan="3" className="p-1 text-right font-bold">
                        Total
                      </td>
                      <td className="p-1 text-right">Lps {calculateTotal().toFixed(2)}</td>
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
