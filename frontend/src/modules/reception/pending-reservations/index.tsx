import React from "react";
import { useNavigate } from "react-router-dom";
import { GenericTable } from "../../../main/components/table";
const ReceptionWaitingReservations = () => {
  const navigate = useNavigate();
  return (
    <div className="py-4">
      <GenericTable
        controller={"reservations/reception"}
        onEdit={(id) => {
          navigate(`/reservation/${id}`);
        }}
      />
    </div>
  );
};

export default ReceptionWaitingReservations;
