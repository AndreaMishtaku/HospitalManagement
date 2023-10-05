import { FC } from "react";
import { Navigate, useLocation } from "react-router-dom";
import useGetUser from "../main/hooks/useGetUser";

const PrivateRoute: FC<any> = (props: any) => {
  const { children } = props;
  const prevLocation = useLocation();

  const user = useGetUser();

  return user ? (
    children
  ) : (
    <Navigate to="/login" state={{ prev: prevLocation.pathname }} />
  );
};

export default PrivateRoute;
