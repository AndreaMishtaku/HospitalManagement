import React from "react";
import { Image } from "@themesberg/react-bootstrap";

export default (props: any) => {
  const { show } = props;

  return (
    <>
      {show && (
        <div
          className={
            "preloader bg-soft flex-column justify-content-center align-items-center "
          }
        >
          <div className="spinner-border" role="status">
            <span className="visually-hidden">Loading...</span>
          </div>
        </div>
      )}
    </>
  );
};
