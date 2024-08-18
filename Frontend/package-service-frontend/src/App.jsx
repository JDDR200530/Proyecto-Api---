import { BrowserRouter as Router, Route,Routes } from "react-router-dom";
import Orders from "./features/packageService/components/Order";
import { AppRouter } from "./routes/AppRouter";

function App(){
  return (
    <Router>
      <Routes>
        <Route path="/orders" element={<Orders/>}/>
         <Route path="/*" element={<AppRouter/>}/>
      </Routes>
    </Router>
  )
}

export default App;
