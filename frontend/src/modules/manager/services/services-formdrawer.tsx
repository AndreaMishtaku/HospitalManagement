import React, { useEffect, useState } from "react";
import { Button, Form, InputGroup } from "react-bootstrap";
import Drawer from "../../../main/components/drawer";
import { useForm, Controller } from "react-hook-form";
import axios from "axios";
import FormSelect from "../../../main/components/form-elements/select";
import AddWorkingHours from "../../../main/components/form-elements/add-workinghours";

interface IServiceFormDrawer {
  onCancel: () => void;
  onSave: (data: any) => void;
}
const ServiceFormDrawer = (props: IServiceFormDrawer) => {
  const { onCancel, onSave } = props;
  const { control, handleSubmit } = useForm();

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
    <Drawer
      title={`Shto sherbim`}
      show={true}
      actions={
        <>
          <Button onClick={() => onCancel()}>Cancel</Button>
          <Button
            className="mx-2"
            color="success"
            onClick={handleSubmit(onSave)}
          >
            Submit
          </Button>
        </>
      }
      onClose={() => onCancel()}
    >
      {equipments && (
        <>
          <Form.Group className="mb-4">
            <Form.Label>Emri</Form.Label>
            <Controller
              control={control}
              name={"name"}
              defaultValue=""
              render={({
                field: { onChange, value },
                fieldState: { error },
              }) => (
                <>
                  <InputGroup>
                    <Form.Control
                      value={value}
                      onChange={onChange}
                      autoFocus
                      required
                    />
                  </InputGroup>
                </>
              )}
            />
          </Form.Group>
          <Form.Group className="mb-4">
            <Form.Label>Pershkrimi</Form.Label>
            <Controller
              control={control}
              name={"description"}
              defaultValue=""
              render={({
                field: { onChange, value },
                fieldState: { error },
              }) => (
                <>
                  <InputGroup>
                    <Form.Control
                      value={value}
                      onChange={onChange}
                      autoFocus
                      required
                    />
                  </InputGroup>
                </>
              )}
            />
          </Form.Group>
          <Form.Group className="mb-4">
            <Form.Label>Kohezgjatja</Form.Label>
            <Controller
              control={control}
              name={"duration"}
              rules={{ required: true }}
              render={({ field: { onChange, value } }) => (
                <>
                  <Form.Control
                    type="time"
                    placeholder="Kphezgjatja"
                    onChange={onChange}
                    value={value}
                  />
                </>
              )}
            />
          </Form.Group>
          <Form.Group className="mb-4">
            <Controller
              control={control}
              name={"workingHours"}
              render={({ field: { onChange, value } }) => (
                <>
                  <AddWorkingHours value={value} onChange={onChange} />
                </>
              )}
            />
          </Form.Group>
          <Form.Group className="mb-4">
            <Form.Label>Pajisjet</Form.Label>
            <Controller
              control={control}
              name={"equipmentIds"}
              render={({ field: { onChange, value } }) => (
                <>
                  <FormSelect
                    options={equipments.map((e: any) => {
                      return {
                        value: e.id,
                        label: e.name,
                      };
                    })}
                    multiple={true}
                    value={value}
                    onChange={onChange}
                  />
                </>
              )}
            />
          </Form.Group>
        </>
      )}
    </Drawer>
  );
};

export default ServiceFormDrawer;
