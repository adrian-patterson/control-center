import ColorPicker from '@radial-color-picker/react-color-picker';
import '@radial-color-picker/react-color-picker/dist/react-color-picker.min.css';
import { Slider, Button } from 'antd';
import 'antd/dist/antd.css';
import React, { Component } from 'react';
import { BulbOutlined, EllipsisOutlined, BgColorsOutlined, RetweetOutlined, ApartmentOutlined, BugOutlined } from '@ant-design/icons';
import Center from 'react-center';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';


export class JuliansRoom extends Component {
    static displayName = JuliansRoom.name;

    constructor(props) {
        super(props);
        this.state = {
            lightHue: 0,
            lightOn: "true",
            lightBrightness: 255,
            loadings: [],
            selectedSequence: ""
        };
        this.handleHueChange = this.handleHueChange.bind(this);
        this.handleBrightnessChange = this.handleBrightnessChange.bind(this);
        this.tipFormat = this.tipFormat.bind(this);
        this.setRgb = this.setRgb.bind(this);
        this.hslToRgb = this.hslToRgb.bind(this);
        this.handleLedToggle = this.handleLedToggle.bind(this);
        this.handleSequenceSelection = this.handleSequenceSelection.bind(this);
        this.enterLoading = this.enterLoading.bind(this);
    }

    tipFormat = (value) => {
        return value + "%";
    }

    handleHueChange = value => {
        if (Math.abs(this.state.lightHue - value) > 1) {
            console.log("Previous Hue: " + this.state.lightHue)
            this.setState({ lightHue: value, lightOn : true }, () => {
                console.log("New Hue: " + value);
                this.setRgb();
            });
        }
    }

    handleBrightnessChange = value => {
        var brightness = parseInt(value * (255 / 100));
        
        this.setState({ lightBrightness: brightness }, () => {
            console.log("Brightness before setting RGB: " +this.state.lightBrightness);
            this.setRgb();
        });
    }

    handleLedToggle = () => {
        this.setState({ lightOn: false }, () => {
            toast.dark('LEDs Turned Off.', {
                position: "bottom-center",
                autoClose: 4000,
                hideProgressBar: false,
                closeOnClick: true,
                pauseOnHover: true,
                draggable: true,
                progress: undefined,
            });
            this.setRgb();
        });
    }

    hslToRgb = (h, s, l) => {
        h = this.state.lightHue;
        s = 100;
        l = 50;

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

    handleSequenceSelection = value => {
        this.setState({ selectedSequence: value }, () => {
            if (value == "Rainbow") {
                this.enterLoading(1);
                toast('Rainbow Sequence Activated!', {
                    position: "bottom-center",
                    autoClose: 4000,
                    hideProgressBar: false,
                    closeOnClick: true,
                    pauseOnHover: true,
                    draggable: true,
                    progress: undefined,
                });
            }
            if (value == "Carousel") {
                this.enterLoading(2);
                toast.warning('Carousel Sequence Activated!', {
                    position: "bottom-center",
                    autoClose: 4000,
                    hideProgressBar: false,
                    closeOnClick: true,
                    pauseOnHover: true,
                    draggable: true,
                    progress: undefined,
                });
            }
            if (value == "RGB") {
                this.enterLoading(3);
                toast.error('RGB Sequence Activated!', {
                    position: "bottom-center",
                    autoClose: 4000,
                    hideProgressBar: false,
                    closeOnClick: true,
                    pauseOnHover: true,
                    draggable: true,
                    progress: undefined,
                });
            }
            if (value == "Jungle") {
                this.enterLoading(4);
                toast.success('Jungle Sequence Activated!', {
                    position: "bottom-center",
                    autoClose: 4000,
                    hideProgressBar: false,
                    closeOnClick: true,
                    pauseOnHover: true,
                    draggable: true,
                    progress: undefined,
                });
            }
            this.setSequence();
        });
    }

    enterLoading = index => {
        this.setState(({ loadings }) => {
            const newLoadings = [...loadings];
            newLoadings[index] = true;

            return {
                loadings: newLoadings,
            };
        });
        setTimeout(() => {
            this.setState(({ loadings }) => {
                const newLoadings = [...loadings];
                newLoadings[index] = false;

                return {
                    loadings: newLoadings,
                };
            });
        }, 750);
    };



    render() {
        const max = 100;
        const min = 0;
        const { value } = this.state;
        const mid = ((max - min) / 2).toFixed(5);
        const preColorCls = value <= mid ? '' : 'icon-wrapper-active';
        const nextColorCls = value >= mid ? 'icon-wrapper-active' : '';
        const loadings = this.state.loadings;
        return (
            <div>
            <Center>
                <ColorPicker size="large" onChange={hue => this.handleHueChange(hue)} onSelect={this.handleLedToggle} />
            </Center>
            <div className="icon-wrapper">
                    <EllipsisOutlined className={preColorCls} />
                <Slider
                    {... this.props}
                    defaultValue={100}
                    tipFormatter={this.tipFormat}
                    onAfterChange={this.handleBrightnessChange}
                    step={10}
                        />
                <BulbOutlined className={nextColorCls} />
                </div>
            <Center>
            <Button className="sequence-btn"
                type="primary"
                icon={<BgColorsOutlined />}
                loading={loadings[1]}
                onClick={() => this.handleSequenceSelection("Rainbow")}
                size="large"
            >
                    Rainbow
            </Button>
            <Button className="sequence-btn"
                type="primary"
                icon={<RetweetOutlined />}
                loading={loadings[2]}
                onClick={() => this.handleSequenceSelection("Carousel")}
                size="large"
            >
                    Carousel
            </Button>
            </Center>
            <Center>
                <Button className="sequence-btn"
                    type="primary"
                    icon={<ApartmentOutlined />}
                    loading={loadings[3]}
                    onClick={() => this.handleSequenceSelection("RGB")}
                    size="large"
                >
                    RGB
            </Button>
                <Button className="sequence-btn"
                    type="primary"
                    icon={<BugOutlined />}
                    loading={loadings[4]}
                    onClick={() => this.handleSequenceSelection("Jungle")}
                    size="large"
                >
                        Jungle
            </Button>
            </Center>
            <ToastContainer
                position="bottom-center"
                autoClose={5000}
                hideProgressBar={false}
                newestOnTop={false}
                closeOnClick
                rtl={false}
                pauseOnFocusLoss
                draggable
                pauseOnHover
            />
        </div>
    );
    }

    async setRgb() {
        var rgb = this.hslToRgb();

        const axios = require('axios');
        axios.post('/Led/Color', {
            'r': rgb.r,
            'g': rgb.g,
            'b': rgb.b,
            'brightness': this.state.lightBrightness,
            'lightOn': this.state.lightOn
        });
    }

    async setSequence() {
        const axios = require('axios');
        axios.post('/Led/Sequence', {
            'sequence': this.state.selectedSequence
        });
    }
}
