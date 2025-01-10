import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Home from './assets/Home';
import Ubicacion from './assets/Ubicacion';
import Multimedia from './assets/Multimedia';
import Horarios from './assets/Horarios';  // Importamos la nueva página Horarios

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/ubicacion" element={<Ubicacion />} />
        <Route path="/multimedia" element={<Multimedia />} />
        <Route path="/horarios" element={<Horarios />} />  {/* Nueva ruta para Horarios */}
      </Routes>
    </Router>
  );
}

export default App;
