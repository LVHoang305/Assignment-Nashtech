import { createContext, useContext, useState, useEffect } from "react";

const AuthContext = createContext({
  isAuthenticated: false,
  setIsAuthenticated: () => {},
  user: { name: "", id: "" },
  setUser: () => {},
});

export const useAuthContext = () => useContext(AuthContext);

const AuthProvider = (props) => {
  const token = localStorage.getItem("token");
  const [isAuthenticated, setIsAuthenticated] = useState(!!token);
  const [user, setUser] = useState({ name: "", id: "" });

  useEffect(() => {
    //call api
    //setUser(data)
  }, []);

  return (
    <AuthContext.Provider
      value={{ isAuthenticated, setIsAuthenticated, user, setUser }}
    >
      {props.children}
    </AuthContext.Provider>
  );
};

export default AuthProvider;
