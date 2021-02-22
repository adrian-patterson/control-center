import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <h1>Welcome</h1>
        <h4>Objective for the welcome page:</h4>
        <ul>
          <li>Display <strong>Weather</strong></li>
          <li><strong>Login page</strong> for personalized tools/pages (room temp, light-activation, and other specific functions)</li>
            <li>Display your daily calendar by linking with google calendar</li>
            <li>Etc.</li>
            </ul>
            <h4>Other Pages</h4>
        <ol>
          <li><strong>Cloud Page</strong> for locally hosted cloud drive</li>
          <li><strong>Lighting page </strong>(Julian's room)</li>
          <li><strong>Other device pages</strong> to link real-time sensors/speakers/devices to the website</li>
        </ol>
      </div>
    );
  }
}
