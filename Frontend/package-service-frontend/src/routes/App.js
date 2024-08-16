import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import { Portal } from "./components/Portal";
import SimpleOrderList from "./components/SimpleOrderList"; // Importa tu nuevo componente aquí

const App = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Portal />} />
        <Route path="/listorder" element={<SimpleOrderList />} /> {/* Aquí se conecta la ruta */}
      </Routes>
    </Router>
  );
};

export default App;
