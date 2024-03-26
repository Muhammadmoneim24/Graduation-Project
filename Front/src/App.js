
import { Provider } from 'react-redux';
import './App.css';
import Body from './components/Body';
import store from './redux/store';

function App() {
  return (
   <Provider store={store}>
     <div className="App">
      <Body />
    </div>
   </Provider>
  );
}

export default App;
