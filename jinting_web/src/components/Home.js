import React,{Component} from 'react';
import '../assets/css/index.css';
import chengdu from '../assets/images/chengdu.png';
import Data from './Data';
//import Route from 'react-router';

class Home extends Component{

    constructor(){
        super();
        this.state={
            city:"Chengdu"

        }
    }

    render(){
        return(
            <div className="home" align="center">
            <h2>My hometown is {this.state.city}</h2>
            <img src={chengdu} />
            <Data />
            </div>
        )
    }
}

export default Home;