import React, { useState } from "react";
import {
  Button,
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter,
} from "react-bootstrap";
import { Card, Form, InputGroup } from "@themesberg/react-bootstrap";
import { useForm, Controller } from "react-hook-form";
import axios from "axios";
import { toast } from "react-toastify";

interface ICheckInModal {
  open: boolean;
  onClose: () => void;
  startTime: any;
  reservationId: number;
  checkIn: (data: boolean) => void;
}
const CheckInModal = (props: ICheckInModal) => {
  const { open, onClose, startTime, reservationId, checkIn } = props;

  const { control, handleSubmit } = useForm();

  const onFormSubmit = async (data: any) => {
    const response = await axios.post("check-ins", {
      reservationId: reservationId,
      checkInStartTime: data.checkInStartTime,
    });

    if (response.data.result) {
      toast.success("Veprimi u krye me sukses");
      checkIn(true);
      onClose();
    }
  };
  return (
    <Modal show={open} centered>
      <ModalHeader>Kryej check-in</ModalHeader>
      <form onSubmit={handleSubmit(onFormSubmit)}>
        <ModalBody>
          <Form.Group>
            <Form.Label>Koha e fillimit te sherbimit</Form.Label>
            <Controller
              control={control}
              name={"checkInStartTime"}
              defaultValue=""
              render={({ field: { onChange, value } }) => (
                <>
                  <Form.Control
                    onChange={onChange}
                    type="time"
                    value={value}
                    required
                    min={startTime}
                  />
                </>
              )}
            />
          </Form.Group>
        </ModalBody>
        <ModalFooter>
          <Button variant="danger" onClick={onClose}>
            Kthehu
          </Button>
          <Button variant="warning" type="submit">
            Vazhdo
          </Button>
        </ModalFooter>
      </form>
    </Modal>
  );
};

export default CheckInModal;
