import React from "react";
import { GenericTable } from "../../../main/components/table";
const ManagerReservations = () => {
  return (
    <div className="py-4">
      <GenericTable controller={"reservations/reception"} />
    </div>
  );
};

export default ManagerReservations;
