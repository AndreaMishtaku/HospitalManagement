import React, { useEffect, useState } from "react";
import {
  Card,
  Button,
  Form,
  Row,
  Col,
  Tabs,
  Tab,
  Spinner,
  InputGroup,
} from "@themesberg/react-bootstrap";
import Header from "../main/components/main-page/header";
import Footer from "../main/components/main-page/footer";
import axios from "axios";
import FormSelect from "../main/components/form-elements/select";
import { useForm, Controller } from "react-hook-form";
import { Steps } from "rsuite";
import "rsuite/dist/rsuite.css";
import { Link } from "react-router-dom";
import { toast } from "react-toastify";
import moment from "moment-timezone";
import Datetime from "react-datetime";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCalendarAlt } from "@fortawesome/free-solid-svg-icons";
import Bill from "../main/components/bill";
import QRCode from "qrcode.react";
import { PDFDownloadLink } from "@react-pdf/renderer";

const Reservation = () => {
  const { control, getValues, handleSubmit, reset } = useForm();
  const [staffService, setStaffService] = useState();
  const [step, setStep] = useState(0);
  const [status, setStatus] = useState<"wait" | "finish" | "process" | "error">(
    "wait"
  );

  const [reservation, setReservation] = useState<any>();

  const [workingDays, setWorkingDays] = useState<Array<any>>();
  const [availableHours, setAvailableHours] = useState<Array<any>>();

  const [rForm, setRForm] = useState(1);

  const fetchServices = async () => {
    const response = await axios.get("staff");

    if (response.data) {
      setStaffService(
        response.data.map((x: any) => {
          return {
            value: x.id,
            label:
              x.staffSpecialization +
              ": " +
              x.doctorFirstName +
              " " +
              x.doctorLastName +
              " -> " +
              x.serviceName,
          };
        })
      );
    }
  };

  useEffect(() => {
    fetchServices();
  }, []);

  const onFormSubmit = async (data: any) => {
    const response = await axios.post(
      rForm == 1 ? "reservations/first-time" : "reservations/more-than-once",
      data
    );

    if (response.data.result) {
      setStep(step + 1);
      setReservation({ reservationId: response.data.reservationId, ...data });
    }
  };

  const checkifExistingClient = async (identityNumber: string) => {
    const response = await axios.post("clients/getBy-identityNumber", {
      identityNumber: identityNumber,
    });

    if (response) {
      return response.data;
    } else {
      return null;
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

  const dayConverter = (day: number) => {
    switch (day) {
      case 0:
        return 6;
      default:
        return day - 1;
    }
  };
  return (
    <div className="d-flex flex-column justify-content-between vh-100">
      <Header />
      <>
        <Card border="light" className="bg-white shadow-sm m-4">
          <Card.Body>
            <div className="my-3">
              <Steps current={step} currentStatus={status}>
                <Steps.Item title="Te dhenat" />
                <Steps.Item title="Datat e rezervimit" />
                <Steps.Item title="Statusi" />
              </Steps>
            </div>
            <form
              onSubmit={handleSubmit(onFormSubmit)}
              style={{ height: step != 2 ? "600px" : "150px" }}
            >
              {step == 0 && staffService && (
                <Tabs defaultActiveKey="1" className="mb-3" fill>
                  <Tab
                    eventKey="1"
                    title="Rezervim per here te pare"
                    onClick={() => setRForm(1)}
                  >
                    <>
                      <h5 className="mb-3">Te dhenat e klientit</h5>
                      <Row>
                        <Col md={6} className="mb-3">
                          <Form.Group id="firstName">
                            <Form.Label>Emri</Form.Label>
                            <Controller
                              control={control}
                              name={"firstName"}
                              defaultValue=""
                              render={({ field: { onChange, value } }) => (
                                <Form.Control
                                  onChange={onChange}
                                  value={value}
                                  type="text"
                                  placeholder="Emri"
                                />
                              )}
                            />
                          </Form.Group>
                        </Col>
                        <Col md={6} className="mb-3">
                          <Form.Group id="lastName">
                            <Form.Label>Mbiemri</Form.Label>
                            <Controller
                              control={control}
                              name={"lastName"}
                              defaultValue=""
                              render={({ field: { onChange, value } }) => (
                                <Form.Control
                                  onChange={onChange}
                                  value={value}
                                  type="text"
                                  placeholder="Mbiemri"
                                />
                              )}
                            />
                          </Form.Group>
                        </Col>
                      </Row>
                      <Row>
                        <Col md={6} className="mb-3">
                          <Form.Group id="email">
                            <Form.Label>Email</Form.Label>
                            <Controller
                              control={control}
                              name={"email"}
                              defaultValue=""
                              render={({ field: { onChange, value } }) => (
                                <Form.Control
                                  onChange={onChange}
                                  value={value}
                                  type="email"
                                  placeholder="Email"
                                />
                              )}
                            />
                          </Form.Group>
                        </Col>
                        <Col md={6} className="mb-3">
                          <Form.Group id="identityNumber">
                            <Form.Label>Numri i identifikimit</Form.Label>
                            <Controller
                              control={control}
                              name={"identityNumber"}
                              defaultValue=""
                              render={({ field: { onChange, value } }) => (
                                <Form.Control
                                  onChange={onChange}
                                  value={value}
                                  min="8"
                                  max="12"
                                  type="text"
                                  placeholder="Numri i identifikimit"
                                />
                              )}
                            />
                          </Form.Group>
                        </Col>
                      </Row>
                      <Row className="align-items-center">
                        <Col md={6} className="mb-3">
                          <Form.Group id="birthday">
                            <Form.Label>Birthday</Form.Label>
                            <Controller
                              control={control}
                              name={"birthday"}
                              defaultValue=""
                              render={({ field: { onChange, value } }) => (
                                <Form.Control
                                  type="date"
                                  onChange={onChange}
                                  value={value}
                                />
                              )}
                            />
                          </Form.Group>
                        </Col>
                        <Col md={6} className="mb-3">
                          <Form.Group id="gender">
                            <Form.Label>Gender</Form.Label>
                            <Controller
                              control={control}
                              name={"gender"}
                              render={({ field: { onChange, value } }) => (
                                <Form.Select onChange={onChange} value={value}>
                                  <option value="Female">Female</option>
                                  <option value="Male">Male</option>
                                </Form.Select>
                              )}
                            />
                          </Form.Group>
                        </Col>
                      </Row>
                      <h5 className="my-3">Zgjidh sherbimin</h5>
                      <Row>
                        <Col md={12} className="mb-3">
                          <Form.Group id="servicey">
                            <Form.Label>Sherbimi</Form.Label>
                            <Controller
                              control={control}
                              name={"staffId"}
                              rules={{ required: true }}
                              render={({ field: { onChange, value } }) => (
                                <FormSelect
                                  options={staffService}
                                  value={value}
                                  onChange={(value) => {
                                    onChange(value);
                                    fetchWorkingDays(value);
                                  }}
                                />
                              )}
                            />
                          </Form.Group>
                        </Col>
                      </Row>
                      <div className="d-flex mt-3 justify-content-end ">
                        <Button
                          variant="primary"
                          onClick={() => {
                            if (
                              getValues("firstName") &&
                              getValues("lastName") &&
                              getValues("email") &&
                              getValues("identityNumber") &&
                              getValues("birthday") &&
                              getValues("gender") &&
                              getValues("staffId")
                            ) {
                              setStep(step + 1);
                            } else {
                              toast.error("Keni fusha te paplotesuara");
                              setStatus("error");
                            }
                          }}
                        >
                          Vazhdo
                        </Button>
                      </div>
                    </>
                  </Tab>
                  <Tab
                    eventKey="2"
                    title="Rezervim me shume se 1 here"
                    onClick={() => setRForm(2)}
                  >
                    <>
                      <h5 className="mb-3">Te dhenat e klientit</h5>

                      <Row>
                        <Col md={12} className="mb-3">
                          <Form.Group id="identityNumber">
                            <Form.Label>Numri i identifikimit</Form.Label>
                            <Controller
                              control={control}
                              name={"identityNumber"}
                              defaultValue=""
                              render={({ field: { onChange, value } }) => (
                                <>
                                  <Form.Control
                                    onChange={onChange}
                                    value={value}
                                    required
                                    type="text"
                                    min="8"
                                    placeholder="Numri i identifikimit"
                                  />
                                </>
                              )}
                            />
                          </Form.Group>
                        </Col>
                      </Row>

                      <h5 className="my-3">Zgjidh sherbimin</h5>
                      <Row>
                        <Col md={12} className="mb-3">
                          <Form.Group id="serviceStaff">
                            <Form.Label>Sherbimi</Form.Label>

                            <Controller
                              control={control}
                              name={"staffId"}
                              render={({ field: { onChange, value } }) => (
                                <>
                                  <FormSelect
                                    options={staffService}
                                    value={value}
                                    onChange={onChange}
                                    required={true}
                                  />
                                </>
                              )}
                            />
                          </Form.Group>
                        </Col>
                      </Row>
                      <div className="d-flex mt-3 justify-content-end ">
                        <Button
                          variant="primary"
                          onClick={async () => {
                            if (
                              getValues("identityNumber") &&
                              getValues("staffId")
                            ) {
                              const checkExisting = await checkifExistingClient(
                                getValues("identityNumber")
                              );

                              if (checkExisting != null) {
                                setStep(step + 1);
                              } else {
                                setStatus("error");
                              }
                            } else {
                              toast.error("Keni fusha te paplotesuara");
                            }
                          }}
                        >
                          Vazhdo
                        </Button>
                      </div>
                    </>
                  </Tab>
                </Tabs>
              )}
              {!staffService && (
                <div
                  style={{
                    display: "flex",
                    justifyContent: "center",
                    height: "100%",
                  }}
                >
                  <Spinner
                    animation="border"
                    role="status"
                    style={{ marginBlock: "auto" }}
                  >
                    <span className="visually-hidden">Loading...</span>
                  </Spinner>
                </div>
              )}
              {step == 1 && workingDays && (
                <>
                  <h5 className="my-3">Detajet e rezervimit</h5>
                  <Row>
                    <Col sm={12} className="mb-3">
                      <Form.Group id="reason">
                        <Form.Label>Arsyeja</Form.Label>
                        <Controller
                          control={control}
                          name={"reason"}
                          defaultValue={""}
                          rules={{ required: true }}
                          render={({ field: { onChange, value } }) => (
                            <>
                              <Form.Control
                                type="text"
                                placeholder="Arsyeja e rezervimit"
                                multiple={true}
                                onChange={onChange}
                                value={value}
                              />
                            </>
                          )}
                        />
                      </Form.Group>
                    </Col>
                  </Row>
                  <Row>
                    <Col sm={6} className="mb-3">
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
                                    getValues("staffId"),
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
                    </Col>
                    <Col sm={6} className="mb-3">
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
                        <div
                          style={{ textAlign: "center", marginTop: "1.5rem" }}
                        >
                          Ju lutem zgjidhni daten per te kontrolluar oraret e
                          lira
                        </div>
                      )}
                    </Col>
                  </Row>
                  <div className="d-flex mt-3 justify-content-between">
                    <Button
                      variant="primary"
                      onClick={() => {
                        setStep(step - 1);
                        reset();
                      }}
                    >
                      Pas
                    </Button>
                    <Button variant="primary" type="submit">
                      Rezervo
                    </Button>
                  </div>
                </>
              )}
              {!workingDays && step == 1 && (
                <div
                  style={{
                    display: "flex",
                    justifyContent: "center",
                    height: "100%",
                  }}
                >
                  <Spinner
                    animation="border"
                    role="status"
                    style={{ marginBlock: "auto" }}
                  >
                    <span className="visually-hidden">Loading...</span>
                  </Spinner>
                </div>
              )}
            </form>
            {step == 2 && (
              <>
                <div className="d-flex justify-content-center">
                  <div className="alert alert-success w-50" role="alert">
                    Rezervimi u krye me sukses
                  </div>
                </div>
                {reservation && (
                  <>
                    <div className="d-flex justify-content-center">
                      <QRCode
                        id={`reservation${reservation.reservationId}`}
                        value={`/reservation/${reservation.reservationId}`}
                        size={200}
                        bgColor="#FFF"
                        fgColor="#000"
                        includeMargin
                        level={"H"}
                      />
                    </div>
                    <div className="d-flex justify-content-center">
                      <PDFDownloadLink
                        document={<Bill reservation={reservation} />}
                        fileName="qrcode.pdf"
                      >
                        {({ loading }) =>
                          loading ? "Loading..." : "Download PDF"
                        }
                      </PDFDownloadLink>
                    </div>
                  </>
                )}
                <div className="d-flex justify-content-center my-5">
                  <Link to={"/"}>Kthehu ne faqen kryesore</Link>
                </div>
              </>
            )}
          </Card.Body>
        </Card>
      </>
      <Footer />
    </div>
  );
};

export default Reservation;
