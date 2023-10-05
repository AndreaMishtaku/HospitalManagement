import { Col } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import "./style.scss";
import { ReactNode } from "react";
import { motion } from "framer-motion";
import { useLocation } from "react-router-dom";
import React from "react";

export interface IModalProps {
  title?: ReactNode;
  show: boolean;
  onClose: () => void;
  children: React.ReactNode;
  actions?: React.ReactNode;
}
const Drawer = (props: IModalProps) => {
  const { title, show, children, actions, onClose } = props;
  const location = useLocation();
  return (
    <>
      {show && (
        <motion.aside
          key={location.pathname}
          initial={{ x: "100%", opacity: 0 }}
          animate={{ x: 0, opacity: 1 }}
          exit={{
            opacity: 0,
            x: "100%",
          }}
          transition={{ type: "spring", bounce: 0, duration: 1 }}
          className={`drawer shadow-lg p-2 d-flex flex-column justify-content-between`}
        >
          <Col className="dbody">
            <div className="p-3 dtitle border-bottom">
              {title}
              <FontAwesomeIcon
                className="close-icon"
                onClick={onClose}
                icon={"fa-solid fa-x" as any}
              />
            </div>
            <div className="p-3">{children}</div>
          </Col>
          <div className="d-flex actions flex-row justify-content-end ">
            {actions}
          </div>
        </motion.aside>
      )}
    </>
  );
};

export default Drawer;
