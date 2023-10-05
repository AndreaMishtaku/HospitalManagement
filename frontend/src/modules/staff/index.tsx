import React, { useEffect, useState } from "react";
import { Card, Col, Container, Row } from "react-bootstrap";
import useGetUser from "../../main/hooks/useGetUser";
import moment from "moment-timezone";
import axios from "axios";
import { Spinner } from "@themesberg/react-bootstrap";

const StaffHomePage = () => {
  const user = useGetUser();

  const [reservationsReport, setReservationsReport] = useState<any>();

  const fetchReport = async () => {
    const res = await axios.get("reports/reservations");
    if (res.data) {
      setReservationsReport(res.data);
    }
  };

  useEffect(() => {
    fetchReport();
  }, []);
  return (
    <>
      {reservationsReport ? (
        <Container>
          <Row style={{ marginBottom: "2rem" }}>
            <Col xs="12" sm="12" md="12" lg="12">
              <Card>
                <Card.Header>
                  <h4>Pershendetje !</h4>
                </Card.Header>
                <Card.Body>
                  <div className="d-flex justify-content-center">
                    <h4>
                      {user.FirstName} {user.LastName}
                    </h4>
                  </div>

                  <div className="d-flex justify-content-center">
                    <h2 style={{ color: "blue" }}>
                      {moment().format("HH:mm")}
                    </h2>
                  </div>

                  <div className="d-flex justify-content-center">
                    <h4>Roli : {user.role}</h4>
                  </div>
                </Card.Body>
              </Card>
            </Col>
          </Row>
          <Row>
            <Col xs="12" sm="12" md="12" lg="12">
              <Card style={{ height: "100%" }}>
                <Card.Header>
                  <h4>Raport mbi Rezervimet</h4>
                </Card.Header>
                <Card.Body>
                  <div className="d-flex justify-content-center">
                    <h4>
                      Rezervime ne pritje :
                      {reservationsReport.totalWaitingReservations}
                    </h4>
                  </div>

                  <div className="d-flex justify-content-center">
                    <h4 style={{ color: "red" }}>
                      Rezervime te anulluara :
                      {reservationsReport.totalCanceledReservations}
                    </h4>
                  </div>

                  <div className="d-flex justify-content-center">
                    <h4 style={{ color: "green" }}>
                      Rezervime te perfunduara :
                      {reservationsReport.totalSuccessfulReservations}
                    </h4>
                  </div>
                </Card.Body>
              </Card>
            </Col>
          </Row>
        </Container>
      ) : (
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
    </>
  );
};

export default StaffHomePage;
