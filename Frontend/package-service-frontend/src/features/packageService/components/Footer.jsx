export const Footer = () => {
    return (
      <footer className="bg-gray-800 text-white py-8">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="flex flex-col md:flex-row justify-between ">
            {/* Column 1 */}
            <div className="mb-8 md:mb-0">
              <h3 className="text-lg font-bold ">Envíos Dynamo</h3>
              <ul>
                <li>
                  <a href="#" className="text-gray-400 hover:text-white">
                    Acerca de Nosotros
                  </a>
                </li>
                <li>
                  <a href="#" className="text-gray-400 hover:text-white">
                    Vision
                  </a>
                </li>
                <li>
                  <a href="#" className="text-gray-400 hover:text-white">
                    Mision
                  </a>
                </li>
              </ul>
            </div>
  
            {/* Column 2 */}
            <div className="mb-6 md:mb-0">
              <h3 className="text-lg font-bold ">Soporte</h3>
              <ul>
                <li>
                  <a href="#" className="text-gray-400 hover:text-white">
                    Contactanos
                  </a>
                </li>
                <li>
                  <a href="#" className="text-gray-400 hover:text-white">
                    Servicio Al Cliente
                  </a>
                </li>
                <li>
                  <a href="#" className="text-gray-400 hover:text-white">
                    Términos y Servicios
                  </a>
                </li>
              </ul>
            </div>
  
            {/* Column 3 */}
            <div className="mb-6 md:mb-0">
              <h3 className="text-lg font-bold ">Nos encuentras</h3>
              <ul className="flex space-x-4">
                <li>
                  <a href="#" className="text-gray-400 hover:text-white">
                    <i className="fab fa-facebook"></i> Facebook
                  </a>
                </li>
                <li>
                  <a href="#" className="text-gray-400 hover:text-white">
                    <i className="fab fa-twitter"></i> Twitter
                  </a>
                </li>
                <li>
                  <a href="#" className="text-gray-400 hover:text-white">
                    <i className="fab fa-instagram"></i> Instagram
                  </a>
                </li>
              </ul>
            </div>
          </div>
  
          <div className="mt-8 text-center text-gray-400">
            &copy; {new Date().getFullYear()} Your Company. All Rights Reserved.
          </div>
        </div>
      </footer>
    );
  };
  