import React from "react";
import { GenericTable } from "../../../main/components/table";
import { useNavigate } from "react-router";
const StaffPPReservations = () => {
  const navigate = useNavigate();
  return (
    <div className="py-4">
      <GenericTable
        controller={"reservations/staff-pp"}
        onEdit={(id) => {
          navigate(`/reservation/${id}`);
        }}
      />
    </div>
  );
};

export default StaffPPReservations;
