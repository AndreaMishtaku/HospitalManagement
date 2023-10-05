import React, { useEffect, useState } from 'react';
import { Button, Form, InputGroup } from 'react-bootstrap';
import Drawer from '../../../main/components/drawer';
import { useForm, Controller } from 'react-hook-form';
import axios from 'axios';
import FormSelect from '../../../main/components/form-elements/select';
interface IServiceStaffDrawer {
  onCancel: () => void;
  onSave: (data: any) => void;
}
const ServiceStaffDrawer = (props: IServiceStaffDrawer) => {
  const { onCancel, onSave } = props;
  const { control, handleSubmit } = useForm();
  const [services, setServices] = useState<any>();
  const [staff, setStaff] = useState<any>();

  const fetchServices = async () => {
    const response = await axios.get('services');

    if (response.data) {
      setServices(
        response.data.map((s: any) => {
          return {
            value: s.id,
            label: s.name,
          };
        })
      );
    }
  };

  const fetchStaff = async () => {
    const response = await axios.get('users');
    if (response.data) {
      setStaff(
        response.data
          .filter((x: any) => x.specialization != null)
          .map((s: any) => {
            return {
              value: s.id,
              label: s.firstName + ' ' + s.lastName,
            };
          })
      );
    }
  };

  useEffect(() => {
    fetchStaff();
    fetchServices();
  }, []);

  return (
    <>
      {services && staff && (
        <Drawer
          title={`Shto staff per sherbim`}
          show={true}
          actions={
            <>
              <Button onClick={() => onCancel()}>Cancel</Button>
              <Button className="mx-2" color="success" onClick={handleSubmit(onSave)}>
                Submit
              </Button>
            </>
          }
          onClose={() => onCancel()}
        >
          <>
            <Form.Group className="mb-4">
              <Form.Label>Staffi</Form.Label>
              <Controller
                control={control}
                name={'staffId'}
                render={({ field: { onChange, value } }) => (
                  <>
                    <FormSelect options={staff} value={value} onChange={onChange} required={true} />
                  </>
                )}
              />
            </Form.Group>
            <Form.Group className="mb-4">
              <Form.Label>Sherbimet</Form.Label>
              <Controller
                control={control}
                name={'serviceId'}
                defaultValue="Doctor"
                render={({ field: { onChange, value } }) => (
                  <>
                    <FormSelect options={services} value={value} onChange={onChange} required={true} />
                  </>
                )}
              />
            </Form.Group>
          </>
        </Drawer>
      )}
    </>
  );
};

export default ServiceStaffDrawer;
