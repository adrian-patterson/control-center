import React, { Component } from 'react';

export class Cloud extends Component {
  static displayName = Cloud.name;

  constructor(props) {
    super(props);
    this.state = { currentCount: 0 };
    this.incrementCloud = this.incrementCloud.bind(this);
  }

  incrementCloud() {
    this.setState({
      currentCount: this.state.currentCount + 1
    });
  }

  render() {
    return (
      <div>
        <h1>Cloud</h1>

        <p>This is a simple example of a React component.</p>

        <p aria-live="polite">Current count: <strong>{this.state.currentCount}</strong></p>

        <button className="btn btn-primary" onClick={this.incrementCloud}>Increment</button>
      </div>
    );
  }
}
