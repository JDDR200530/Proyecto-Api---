import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import CreateOrder from '../pages/CreateOrder';
import NavBar from './NavBar'; 
function App() {
  return (
    <Router>
      <NavBar />
      <Routes>
        <Route path="/create-order" element={<CreateOrder />} />
        {/* Otras rutas */}
      </Routes>
    </Router>
  );
}

export default App;
