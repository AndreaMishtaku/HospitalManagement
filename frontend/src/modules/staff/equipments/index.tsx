import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import axios from "axios";
import React, { useEffect, useState } from "react";
import { ListGroup } from "react-bootstrap";

const StaffEquipments = () => {
  const [equipments, setEquipments] = useState<Array<any>>();

  const fetchEquipments = async () => {
    const result = await axios.get("equipments");

    if (result.data) {
      setEquipments(result.data);
    }
  };

  useEffect(() => {
    fetchEquipments();
  }, []);

  return (
    <>
      {equipments ? (
        <div>
          <ListGroup variant="flush">
            {equipments.map((e: any, index: number) => {
              return (
                <ListGroup.Item
                  className="d-flex py-4 gap-5 fs-5"
                  key={`equipments${index}`}
                >
                  <div>{index + 1}</div>
                  <div className="fw-bold fst-italic text-primary">
                    {e.name}
                  </div>
                  <div>Prodhuar me {e.producedAt.split("T")[0]}</div>
                </ListGroup.Item>
              );
            })}
          </ListGroup>
        </div>
      ) : (
        <div style={{ display: "flex", justifyContent: "center" }}>
          <div className="spinner-border" role="status">
            <span className="visually-hidden">Loading...</span>
          </div>
        </div>
      )}
    </>
  );
};

export default StaffEquipments;
