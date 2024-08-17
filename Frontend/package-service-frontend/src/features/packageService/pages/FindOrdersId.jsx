import {useParams} from "react-router-dom";
export const FindOrdersId = () =>{
    const {orderId} = useParams();

    const orders = [
        {
            OrderId: "dcde359e-ffcc-426b-96ba-77ab507c0946",
            OrderDate:  "8-6-2024",
            SenderName: "Carlos Pineda",
            Address: "Santa Rosa de Copan",
            ReciverName: "Juan Perez",
          },
          {
            OrderId: "7e4d2f82-cb9f-4b67-8257-09a2f3fba4c9",
            OrderDate: "25-5-2024",
            SenderName: "Ana Morales",
            Address: "Tegucigalpa",
            ReciverName: "Luis Fernández",
          },
          {
            OrderId: "3f2c1d7f-5f0e-4c80-b06e-0d41f5bde7f5",
            OrderDate: "10-8-2024",
            SenderName: "Marta López",
            Address: "San Pedro Sula",
            ReciverName: "Jorge Martinez",
          },
          {
            OrderId: "d1e67c21-737f-487b-bb5a-7872d568e582",
            OrderDate: "25-6-2024",
            SenderName: "Pedro Gómez",
            Address: "La Ceiba",
            ReciverName: "María Rodríguez",
          },
          {
            OrderId: "8a4f2d85-8030-4d7d-bb76-f0a1b4b3e5d8",
            OrderDate: "20-3-23",
            SenderName: "Sofia Castillo",
            Address: "Choluteca",
            ReciverName: "Roberto García",
          },
    ];
    
    const order = orders.find((o) =>o.OrderId.toLowerCase() === orderId.trim().toLowerCase());
    if (!order){
        return(
                <div>
                    <h1>Orden No encontrada</h1>
                </div>
        );
    }

    return(
        <div className="container mx-auto p-4">
            <h1 className="text-2x1 font-bold mb-4">Detalles de la Orden</h1>
            <p><strong>Orden Id: </strong>{order.OrderId}</p>
            <p><strong>Orden Date: </strong>{order.OrderDate}</p>
            <p><strong>SenderName: </strong>{order.SenderName}</p>
            <p><strong>ReciverName: </strong>{order.ReciverName}</p>
            <p><strong>Address: </strong>{order.Address}</p>
        </div>
    );
};
export default FindOrdersId;