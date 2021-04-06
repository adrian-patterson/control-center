import React, { Component,  useState, useCallback } from 'react';
import Gallery from "react-photo-gallery";
import Carousel, { Modal, ModalGateway } from "react-images";
import { photos } from '../photos';
import Center from 'react-center';

export class Home extends Component {
    static displayName = Home.name;
    
    render() {

        return (
        <div>
            <Center>
                <h1 className="text-white">Welcome Julian</h1>
            </Center>
            <Center>
                <h3 className="text-white">Oh, Word?</h3>
            </Center>
            <div>
                <Gallery photos={photos} />
            </div>
      </div>
    );
  }
}
