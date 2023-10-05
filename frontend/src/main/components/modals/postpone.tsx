import React, { useEffect, useState } from "react";
import {
  Button,
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter,
} from "react-bootstrap";
import { Form, InputGroup } from "@themesberg/react-bootstrap";
import { useForm, Controller } from "react-hook-form";
import axios from "axios";
import { toast } from "react-toastify";
import Datetime from "react-datetime";
import moment from "moment-timezone";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCalendarAlt } from "@fortawesome/free-solid-svg-icons";
import FormSelect from "../form-elements/select";

interface IPostponeModal {
  open: boolean;
  onClose: () => void;
  reservation: any;
  refresh?: () => void;
}
const PostponeModal = (props: IPostponeModal) => {
  const { open, onClose, reservation, refresh } = props;
  const { control, handleSubmit } = useForm();
  const [availableHours, setAvailableHours] = useState<Array<any>>();
  const [workingDays, setWorkingDays] = useState<Array<any>>();

  const onFormSubmit = async (data: any) => {
    const response = await axios.put(
      `reservations/postpone/${reservation.id}`,
      {
        ...data,
      }
    );

    if (response.data.result) {
      toast.success("Veprimi u krye me sukses");
      refresh();
      onClose();
    }
  };

  const fetchScheduleInfo = async (staffId: number, date: any) => {
    const response = await axios.post("schedule", {
      staffId: staffId,
      date: date,
    });

    if (response.data) {
      setAvailableHours(response.data.availableHours);
    }
  };

  const fetchWorkingDays = async (staffId: number) => {
    const response = await axios.get(`schedule/workingDays/${staffId}`);

    if (response.data) {
      setWorkingDays(
        response.data.map((r: any) => {
          return r.weekday;
        })
      );
    }
  };
  useEffect(() => {
    fetchWorkingDays(reservation.serviceDoctorId);
  }, []);

  const dayConverter = (day: number) => {
    switch (day) {
      case 0:
        return 6;
      default:
        return day - 1;
    }
  };
  return (
    <>
      {workingDays && (
        <Modal show={open} centered>
          <ModalHeader>Kryej check-in</ModalHeader>
          <form onSubmit={handleSubmit(onFormSubmit)}>
            <ModalBody>
              <Form.Group>
                <Form.Label>Data e rezervimit</Form.Label>
                <Controller
                  control={control}
                  name={"date"}
                  defaultValue=""
                  rules={{ required: true }}
                  render={({ field: { onChange, value } }) => (
                    <>
                      <Datetime
                        timeFormat={false}
                        dateFormat={"YYYY-MM-DD"}
                        value={value}
                        onChange={(date) => {
                          onChange(moment(date).format("YYYY-MM-DD"));
                          fetchScheduleInfo(
                            reservation.serviceDoctorId,
                            moment(date).format("YYYY-MM-DD")
                          );
                        }}
                        isValidDate={(date) => {
                          const day = moment(date).day();
                          return (
                            workingDays.includes(dayConverter(day)) &&
                            date > moment().subtract(1, "days")
                          );
                        }}
                        renderInput={(props, openCalendar) => (
                          <InputGroup>
                            <InputGroup.Text>
                              <FontAwesomeIcon icon={faCalendarAlt} />
                            </InputGroup.Text>
                            <Form.Control
                              required
                              type="text"
                              value={value}
                              placeholder="mm/dd/yyyy"
                              onFocus={() => {
                                openCalendar();
                              }}
                            />
                          </InputGroup>
                        )}
                      />
                    </>
                  )}
                />
              </Form.Group>
              {availableHours ? (
                <>
                  {availableHours.length > 0 ? (
                    <Form.Group id="city">
                      <Form.Label>Koha e fillimit</Form.Label>
                      <Controller
                        control={control}
                        name={"startTime"}
                        rules={{ required: true }}
                        render={({ field: { onChange, value } }) => (
                          <FormSelect
                            options={availableHours.map((wd: any) => {
                              return {
                                value: wd,
                                label: wd,
                              };
                            })}
                            value={value}
                            onChange={onChange}
                            required={true}
                          />
                        )}
                      />
                    </Form.Group>
                  ) : (
                    <div
                      style={{
                        textAlign: "center",
                        marginTop: "1.5rem",
                      }}
                    >
                      Nuk ka orare te lira per kete ore
                    </div>
                  )}
                </>
              ) : (
                <div style={{ textAlign: "center", marginTop: "1.5rem" }}>
                  Ju lutem zgjidhni daten per te kontrolluar oraret e lira
                </div>
              )}
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
      )}
    </>
  );
};

export default PostponeModal;
