import React, { useEffect, useState } from 'react';
import SimpleBar from 'simplebar-react';
import { useLocation, useNavigate } from 'react-router-dom';
import { CSSTransition } from 'react-transition-group';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import axios from 'axios';
import { faTimes, faHospital } from '@fortawesome/free-solid-svg-icons';
import { Nav, Badge, Image, Button, Dropdown, Accordion, Navbar } from '@themesberg/react-bootstrap';
import { Link } from 'react-router-dom';
import AuthManager from '../../utils/authManager';
import { navigateTo } from '../../store/stores/navigation/navigation.store';

const Menu = () => {
  const location = useLocation();
  const { pathname } = location;
  const [show, setShow] = useState(false);
  const navigate = useNavigate();
  const showClass = show ? 'show' : '';

  const [menuItems, setMenuItems] = useState<Array<any>>();

  const fetchMenuItems = async () => {
    const result = await axios.get('/menu');
    setMenuItems(result.data);
  };

  useEffect(() => {
    fetchMenuItems();
  }, []);
  const onCollapse = () => setShow(!show);

  const CollapsableNavItem = (props: any) => {
    const { eventKey, title, icon, children = null } = props;
    const defaultKey = pathname.indexOf(eventKey) !== -1 ? eventKey : '';

    return (
      <Accordion as={Nav.Item} defaultActiveKey={defaultKey}>
        <Accordion.Item eventKey={eventKey}>
          <Accordion.Button as={Nav.Link} className="d-flex justify-content-between align-items-center">
            <span>
              <span className="sidebar-icon">
                <FontAwesomeIcon icon={`${icon}` as any} />
              </span>
              <span className="sidebar-text fs-5">{title}</span>
            </span>
          </Accordion.Button>
          <Accordion.Body className="multi-level">
            <Nav className="flex-column">{children}</Nav>
          </Accordion.Body>
        </Accordion.Item>
      </Accordion>
    );
  };

  const NavItem = (props: any) => {
    const { title, link, target, icon, image, badgeText, isChild = false, badgeBg = 'secondary', badgeColor = 'primary' } = props;
    const classNames = badgeText ? 'd-flex justify-content-start align-items-center justify-content-between' : '';
    const navItemClassName = link === pathname ? 'active' : '';

    return (
      <Nav.Item className={navItemClassName} onClick={() => setShow(false)}>
        <Nav.Link as={Link} to={link} target={target} className={classNames}>
          <span>
            {icon ? (
              <span className="sidebar-icon">
                <FontAwesomeIcon icon={`${icon}` as any} />{' '}
              </span>
            ) : null}
            {image ? <Image src={image} width={20} height={20} className="sidebar-icon svg-icon" /> : null}

            <span className={`sidebar-text ${!isChild ? 'fs-5' : ''}`}>{title}</span>
          </span>
          {badgeText ? (
            <Badge pill bg={badgeBg} text={badgeColor} className="badge-md notification-count ms-2">
              {badgeText}
            </Badge>
          ) : null}
        </Nav.Link>
      </Nav.Item>
    );
  };

  return (
    <>
      <Navbar expand={false} collapseOnSelect variant="dark" className="navbar-theme-primary px-4 d-md-none">
        <Navbar.Toggle as={Button} aria-controls="main-navbar" onClick={onCollapse}>
          <span className="navbar-toggler-icon" />
        </Navbar.Toggle>
        <div className="me-3">
          <button
            className="btn btn-danger"
            onClick={() => {
              AuthManager.logout();
              navigate('/');
            }}
          >
            <FontAwesomeIcon icon={'fa-solid fa-right-to-bracket' as any} className="me-2" />
            Log out
          </button>
        </div>
      </Navbar>

      <div className={`header-top contentw-100 py-3 d-flex justify-content-between sidebar-text`}>
        <div className="fs-3  text-align-center fst-italic fw-bold" style={{ color: '#052b6b' }}>
          Hospital Management
        </div>
        <div className="me-3 ">
          <button
            className="btn btn-danger"
            onClick={() => {
              AuthManager.logout();
              navigate('/');
            }}
          >
            <FontAwesomeIcon icon={'fa-solid fa-right-to-bracket' as any} className="me-2" />
            Log out
          </button>
        </div>
      </div>
      <CSSTransition timeout={300} in={show} classNames="sidebar-transition">
        <SimpleBar className={`collapse ${showClass} sidebar d-md-block bg-primary text-white`}>
          <div className="d-flex  align-items-center justify-content-end me-3 mt-2 ">
            <Button className="collapse-close d-md-none " onClick={onCollapse}>
              <FontAwesomeIcon icon={faTimes} />
            </Button>
          </div>

          <div className="sidebar-inner px-4 pt-3">
            <div className="mx-auto text-align-center fs-4 mt-1 mb-3 fst-italic">
              <FontAwesomeIcon icon={faHospital} className="me-3" color="red" />H M
            </div>
            <Nav className="flex-column pt-3">
              {menuItems &&
                menuItems.map((m: any) => {
                  if (m.children.length == 0) {
                    return <NavItem key={`navM${m.id}`} title={m.title} link={m.path} icon={m.icon} />;
                  } else {
                    return (
                      <CollapsableNavItem eventKey={`${m.path}/`} title={m.title} icon={m.icon}>
                        {m.children.map((c: any) => {
                          return <NavItem key={`navChild${c.Id}`} title={c.title} link={c.path} isChild={true} />;
                        })}
                      </CollapsableNavItem>
                    );
                  }
                })}
            </Nav>
          </div>
        </SimpleBar>
      </CSSTransition>
    </>
  );
};

export default Menu;
