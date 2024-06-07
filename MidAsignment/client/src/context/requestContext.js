import { createContext, useContext, useState, useEffect } from "react";

const RequestContext = createContext({
  booksRequest: [],
  setBooksRequest: () => {},
  request: 0,
  setRequest: () => {},
});

export const useRequestContext = () => useContext(RequestContext);

const RequestProvider = (props) => {
    const [booksRequest,setBooksRequest] = useState([]);
    const [request,setRequest] = useState(0)

  useEffect(() => {
    //call api
    //setUser(data)
  }, []);

  return (
    <RequestContext.Provider
      value={{ booksRequest, setBooksRequest, request, setRequest }}
    >
      {props.children}
    </RequestContext.Provider>
  );
};

export default RequestProvider;
