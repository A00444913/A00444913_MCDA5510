import React,{Component} from 'react';
import cold from '../assets/images/cold.png';
import '../assets/css/index.css';

class Pic extends Component{
    constructor(props){
        super(props);
        this.state={
            msg:"news",
            list:["111","222","333"],
            list2:[<h2>i'm a h2</h2>,<h2>i'm a h2</h2>]
        }
    }

    run(){
        alert("I am a run function")
    }

    render(){
        let LisrResult = this.state.list.map(function(value,key){
            return <li key={key}>{value}</li>

        })
        return(
            <div>
                <button onClick={this.run}>activate</button>
                {this.state.msg}
                <img src={cold} />
                <hr />
                {this.state.list2}
                <hr />
                {LisrResult}
            </div>

        )
    }
}

export default Pic;