import React, { useState } from "react";
import Button from "react-bootstrap/Button";

const Counter = () => {
  const [num, setNum] = useState(0);
  return (
    <div>
      <div style={{ display: "flex", gap: 30 }}>
        <Button variant="light" onClick={() => setNum(num - 1)}>
          -
        </Button>
        <p>{num}</p>
        <Button variant="light" onClick={() => setNum(num + 1)}>
          +
        </Button>
      </div>
    </div>
  );
};

export default Counter;
