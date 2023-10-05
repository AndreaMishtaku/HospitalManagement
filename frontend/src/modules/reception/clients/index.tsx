import React from "react";
import { GenericTable } from "../../../main/components/table";
const ReceptionClients = () => {
  return (
    <div className="py-4">
      <GenericTable controller={"clients"} />
    </div>
  );
};

export default ReceptionClients;
