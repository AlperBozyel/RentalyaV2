import './App.css';
// Login bileşenini import edelim
import Login from './components/Login/Login';

function App() {
  return (
    <div className="App">
      {/* Eski içeriği kaldırıp Login bileşenini ekleyelim */}
      <Login />
    </div>
  );
}

export default App;
