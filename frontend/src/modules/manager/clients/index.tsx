import React from "react";
import { GenericTable } from "../../../main/components/table";
const ManagerClients = () => {
  return (
    <div className="py-4">
      <GenericTable controller={"clients"} />
    </div>
  );
};

export default ManagerClients;
