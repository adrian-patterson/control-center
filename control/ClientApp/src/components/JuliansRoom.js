import React, { Component } from 'react';
import axios from 'axios';

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
        alert("LED toggled");
        this.state.lightOn = !this.state.lightOn;

        const axios = require('axios');
        axios.post('/Lighting', {
            toggle: 'yes'
        })
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
                console.log(error);
            });
    }
}
