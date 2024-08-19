import  { useState } from 'react';
import PropTypes from 'prop-types';
import { useOrders } from '../hooks/usesOrder';

const EditOrder = ({ orderId, initialReceiverName, initialAddress }) => {
    const { updateOrder } = useOrders();
    const [receiverName, setReceiverName] = useState(initialReceiverName);
    const [address, setAddress] = useState(initialAddress);
    const [loading, setLoading] = useState(false);
    const [successMessage, setSuccessMessage] = useState("");
    const [errorMessage, setErrorMessage] = useState("");

    const handleUpdate = async () => {
        try {
            setLoading(true);
            const updatedData = { receiverName, address };
            await updateOrder(orderId, updatedData);
            setSuccessMessage("Orden actualizada exitosamente.");
            setErrorMessage("");
        } catch (error) {
            console.error("Error al actualizar la orden:", error);
            setSuccessMessage("");
            setErrorMessage("No se pudo actualizar la orden. Por favor, inténtelo de nuevo.");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="container mx-auto p-4">
            <h3 className="text-2xl font-bold mb-4">Editar Orden {orderId}</h3>
            <div className="mb-4">
                <label className="block text-gray-700">Nombre del Destinatario:</label>
                <input
                    type="text"
                    value={receiverName}
                    onChange={(e) => setReceiverName(e.target.value)}
                    className="border rounded p-2 w-full"
                />
            </div>
            <div className="mb-4">
                <label className="block text-gray-700">Dirección:</label>
                <input
                    type="text"
                    value={address}
                    onChange={(e) => setAddress(e.target.value)}
                    className="border rounded p-2 w-full"
                />
            </div>
            <button
                onClick={handleUpdate}
                disabled={loading}
                className={`bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-700 ${loading && "opacity-50 cursor-not-allowed"}`}
            >
                {loading ? "Actualizando..." : "Guardar Datos"}
            </button>

            {successMessage && (
                <p className="text-green-500 mt-4">{successMessage}</p>
            )}
            {errorMessage && (
                <p className="text-red-500 mt-4">{errorMessage}</p>
            )}
        </div>
    );
};

// Validación de las props usando PropTypes
EditOrder.propTypes = {
    orderId: PropTypes.string.isRequired,
    initialReceiverName: PropTypes.string.isRequired,
    initialAddress: PropTypes.string.isRequired,
};

export default EditOrder;
