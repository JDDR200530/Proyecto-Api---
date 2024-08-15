
import { FaBoxesPacking } from "react-icons/fa6";

export const NavBar = () => {
  return (
    <nav className="bg-gray-900 text-white py-6">
    <div className=" container mx-auto flex justify-between items-center">
      {/* Logotipo */}
      <div className="flex items-center">
        <img src="/src/assets/Logo.png" alt="Logo" className=" h-16 w-40 p-0 rounded-sm" />
       <h1 className="text-white ml-3 font-bold text-2xl ">Envíos
        <span className="ml-3 font-bold text-2xl text-orange-400">Dynamo</span></h1> 
      </div>

      {/* Enlaces del menú */}
      <div className="hidden md:flex space-x-8">
        <a href="#" className="hover:text-blue-300">Inicio</a>
        <div className="relative group">
          <button className="hover:text-blue-300 focus:outline-none">Agencias</button>
          {/* Dropdown */}
          <div className="absolute hidden group-hover:block bg-white text-black mt-2 py-2 rounded shadow-lg">
            <a href="#" className="block px-4 py-2 hover:bg-gray-200">Subcategoría 1</a>
            <a href="#" className="block px-4 py-2 hover:bg-gray-200">Subcategoría 2</a>
          </div>
        </div>
        <a href="#" className="hover:text-green-300">Contacto</a>
        <a href="#" className="hover:text-green-300 text-orange-400 font-bold">Rastrear</a>
      </div>

      {/* Botones a la derecha */}
      <div className="hidden md:flex space-x-4">
        <button className=" flex justify-between bg-gray-400 text-white hover:bg-gray-300  hover:text-gray-500 font-bold py-2 px-4 rounded">
         <FaBoxesPacking className="px-1 h-6 w-6"  />
          Realiza tu Envío
        </button>
        <button className="bg-orange-500 hover:bg-orange-700 text-white font-bold py-2 px-4 rounded">
          Mi perfil
        </button>
      </div>

      
    </div>
  </nav>
  )
}
