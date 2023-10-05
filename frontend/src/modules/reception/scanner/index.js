import { Button } from "@themesberg/react-bootstrap";
import React, { useState } from "react";
import { QrReader } from "react-qr-reader";
import { useNavigate } from "react-router-dom";

const Test = (props) => {
  const [isScannerOpen, setIsScannerOpen] = useState(false);
  const navigate = useNavigate();

  return (
    <div>
      <div className="d-flex justify-content-center my-3">
        <div>
          <h5>Klikoni butonin per te skanuar QR Code te rezervimit</h5>
          <div className="d-flex justify-content-center">
            <Button onClick={() => setIsScannerOpen(!isScannerOpen)}>
              {isScannerOpen ? "Stop skanimin" : "Fillo Skanimin"}
            </Button>
          </div>
        </div>
      </div>
      <div className="d-flex justify-content-center">
        {isScannerOpen && (
          <div style={{ width: "350px", height: "350px" }}>
            <QrReader
              onResult={(result, error) => {
                if (result) {
                  navigate(`${result?.text}`);
                }
              }}
            />
          </div>
        )}
      </div>
    </div>
  );
};
export default Test;
