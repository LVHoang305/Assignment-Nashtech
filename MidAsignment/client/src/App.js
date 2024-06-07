import "./styles/App.css";
import React from "react";
import { Public } from "./layouts";
import AuthProvider from "./context/authContext";
import RequestProvider from "./context/requestContext";

function App() {
  return (
    <AuthProvider>
      <RequestProvider>
          <div className="App h-full w-full">
            <Public />
          </div>
      </RequestProvider>
    </AuthProvider>
  );
}

export default App;
