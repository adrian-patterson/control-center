import ColorPicker from '@radial-color-picker/react-color-picker';
import '@radial-color-picker/react-color-picker/dist/react-color-picker.min.css';
import { Slider } from 'antd';
import 'antd/dist/antd.css';
import React, { Component } from 'react';
import { BulbOutlined, EllipsisOutlined, CaretDownOutlined, CaretUpOutlined } from '@ant-design/icons';


export class JuliansRoom extends Component {
    static displayName = JuliansRoom.name;

    constructor(props) {
        super(props);
        this.state = {
            lightHue: 0,
            lightSaturation: 100,
            lightLuminosity: 50,
        };
        this.handleHueChange = this.handleHueChange.bind(this);
        this.handleSaturationChange = this.handleSaturationChange.bind(this);
        this.handleLuminosityChange = this.handleLuminosityChange.bind(this);
        this.tipFormat = this.tipFormat.bind(this);
        this.updateLed = this.updateLed.bind(this);
        this.HSLtoRGB = this.HSLtoRGB.bind(this);
    }

    tipFormat = (value) => {
        return value + "%";
    }

    handleHueChange = value => {
        if (Math.abs(this.state.lightHue - value) > 1) {
            console.log("Previous Hue: " + this.state.lightHue)
            this.setState({ lightHue: value }, () => {
                console.log("New Hue: " + value);
                this.updateLed();
            });
        }
    }

    handleSaturationChange = value => {
        this.setState({ lightSaturation: value }, () => {
            this.updateLed();
        });
    }

    handleLuminosityChange = value => {
        this.setState({ lightLuminosity: value }, () => {
            this.updateLed();
        });
    }

    HSLtoRGB = (h, s, l) => {
        h = this.state.lightHue;
        s = this.state.lightSaturation;
        l = this.state.lightLuminosity;

        var r, g, b, m, c, x

        if (!isFinite(h)) h = 0
        if (!isFinite(s)) s = 0
        if (!isFinite(l)) l = 0

        h /= 60
        if (h < 0) h = 6 - (-h % 6)
        h %= 6

        s = Math.max(0, Math.min(1, s / 100))
        l = Math.max(0, Math.min(1, l / 100))

        c = (1 - Math.abs((2 * l) - 1)) * s
        x = c * (1 - Math.abs((h % 2) - 1))

        if (h < 1) {
            r = c
            g = x
            b = 0
        } else if (h < 2) {
            r = x
            g = c
            b = 0
        } else if (h < 3) {
            r = 0
            g = c
            b = x
        } else if (h < 4) {
            r = 0
            g = x
            b = c
        } else if (h < 5) {
            r = x
            g = 0
            b = c
        } else {
            r = c
            g = 0
            b = x
        }

        m = l - c / 2
        r = Math.round((r + m) * 255)
        g = Math.round((g + m) * 255)
        b = Math.round((b + m) * 255)

        return { r: r, g: g, b: b }
    }


    render() {
        const max = 100;
        const min = 0;
        const { value } = this.state;
        const mid = ((max - min) / 2).toFixed(5);
        const preColorCls = value >= mid ? '' : 'icon-wrapper-active';
        const nextColorCls = value >= mid ? 'icon-wrapper-active' : '';

    return (
        <div className=".color-wheel-and-slider">
            <ColorPicker onInput={hue => this.handleHueChange(hue)} />
            <p>Saturation</p>
            <div className="icon-wrapper">
                <EllipsisOutlined className={preColorCls} />
                <Slider
                    {... this.props}
                    defaultValue={100}
                    tipFormatter={this.tipFormat}
                    onChange={this.handleSaturationChange}
                />
                <BulbOutlined className={nextColorCls} />
            </div>

            <p>Luminosity</p>
            <div className="icon-wrapper">
                <CaretDownOutlined className={preColorCls} />
                <Slider
                    {... this.props}
                    defaultValue={50}
                    tipFormatter={this.tipFormat}
                    onChange={this.handleLuminosityChange}
                />
                <CaretUpOutlined className={nextColorCls} />
            </div>
    </div>
    );
    }

    async updateLed() {
        var rgb = this.HSLtoRGB();
        console.log("R: " + rgb.r + "\tG: " + rgb.g + "\tB: " + rgb.b);
        const axios = require('axios');
        axios.post('/Lighting', {
            r: rgb.r,
            g: rgb.g,
            b: rgb.b
        });
    }
}
