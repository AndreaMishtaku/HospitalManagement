import React from 'react';
import { useState } from 'react';
import { Carousel } from 'react-bootstrap';

import banner1 from '../images/banner1.jpg';
import banner2 from '../images/banner2.jpg';
import banner3 from '../images/banner3.jpg';

const Banner = () => {
  const [index, setIndex] = useState(0);
  const handleSelect = (selectedIndex, e) => {
    setIndex(selectedIndex);
  };

  return (
    <Carousel activeIndex={index} onSelect={handleSelect} pause={false} style={{height:"50%"}}>
      <Carousel.Item>
        <img className="d-block " src={banner1} alt="First slide"  style={{ height:"500px", width: '70%',marginInline:'auto'}}/>
        <Carousel.Caption className="rounded-3" style={{fontSize: '1.4rem',background: 'rgba(168, 197, 209, 0.459)',borderRadius: '12px'}}>
          <h3>FTI Clinic</h3>
          <p>Ne sigurojmë kujdesin më të mirë shëndetësor si dhe shërbimin klinik</p>
        </Carousel.Caption>
      </Carousel.Item>
      <Carousel.Item>
        <img className="d-block" src={banner2} alt="Second slide" style={{height:"500px", width: '70%',marginInline:'auto'}}/>

        <Carousel.Caption style={{fontSize: '1.4rem',background: 'rgba(168, 197, 209, 0.459)',borderRadius: '12px'}}>
          <h3>FTI Clinic</h3>
          <p>Ne sigurojmë kujdesin më të mirë shëndetësor si dhe shërbimin klinik</p>
        </Carousel.Caption>
      </Carousel.Item>
      <Carousel.Item>
        <img className="d-block " src={banner3} alt="Third slide" style={{height:"500px",  width: '70%',marginInline:'auto'}}/>

        <Carousel.Caption style={{fontSize: '1.4rem',background: 'rgba(168, 197, 209, 0.459)',borderRadius: '12px'}}>
          <h3>FTI Clinic</h3>
          <p>Ne sigurojmë kujdesin më të mirë shëndetësor si dhe shërbimin klinik</p>
        </Carousel.Caption>
      </Carousel.Item>
    </Carousel>
  );
};

export default Banner;
