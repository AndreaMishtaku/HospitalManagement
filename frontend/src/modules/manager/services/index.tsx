import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import axios from "axios";
import React, { useEffect, useState } from "react";
import { Alert, Card, Col, Nav, Row, Tab } from "react-bootstrap";
import { useNavigate, useParams } from "react-router";
import ServiceFormDrawer from "./services-formdrawer";

const ManagerServices = () => {
  const params = useParams();
  const { id } = params;
  const navigate = useNavigate();

  const [openDrawer, setOpenDrawer] = useState(false);
  const [services, setServices] = useState<Array<any>>();
  const [serviceDetail, setServiceDetail] = useState();

  const fetchServices = async () => {
    const result = await axios.get("services");

    if (result.data) {
      setServices(result.data);
    }
  };

  const fetchServiceDetailById = async (serviceId: any) => {
    const result = await axios.get(`services/${serviceId}`);

    if (result.data) {
      setServiceDetail(result.data);
    }
  };

  useEffect(() => {
    fetchServices();
  }, []);

  const handleFormSubmit = async (data: any) => {
    const result = await axios.post("services", data);

    console.log(result, "Test");
    if (result.data) {
      fetchServices();
      setOpenDrawer(false);
    }
  };

  const getWeekDay = (num: number) => {
    switch (num) {
      case 0:
        return "E Hene";
      case 1:
        return "E Marte";
      case 2:
        return "E Merkure";
      case 3:
        return "E Enjte";
      case 4:
        return "E Premte";
      case 5:
        return "E Shtune";
      case 6:
        return "E Diel";
    }
  };

  return (
    <Card>
      <Card.Header className=" d-flex fs-4 fw-bold text-center justify-content-between">
        <div>Informacion mbi sherbimet</div>
        <div className="d-flex  ms-1">
          <button
            className="me-3 btn btn-warning"
            onClick={() => setOpenDrawer(true)}
          >
            <FontAwesomeIcon
              icon={"fa-solid fa-plus" as any}
              className="me-1"
            />
            Shto
          </button>
        </div>
      </Card.Header>
      {services ? (
        <Card.Body>
          {services.length > 0 ? (
            <Tab.Container
              id="left-tabs-example"
              defaultActiveKey="default"
              onSelect={(eventKey: any) => {
                fetchServiceDetailById(eventKey);
              }}
            >
              <Row>
                <Col sm={4}>
                  <Nav variant="pills" className="flex-column">
                    {services.map((s: any) => {
                      return (
                        <Nav.Item
                          key={`service${s.id}`}
                          style={{
                            fontSize: "18px",
                            padding: "5px",
                            cursor: "pointer",
                          }}
                        >
                          <Nav.Link
                            eventKey={`${s.id}`}
                            className="text-info fw-bold fst-italic"
                            style={{ backgroundColor: "lightgray" }}
                          >
                            {s.name}
                          </Nav.Link>
                        </Nav.Item>
                      );
                    })}
                  </Nav>
                </Col>
                <Col sm={7}>
                  <Tab.Content>
                    {services.map((s: any) => {
                      return (
                        <Tab.Pane
                          eventKey={`${s.id}`}
                          key={`servicePane${s.id}`}
                        >
                          {serviceDetail ? (
                            <div className="ps-2">
                              <div className="fs-4 fst-italic fw-bold">
                                {s.name}
                              </div>
                              <hr />
                              <div className="fs-5">{s.description}</div>
                              <hr />
                              <div className="fs-5">
                                Kohezgjatja: {s.duration.substring(0, 8)}
                              </div>
                              <Card className="fs-5 my-3">
                                <Card.Header>Oret e punes:</Card.Header>
                                {s.workingHours.length > 0 ? (
                                  <Card.Body>
                                    <ul className="text-info ">
                                      {s.workingHours.map((x: any) => {
                                        return (
                                          <li className="fs-5">
                                            <span className="me-3">
                                              {getWeekDay(x.weekday)}
                                            </span>
                                            <span className="me-3">
                                              {x.startHour.substring(0, 5)}
                                            </span>
                                            <span>
                                              {x.endHour.substring(0, 5)}
                                            </span>
                                          </li>
                                        );
                                      })}
                                    </ul>
                                  </Card.Body>
                                ) : (
                                  <div className="ms-4">
                                    Nuk ka ore pune te konfiguruara per kete
                                    sherbim.
                                  </div>
                                )}
                              </Card>
                              <Card className="fs-5">
                                <Card.Header>Pajisjet: </Card.Header>
                                <Card.Body>
                                  {s.equipments.length > 0 ? (
                                    <ul>
                                      {s.equipments.map(
                                        (x: any, index: number) => {
                                          return (
                                            <li key={`equipment${index}`}>
                                              <span>{x.name}</span>
                                              <span>{x.producedAt}</span>
                                            </li>
                                          );
                                        }
                                      )}
                                    </ul>
                                  ) : (
                                    <div>
                                      Sherbimi mund te kryhet pa pajisje
                                      ndihmese
                                    </div>
                                  )}
                                </Card.Body>
                              </Card>
                            </div>
                          ) : (
                            <div className="spinner-border" role="status">
                              <span className="visually-hidden">
                                Loading...
                              </span>
                            </div>
                          )}
                        </Tab.Pane>
                      );
                    })}
                    <Tab.Pane eventKey="default" className="fs-5">
                      <div>
                        Klikoni ne nje prej sherbimeve per me shume detaje...
                      </div>
                    </Tab.Pane>
                  </Tab.Content>
                </Col>
              </Row>
            </Tab.Container>
          ) : (
            <Alert variant={"warning"}>
              Nuk keni asnje sherbim te disponueshem
            </Alert>
          )}
        </Card.Body>
      ) : (
        <div className="spinner-border" role="status">
          <span className="visually-hidden">Loading...</span>
        </div>
      )}
      <div>
        {openDrawer && (
          <ServiceFormDrawer
            onCancel={() => setOpenDrawer(false)}
            onSave={handleFormSubmit}
          />
        )}
      </div>
    </Card>
  );
};

export default ManagerServices;
