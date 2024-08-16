import { Link } from "react-router-dom";
import { TbCubeSend } from "react-icons/tb";

export const HomePage = () => {
  return (
    <div className="items-center content-center w-full ">
      {/* Hero Section */}

      <Link className="text-center flex w-full bg-slate-100 p-10 rounded-lg"
      to={'/createorder'}
      >
        
        <div className="flex w-1/2 bg-orange-300 p-10 rounded-lg shadow-2xl">
          <h1 className="text-5xl font-bold ml-8 p-8 w-1/3">
            Su mejor opción en envíos
          </h1>
          <TbCubeSend className="w-48 h-48 m-8" />
        </div>
        <div className="p-4">
            <h2 className="text-2xl font-semibold m-6 text-gray-900 text-justify">
            En Envíos Dynamo, nos especializamos en ofrecer soluciones de entrega rápidas, seguras y eficientes para satisfacer todas tus necesidades logísticas. Con nuestro compromiso de proporcionar un servicio excepcional, garantizamos que tus paquetes lleguen a su destino a tiempo, sin importar la distancia.
            </h2>
            <h3 className="m-4 text-xl font-bold text-orange-300 shadow-sm ">
            Cada Paquete, un Compromiso
            </h3>
        </div>
        

      </Link>

      {/* Introduction Section */}
      <div className="py-10 px-6 bg-gray-300 rounded shadow mt-16">
        <div className="container mx-auto">
          <h2 className="text-3xl font-bold mb-4">¿Por qué elegir Envíos Dynamo?</h2>
          <p className="text-lg mb-1 text-black">
            <span className="font-bold">Velocidad y Eficiencia:</span> Nuestros avanzados sistemas de logística y una flota moderna de vehículos nos permiten realizar entregas rápidas y puntuales. Desde la recolección hasta la entrega final, optimizamos cada etapa del proceso para asegurar la máxima eficiencia.
          </p>
          <p className="text-lg mb-1 text-black">
            <span className="font-bold">Atención al Cliente de Primera:</span> Nuestro equipo de atención al cliente está disponible para responder a tus consultas y resolver cualquier problema que puedas tener. En Envios Dynamo, nos enorgullece brindar un servicio amigable y profesional.
          </p>
          <p className="text-lg mb-1 text-black">
            <span className="font-bold">Cada Paquete, un Compromiso:</span> En Envios Dynamo, entendemos que cada envío es importante. Es por eso que nos dedicamos a ofrecer un servicio que combina rapidez, seguridad y un toque personal para cada entrega. Confía en nosotros para manejar tus envíos con el cuidado y la precisión que merecen.
          </p>
        </div>
      </div>
    </div>
  );
};
