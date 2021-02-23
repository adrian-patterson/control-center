import React, { Component } from 'react';

export class JuliansRoom extends Component {
  static displayName = JuliansRoom.name;

  constructor(props) {
    super(props);
      this.state = { lightOn: true };
      this.turnOnLight = this.turnOnLight.bind(this);
  }

  componentDidMount() {
    // call turn on light function, where HTTP post is used to send on signal to back-end
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
    }
}
