import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEnvelope, faUnlockAlt } from "@fortawesome/free-solid-svg-icons";

import {
  Col,
  Row,
  Form,
  Card,
  Button,
  FormCheck,
  Container,
  InputGroup,
} from "@themesberg/react-bootstrap";
import { Link } from "react-router-dom";
import { useForm, Controller } from "react-hook-form";
import onLogin from "../main/store/stores/user/login.store.on-login";
import { useDispatch } from "react-redux";
import Header from "../main/components/main-page/header";

const LoginPage = () => {
  const { control, handleSubmit } = useForm();
  const dispatch = useDispatch();

  const onSubmitHandler = (data: any) => {
    dispatch(onLogin(data));
  };

  return (
    <>
      <Header />
      <section className="d-flex align-items-center my-5 mt-lg-6 mb-lg-5">
        <Container>
          <Row className="justify-content-center form-bg-image mt-5">
            <Col
              xs={12}
              className="d-flex align-items-center justify-content-center"
            >
              <div className="bg-white shadow-soft border rounded border-light p-4 p-lg-5 w-100 fmxw-500 py-5">
                <div className="text-center text-md-center mb-4 mt-md-0">
                  <h3 className="mb-0">Identifikohu</h3>
                </div>
                <Form className="mt-4" onSubmit={handleSubmit(onSubmitHandler)}>
                  <Form.Group id="email" className="mb-4">
                    <Form.Label className="fs-5">Email</Form.Label>
                    <Controller
                      control={control}
                      name={"email"}
                      defaultValue=""
                      render={({
                        field: { onChange, value },
                        fieldState: { error },
                      }) => (
                        <>
                          <InputGroup>
                            <InputGroup.Text>
                              <FontAwesomeIcon icon={faEnvelope} />
                            </InputGroup.Text>
                            <Form.Control
                              value={value}
                              onChange={onChange}
                              autoFocus
                              required
                              type="email"
                              placeholder="example@gmail.com"
                            />
                          </InputGroup>
                        </>
                      )}
                    />
                  </Form.Group>
                  <Form.Group>
                    <Form.Group id="password" className="mb-4">
                      <Form.Label className="fs-5">Password</Form.Label>
                      <Controller
                        control={control}
                        name={"password"}
                        defaultValue=""
                        render={({
                          field: { onChange, value },
                          fieldState: { error },
                        }) => (
                          <>
                            <InputGroup>
                              <InputGroup.Text>
                                <FontAwesomeIcon icon={faUnlockAlt} />
                              </InputGroup.Text>
                              <Form.Control
                                value={value}
                                onChange={onChange}
                                required
                                type="password"
                                placeholder="Password"
                              />
                            </InputGroup>
                          </>
                        )}
                      />
                    </Form.Group>
                    <div className="text-center mb-4">
                      Plotesoni me te dhenat e kerkuara per te patur akses si
                      staff
                    </div>
                  </Form.Group>
                  <Button variant="primary" type="submit" className="w-100">
                    Identifikohu
                  </Button>{" "}
                </Form>{" "}
                <div className="text-center my-4 fs-5">
                  <Link to="/">Kthehu ne faqen kryesore</Link>
                </div>
              </div>
            </Col>
          </Row>
        </Container>
      </section>
    </>
  );
};

export default LoginPage;
