import React from "react";

const Welcome = () => {
  const people = [
    { name: "Hoangđd", age: 34, color: "red" },
    { name: "Son Tung MTP", age: 25, color: "yellow" },
    { name: "Ronaldo", age: 37, color: "green" },
  ];

  return (
    <div>
      {people.map((item) => {
        return (
          <div style={{ backgroundColor: item.color }}>
            <h1 style={{ display: "flex" }}>Hello {item.name}</h1>
            <p style={{ display: "flex" }}>Age: {item.age}</p>
          </div>
        );
      })}
    </div>
  );
};

export default Welcome;
