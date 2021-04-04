import logo from './assets/images/logo.svg';
import './assets/css/index.css';
import React,{Component} from 'react';
import Home from './components/Home';
import Me from './components/Me';

class App extends Component{
  constructor(props){
    super(props)
    this.state={
      selectedMenu:'myself'
    }
  }
  render(){
    return(
      <div className="App">
        <div className="menu">
          <p className="menu-item" onClick={() => this.setState({selectedMenu:'myself'})}>About me</p>
          <p className="menu-item" onClick={() => this.setState({selectedMenu:'town'})}>My Town</p>
        </div>
        <br />
        {this.state.selectedMenu === 'town'?
        <Home />
      :
      <Me />}

    </div>
    );
  }
}

export default App;
