import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import axios from 'axios';
import React, { useEffect, useState } from 'react';
import { ListGroup } from 'react-bootstrap';
import EquipmentDrawer from './equipment-formdrawer';

const ManagerEquipments = () => {
  const [openDrawer, setOpenDrawer] = useState(false);
  const [equipments, setEquipments] = useState<Array<any>>();

  const fetchEquipments = async () => {
    const result = await axios.get('equipments');

    if (result.data) {
      setEquipments(result.data);
    }
  };

  useEffect(() => {
    fetchEquipments();
  }, []);

  const handleFormSubmit = async (data: any) => {
    const result = await axios.post('equipments', data);

    if (result.data) {
      fetchEquipments();
      setOpenDrawer(false);
    }
  };
  return (
    <>
      {equipments ? (
        <div>
          <div className="d-flex my-4 ms-1">
            <button className="me-3 btn btn-warning" onClick={() => setOpenDrawer(true)}>
              <FontAwesomeIcon icon={'fa-solid fa-plus' as any} className="me-1" />
              Shto
            </button>
          </div>
          <ListGroup variant="flush">
            {equipments.map((e: any, index: number) => {
              return (
                <ListGroup.Item className="d-flex py-4 gap-5 fs-5" key={`equipments${index}`}>
                  <div>{index + 1}</div>
                  <div className="fw-bold fst-italic text-primary">{e.name}</div>
                  <div>Prodhuar me {e.producedAt.split('T')[0]}</div>
                </ListGroup.Item>
              );
            })}
          </ListGroup>
          {openDrawer && <EquipmentDrawer onCancel={() => setOpenDrawer(false)} onSave={handleFormSubmit} />}
        </div>
      ) : (
        <div className="spinner-border" role="status">
          <span className="visually-hidden">Loading...</span>
        </div>
      )}
    </>
  );
};

export default ManagerEquipments;
