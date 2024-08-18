import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import CreateOrder from '../pages/CreateOrder';
import ListOrder  from '../pages/ListOrder';
<pages></pages>; 
import NavBar from './NavBar'; 
function App() {
  return (
    <Router>
      <NavBar />
      <Routes>
        <Route path="/create-order" element={<CreateOrder />} />
        <Route path= "/listorder" element = {<ListOrder/>}/>
         {/* Otras rutas */}
      </Routes>
    </Router>
  );
}

export default App;
