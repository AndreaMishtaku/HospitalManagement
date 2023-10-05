import React from 'react';
import Banner from '../main/components/main-page/banner';
import ContactSection from '../main/components/main-page/contact-section';
import Intro from '../main/components/main-page/intro';
import Header from '../main/components/main-page/header';
import Footer from '../main/components/main-page/footer';

const Home = () => {
  return (
    <div>
      <Header />
      <Banner />
      <Intro />
      <ContactSection />
      <Footer />
    </div>
  );
};

export default Home;
