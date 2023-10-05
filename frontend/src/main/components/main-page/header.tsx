import { Container, Nav, Navbar } from '@themesberg/react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React from 'react';
import { Link } from 'react-router-dom';

const Header = () => {
  return (
    <Navbar bg="light" variant="light" expand="lg" sticky="top">
      <Container>
        <Navbar.Brand as={Link} to="/">
          FTI Clinic
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="mx-auto fs-5 fw-bold">
            <Nav.Link as={Link} to="/reservations">
              Rezervo
            </Nav.Link>
          </Nav>
          <Navbar.Text className="fs-5 fw-bold">
            <Nav.Link as={Link} to="/login">
              Staff <FontAwesomeIcon icon={'fa-solid fa-right-to-bracket' as any} className="ms-2" />
            </Nav.Link>
          </Navbar.Text>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
};

export default Header;
