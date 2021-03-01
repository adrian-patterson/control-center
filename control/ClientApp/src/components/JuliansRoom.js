import React, { Component } from 'react';

export class JuliansRoom extends Component {
  static displayName = JuliansRoom.name;

  constructor(props) {
    super(props);
      this.state = { lightOn: false };
      this.turnOnLight = this.turnOnLight.bind(this);
  }

  componentDidMount() {
      var data = {
          "lightOn": this.state.lightOn
      }
    }

    

  render() {
    return (
      <div>
            <button className="btn btn-primary" onClick={this.turnOnLight}>Toggle LED</button>
      </div>
    );
  }

    async turnOnLight() {

        const axios = require('axios');

        axios.post('/Lighting', {
            "toggle" : JSON.stringify(this.state.lightOn)
            })
                .then(function (response) {
                    console.log(response);
                })
                .catch(function (error) {
                    console.log(error);
                });
        this.state.lightOn = !this.state.lightOn;
        console.log("STATE OF LIGHT ON: " + this.state.lightOn);
    }
}
