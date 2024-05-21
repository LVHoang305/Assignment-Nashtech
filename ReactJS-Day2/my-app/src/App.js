import "./App.css";
import React, { useState } from "react";
import Select from "react-select";
import Welcome from "./components/Welcome";
import Counter from "./components/Counter";
import Checkboxs from "./components/Checkboxs";

function App() {
  const [tag, settag] = useState("");
  const options = [
    { value: "welcome", label: "Welcome" },
    { value: "counter", label: "Counter" },
    { value: "checkboxs", label: "Checkboxs" },
  ];
  const MyComponent = () => (
    <Select options={options} onChange={(e) => settag(e.value)} />
  );

  return (
    <div className="App">
      <MyComponent style={{ display: "flex" }} />
      <h1>Option selected: {tag}</h1>
      {tag === "welcome" && <Welcome />}
      {tag === "counter" && <Counter />}
      {tag === "checkboxs" && <Checkboxs />}
    </div>
  );
}

export default App;
