import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Card, Col, Form, Row } from "@themesberg/react-bootstrap";
import axios from "axios";
import React, { useEffect, useState } from "react";
import { Button, Spinner } from "react-bootstrap";
import { useParams } from "react-router-dom";
import CheckInModal from "../../main/components/modals/check-in";
import PostponeModal from "../../main/components/modals/postpone";
import CancelModal from "../../main/components/modals/cancel";
import useGetUser from "../../main/hooks/useGetUser";

const PendingReservation = () => {
  const params = useParams();
  const { id } = params;

  const user = useGetUser();

  const [model, setModel] = useState<any>();
  const [checkInModalOpen, setCheckInModalOpen] = useState(false);
  const [isCheckedIn, setIsCheckedIn] = useState(false);
  const [postponeModalOpen, setPostponeModalOpen] = useState(false);
  const [isCanceled, setIsCanceled] = useState(false);
  const [cancelModalOpen, setCancelModalOpen] = useState(false);

  const fetchReservation = async () => {
    const response = await axios.get(`reservations/${id}`);

    if (response.data) {
      setModel(response.data);
    }
  };

  useEffect(() => {
    fetchReservation();
  }, []);
  return model ? (
    <div className="mt-3">
      <div style={{ display: "flex", gap: 5, marginBottom: "1rem" }}>
        {user?.role == "Staff" && (
          <>
            {!isCheckedIn ? (
              <>
                <Button
                  variant="success"
                  onClick={() => {
                    setCheckInModalOpen(true);
                  }}
                >
                  <FontAwesomeIcon
                    icon={"fa-solid fa-list-check" as any}
                    className="me-2"
                  />
                  Kryej check-in
                </Button>
                <Button
                  variant="warning"
                  onClick={() => {
                    setPostponeModalOpen(true);
                  }}
                >
                  <FontAwesomeIcon
                    icon={"fa-solid fa-clock-rotate-left" as any}
                    className="me-2"
                  />
                  Shtyj
                </Button>
              </>
            ) : (
              <div>Check-in eshte kryer</div>
            )}
          </>
        )}
        {user?.role == "Reception" && (
          <>
            {!isCanceled ? (
              <>
                <Button
                  variant="danger"
                  onClick={() => {
                    setCancelModalOpen(true);
                  }}
                >
                  <FontAwesomeIcon
                    icon={"fa-solid fa-xmark" as any}
                    className="me-2"
                  />
                  Anullo
                </Button>
              </>
            ) : (
              <div>Rezervimi eshte anulluar</div>
            )}
          </>
        )}
      </div>
      {user?.role == "Staff" && (
        <>
          <CheckInModal
            open={checkInModalOpen}
            onClose={() => setCheckInModalOpen(false)}
            startTime={model.startTime}
            reservationId={model.id}
            checkIn={setIsCheckedIn}
          />
          <PostponeModal
            open={postponeModalOpen}
            onClose={() => setPostponeModalOpen(false)}
            reservation={model}
            refresh={() => {
              fetchReservation();
            }}
          />
        </>
      )}
      {user?.role == "Reception" && (
        <CancelModal
          open={cancelModalOpen}
          onClose={() => setCancelModalOpen(false)}
          reservationId={model.id}
          canceled={setIsCanceled}
        />
      )}

      <Card border="light" className="bg-white shadow-sm mb-4">
        <Card.Body>
          <h5 className="mb-4">Informacion mbi klientin</h5>
          <Form>
            <Row>
              <Col md={6} className="mb-3">
                <Form.Group id="firstName">
                  <Form.Label>Emri</Form.Label>
                  <Form.Control
                    required
                    type="text"
                    value={model.firstName}
                    readOnly
                  />
                </Form.Group>
              </Col>
              <Col md={6} className="mb-3">
                <Form.Group id="lastName">
                  <Form.Label>Mbiemri</Form.Label>
                  <Form.Control
                    required
                    type="text"
                    value={model.lastName}
                    readOnly
                  />
                </Form.Group>
              </Col>
            </Row>
            <Row className="align-items-center">
              <Col md={6} className="mb-3">
                <Form.Group id="birthday">
                  <Form.Label>Datelindja</Form.Label>
                  <div>{model.birthday.split("T")[0]}</div>
                </Form.Group>
              </Col>
              <Col md={6} className="mb-3">
                <Form.Group id="gender">
                  <Form.Label>Gjinia</Form.Label>
                  <Form.Control
                    required
                    type="text"
                    value={model.gender}
                    readOnly
                  />
                </Form.Group>
              </Col>
            </Row>
            <Row>
              <Col md={6} className="mb-3">
                <Form.Group id="email">
                  <Form.Label>Email</Form.Label>
                  <Form.Control
                    required
                    type="email"
                    value={model.email}
                    readOnly
                  />
                </Form.Group>
              </Col>
              <Col md={6} className="mb-3">
                <Form.Group id="phone">
                  <Form.Label>Numri i identifikimit</Form.Label>
                  <Form.Control
                    required
                    type="text"
                    value={model.identityNumber}
                    readOnly
                  />
                </Form.Group>
              </Col>
            </Row>

            <h5 className="my-4">Detajet e rezervimit</h5>
            <Row>
              <Col sm={12} className="mb-3">
                <Form.Group id="address">
                  <Form.Label>Arsyeja</Form.Label>
                  <Form.Control
                    type="text"
                    multiple={true}
                    value={model.reason}
                    readOnly
                  />
                </Form.Group>
              </Col>
            </Row>
            <Row>
              <Col sm={6} className="mb-3">
                <Form.Group id="city">
                  <Form.Label>Data</Form.Label>
                  <Form.Control
                    required
                    type="text"
                    value={model.date.split("T")[0]}
                    readOnly
                  />
                </Form.Group>
              </Col>
              <Col sm={6}>
                <Form.Group id="zip">
                  <Form.Label>Koha e fillimit</Form.Label>
                  <Form.Control value={model.startTime} readOnly />
                </Form.Group>
              </Col>
            </Row>
          </Form>
        </Card.Body>
      </Card>
    </div>
  ) : (
    <div
      style={{
        display: "flex",
        justifyContent: "center",
        height: "100%",
      }}
    >
      <Spinner animation="border" role="status" style={{ marginBlock: "auto" }}>
        <span className="visually-hidden">Loading...</span>
      </Spinner>
    </div>
  );
};

export default PendingReservation;
