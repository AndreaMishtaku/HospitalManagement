import React, { useState } from 'react';
import { GenericTable } from '../../../main/components/table';
import eFormMode from '../../../main/assets/enums/eFormMode';
import EmployeeFormDrawer from './employee-formdrawer';
import axios from 'axios';
import EventManager from '../../../main/utils/eventManager';
import { Button } from 'react-bootstrap';
import ServiceStaffDrawer from './staff-service-drawer';

const ManagerEmployees = () => {
  const [mode, setMode] = useState<eFormMode>();
  const [model, setModel] = useState<any>();

  const [openServiceStaffDrawer, setServiceStaffDrawer] = useState(false);

  const handleAddNewRecord = () => {
    setMode(eFormMode.Insert);
  };

  const handleFormSubmit = async (data: any) => {
    const result = await axios.post('/users', data);
    if (result) {
      EventManager.raiseRefreshTable('users');
      setMode(undefined);
    }
  };

  const handleStaffSubmit = async (data: any) => {
    const result = await axios.post('/staff', data);
    if (result) {
      setServiceStaffDrawer(false);
    }
  };
  return (
    <div>
      <Button className="mx-4 mb-4" onClick={() => setServiceStaffDrawer(true)}>
        Shto staff
      </Button>
      <GenericTable controller={'users'} onAdd={handleAddNewRecord} />
      {(model || mode) && <EmployeeFormDrawer mode={mode} onCancel={() => setMode(undefined)} onSave={handleFormSubmit} />}
      {openServiceStaffDrawer && <ServiceStaffDrawer onCancel={() => setServiceStaffDrawer(false)} onSave={handleStaffSubmit} />}
    </div>
  );
};

export default ManagerEmployees;
