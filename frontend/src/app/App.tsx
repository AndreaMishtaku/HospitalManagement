import AppNavigate from "./AppNavigate";
import PrivateRoute from "./PrivateRoute";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import MainPage from "../modules/index";
import LoginPage from "../modules/login";
import ManagerHomePage from "../modules/manager";
import LayoutWrapper from "../main/components/layout-wrapper";
import ManagerEmployees from "../modules/manager/employees";
import ManagerClients from "../modules/manager/clients";
import ManagerServices from "../modules/manager/services";
import ManagerEquipments from "../modules/manager/equipments";
import ManagerReservations from "../modules/manager/reservations";
import StaffHomePage from "../modules/staff";
import ReceptionMainPage from "../modules/reception";
import StaffSuccededReservations from "../modules/staff/succeded-reservations";
import StaffPPReservations from "../modules/staff/pending-reservations";
import Reservation from "../modules/reservation";
import PendingReservation from "../modules/common/reseration-actions";
import StaffEquipments from "../modules/staff/equipments";
import ReceptionWaitingReservations from "../modules/reception/pending-reservations";
import ReceptionStaff from "../modules/reception/staff";
import ReceptionClients from "../modules/reception/clients";
import Scanner from "../modules/reception/scanner";

const App = () => {
  return (
    <BrowserRouter>
      <AppNavigate />
      <Routes>
        <Route path="/" element={<MainPage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/reservations" element={<Reservation />} />
        <Route
          path="/manager"
          element={
            <PrivateRoute>
              <LayoutWrapper>
                <ManagerHomePage />
              </LayoutWrapper>
            </PrivateRoute>
          }
        />
        <Route
          path="/manager/employees"
          element={
            <PrivateRoute>
              <LayoutWrapper>
                <ManagerEmployees />
              </LayoutWrapper>
            </PrivateRoute>
          }
        />
        <Route
          path="/manager/clients"
          element={
            <PrivateRoute>
              <LayoutWrapper>
                <ManagerClients />
              </LayoutWrapper>
            </PrivateRoute>
          }
        />
        <Route
          path="/manager/services"
          element={
            <PrivateRoute>
              <LayoutWrapper>
                <ManagerServices />
              </LayoutWrapper>
            </PrivateRoute>
          }
        />
        <Route
          path="/manager/equipments"
          element={
            <PrivateRoute>
              <LayoutWrapper>
                <ManagerEquipments />
              </LayoutWrapper>
            </PrivateRoute>
          }
        />
        <Route
          path="/manager/reservations"
          element={
            <PrivateRoute>
              <LayoutWrapper>
                <ManagerReservations />
              </LayoutWrapper>
            </PrivateRoute>
          }
        />
        <Route
          path="/staff"
          element={
            <PrivateRoute>
              <LayoutWrapper>
                <StaffHomePage />
              </LayoutWrapper>
            </PrivateRoute>
          }
        />
        <Route
          path="/staff/succeded"
          element={
            <PrivateRoute>
              <LayoutWrapper>
                <StaffSuccededReservations />
              </LayoutWrapper>
            </PrivateRoute>
          }
        />{" "}
        <Route
          path="/staff/waiting"
          element={
            <PrivateRoute>
              <LayoutWrapper>
                <StaffPPReservations />
              </LayoutWrapper>
            </PrivateRoute>
          }
        />
        <Route
          path="/staff/equipments"
          element={
            <PrivateRoute>
              <LayoutWrapper>
                <StaffEquipments />
              </LayoutWrapper>
            </PrivateRoute>
          }
        />
        <Route
          path="/reception"
          element={
            <PrivateRoute>
              <LayoutWrapper>
                <ReceptionMainPage />
              </LayoutWrapper>
            </PrivateRoute>
          }
        />
        <Route
          path="/reception/waiting"
          element={
            <PrivateRoute>
              <LayoutWrapper>
                <ReceptionWaitingReservations />
              </LayoutWrapper>
            </PrivateRoute>
          }
        />
        <Route
          path="/reception/staff"
          element={
            <PrivateRoute>
              <LayoutWrapper>
                <ReceptionStaff />
              </LayoutWrapper>
            </PrivateRoute>
          }
        />
        <Route
          path="/reception/clients"
          element={
            <PrivateRoute>
              <LayoutWrapper>
                <ReceptionClients />
              </LayoutWrapper>
            </PrivateRoute>
          }
        />
        {
          <Route
            path="/scanner"
            element={
              <PrivateRoute>
                <LayoutWrapper>
                  <Scanner />
                </LayoutWrapper>
              </PrivateRoute>
            }
          />
        }
        <Route
          path="/reservation/:id"
          element={
            <PrivateRoute>
              <LayoutWrapper>
                <PendingReservation />
              </LayoutWrapper>
            </PrivateRoute>
          }
        />
      </Routes>
    </BrowserRouter>
  );
};

export default App;
