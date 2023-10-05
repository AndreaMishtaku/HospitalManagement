import React from 'react';
import { GenericTable } from '../../../main/components/table';
const StaffSuccededReservations = () => {
  return (
    <div className="py-4">
      <GenericTable controller={'reservations/staff-succeded'} />
    </div>
  );
};

export default StaffSuccededReservations;
