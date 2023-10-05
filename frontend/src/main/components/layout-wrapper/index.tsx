import React from 'react';
import Preloader from '../loader';
import Sidebar from '../menu';
import { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

const LayoutWrapper = (props: any) => {
  const { children } = props;
  const [loaded, setLoaded] = useState(false);

  useEffect(() => {
    const timer = setTimeout(() => setLoaded(true), 1000);
    return () => clearTimeout(timer);
  }, []);

  return (
    <>
      <Preloader show={loaded ? false : true} />
      <Sidebar />
      <main className="content py-5 px-3">{children}</main>
    </>
  );
};

export default LayoutWrapper;
