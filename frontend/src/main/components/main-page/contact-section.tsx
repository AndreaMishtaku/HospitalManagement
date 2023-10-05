import React from "react";

const ContactSection = () => {
  return (
    <div className="container px-4 shadow-lg rounded">
      <h2 className="text-center blue-color pt-3">
        <i className="fas fa-clock"></i> Orari
      </h2>
      <div className="row gx-5">
        <div className="col">
          <div className="p-3 w-75 mx-auto">
            <h2 className="fw-bolder blue-color">FTI Clinic</h2>
            <p className="fw-bold text-secondary">
              FTI Clinic siguron kujdesin më të mirë shëndetësor si dhe
              shërbimin klinik me shërbim personal të jashtëzakonshëm. Ne
              garantojmë kujdesin më të mirë shëndetësor si si dhe shërbim
              klinik me shërbim personal të jashtëzakonshëm.
            </p>
            <p className="mt-4">
              <i className="blue-color fas fa-location-arrow"></i> Sheshi Nene
              Tereza Tirane
            </p>
            <p>
              <i className="blue-color fas fa-phone"></i> +355 68 29 27 168
            </p>
            <div className="mb-3 fs-4">
              <i className="fab fa-facebook-square me-4"></i>
              <i className="fab fa-twitter me-4"></i>
              <i className="fab fa-instagram me-4"></i>
              <i className="fab fa-linkedin-in me-4"></i>
            </div>
          </div>
        </div>
        <div className="col">
          <div className="p-3 w-75 mx-auto">
            <h4 className="fw-bolder">Hapur</h4>
            <table className="table bluish-bg text-secondary fw-bold rounded">
              <tbody>
                <tr>
                  <td>E Hene</td>
                  <td>09:00 - 19:00</td>
                </tr>
                <tr>
                  <td>E Marte</td>
                  <td>09:00 - 19:00</td>
                </tr>
                <tr>
                  <td>E Merkure</td>
                  <td>09:00 - 19:00</td>
                </tr>
                <tr>
                  <td>E Enjte</td>
                  <td>09:00 - 19:00</td>
                </tr>
                <tr>
                  <td>E Premte</td>
                  <td>09:00 - 19:00</td>
                </tr>
                <tr>
                  <td>E Shtune</td>
                  <td>10:00 - 18:00</td>
                </tr>
                <tr>
                  <td>E Diel</td>
                  <td>Urgjenca</td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ContactSection;
