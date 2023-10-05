import React from "react";
import introBanner from "../images/intro-banner.png";

const Intro = () => {
  return (
    <div className="container overflow-hidden my-5 border bg-light shadow-lg rounded-3">
      <div className="row gy-5">
        <div className="col-12 col-md-5">
          <div className="p-3">
            <img className="img-fluid" src={introBanner} alt="" />
          </div>
        </div>
        <div className="col-12 col-md-7">
          <div className="p-3">
            <h3 className="fw-extrabold blue-color">
              Pse te zgjidhni FTI Clinic?
            </h3>
            <p className="fs-5">
              Ne kemi mjekë me cilësi të lartë që janë të gatshëm t'ju ndihmojnë
              të shëroheni. Ne ofrojmë të gjitha llojet e trajtimeve mjekësore.
              Ne kemi laborator të ndryshëm kërkimor dhe mjek ekspertizë. FTI
              Clinic siguron kujdesin më të mirë shëndetësor si dhe shërbimin
              klinik me shërbim personal të jashtëzakonshëm.
            </p>
            <p className="fs-5">
              Motoja e FTI Clinic është se kujdesi ndaj njerëzve është më shumë
              se kujdesi shëndetësor. Ne preferojmë cilësinë mbi çdo gjë.
            </p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Intro;
