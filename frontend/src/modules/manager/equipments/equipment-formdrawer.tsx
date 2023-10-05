import React, { useState } from "react";
import { Button, Form, InputGroup } from "react-bootstrap";
import { Controller, useForm } from "react-hook-form";
import Drawer from "../../../main/components/drawer";

interface IEquipmentDrawer {
  onCancel: () => void;
  onSave: (data: any) => void;
}
const EquipmentDrawer = (props: IEquipmentDrawer) => {
  const { onCancel, onSave } = props;

  const { control, handleSubmit } = useForm();

  return (
    <Drawer
      title={`Shto pajisje`}
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
      <>
        <Form.Group className="mb-4">
          <Form.Label>Emri</Form.Label>
          <Controller
            control={control}
            name={"name"}
            defaultValue=""
            render={({ field: { onChange, value }, fieldState: { error } }) => (
              <>
                <InputGroup>
                  <Form.Control value={value} onChange={onChange} autoFocus />
                </InputGroup>
              </>
            )}
          />
        </Form.Group>
        <Form.Group className="mb-4">
          <Form.Label>Pershkrimi</Form.Label>
          <Controller
            control={control}
            name={"producedAt"}
            defaultValue=""
            render={({ field: { onChange, value }, fieldState: { error } }) => (
              <>
                <InputGroup>
                  <Form.Control
                    type="date"
                    value={value}
                    onChange={onChange}
                    autoFocus
                  />
                </InputGroup>
              </>
            )}
          />
        </Form.Group>
      </>
    </Drawer>
  );
};

export default EquipmentDrawer;
