import React, { Component } from "react"
import cold from '../assets/images/cold.png';
import mild from '../assets/images/mild.png';
import sunny from '../assets/images/sunny.png';
import '../assets/css/index.css';

class Data extends Component {
    constructor(props) {
        super(props)
        this.state = {
            temp: '',
            faren:'',
            Time: Date(),
            selected:'c'
        }
        this.fetchData = this.fetchData.bind(this);
    }

    fetchData(){
        fetch("https://api.openweathermap.org/data/2.5/weather?q=Chengdu&appid=70dbe7c6d0e96544723de008abad5f64&units=metric")
            .then(response => {
                return response.json()
            })
            .then(response => {
                 //console.log(response.main.temp);
                this.setState({
                    temp: response.main.temp,
                    faren: this.Farenheit(this.state.temp)
                })
            });
    }

    componentDidMount() {
        this.fetchData()
    }


    Farenheit(value){
        return 9*value/5+32;
    }

    render() {
        return (
            <div className="temp">
                <p>Chengdu is located in the southwest part of China. It is famous for its people's life style, and it is also the hometown for panda!</p>
                <p><b>Current time is : </b>{this.state.Time}</p>
                <br />
                <button onClick={() => this.setState({selected:'c'})}>See weather in Celsius scale</button>
                <button onClick={() => this.setState({selected:'f'})}>See weather in Fahrenheit scale</button>
                {this.state.selected === 'c'?
                    <p><b>Today's temperature in Chengdu is : </b>{this.state.temp}°C </p>
                    :
                    <p><b>Today's temperature in Chengdu is : </b>{this.state.faren}°F
                    </p>}
                {this.state.temp <= 10?
                <img src={cold} />
            :
            this.state.temp <=20 && this.state.temp >10?
            <img src={mild} />
            :
            <img src={sunny} />}
            </div>
        )
    }
}

export default Data;