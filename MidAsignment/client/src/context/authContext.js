import { createContext, useContext, useState, useEffect } from "react";

const AuthContext = createContext({
  isAuthenticated: false,
  setIsAuthenticated: () => {},
  isSuperUser: false,
  setIsSuperUser: () => {},
  user: { username: "", email: "", id: "", isSuperUser: false },
  setUser: () => {},
});

export const useAuthContext = () => useContext(AuthContext);

const AuthProvider = (props) => {
  const token = window.sessionStorage.getItem("token");
  const superUser = window.sessionStorage.getItem("isSuperUser");
  const [isAuthenticated, setIsAuthenticated] = useState(!!token);
  const [isSuperUser, setIsSuperUser] = useState(!!superUser);
  const [user, setUser] = useState({isSuperUser: isSuperUser});

  useEffect(() => {
    //call api
    //setUser(data)
  }, []);

  return (
    <AuthContext.Provider
      value={{ isAuthenticated, setIsAuthenticated, user, setUser, isSuperUser, setIsSuperUser }}
    >
      {props.children}
    </AuthContext.Provider>
  );
};

export default AuthProvider;
