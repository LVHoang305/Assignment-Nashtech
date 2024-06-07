import { Navigate } from "react-router-dom";
import { useAuthContext } from "../context/authContext";

const SuperAuth = (props) => {
  const { children } = props;
  const { isSuperUser } = useAuthContext();

  return isSuperUser ? children : <Navigate to="/" />;
};

export default SuperAuth;
