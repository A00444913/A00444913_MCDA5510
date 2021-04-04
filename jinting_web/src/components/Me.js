import React,{Component} from 'react';
import me from '../assets/images/me.png';
import '../assets/css/index.css';

class Me extends Component{

    constructor(props){
        super(props);

        this.state={
            name:"Jinting"
    
        }
    }

    render(){
        return(
            <div className="me" align="center">
                <h2>Hi, My name is {this.state.name}</h2>
                <img src={me} />
                <br />
                <br />
                <p>I'm from China, and I had worked as software tester for over 4 years. I love jogging and travelling in my spare time!</p>
                <p>I joined MCDA because I faced some bottleneck in my previous job and also wanted to live in another country for a while to challenge myself.</p>

            </div>
        )
    }
}

export default Me;