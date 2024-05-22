import React, { useState, useEffect } from "react";

const Checkboxs = () => {
  const [coding, setCoding] = useState(false);
  const [music, setMusic] = useState(false);
  const [readingBooks, setReadingBooks] = useState(false);
  const [allSelected, setAllSelected] = useState(false);

  useEffect(() => {
    const allChecked = coding && music && readingBooks;
    setAllSelected(allChecked);
  }, [coding, music, readingBooks]);

  const handleAllChange = (e) => {
    const newValue = e.target.checked;
    setAllSelected(newValue);
    setCoding(newValue);
    setMusic(newValue);
    setReadingBooks(newValue);
  };

  const handleIndividualChange = (setter) => {
    return (e) => {
      setter(e.target.checked);
      setAllSelected(false);
    };
  };

  return (
    <div>
      <h2>Choose your interest</h2>
      <div style={{ display: "flex" }}>
        <span className="text-white">
          <input
            type="checkbox"
            checked={allSelected}
            onChange={handleAllChange}
          />{" "}
          All
        </span>
      </div>
      <div style={{ display: "flex" }}>
        <span className="text-white">
          <input
            type="checkbox"
            checked={coding}
            onChange={handleIndividualChange(setCoding)}
          />{" "}
          Coding
        </span>
      </div>
      <div style={{ display: "flex" }}>
        <span className="text-white">
          <input
            type="checkbox"
            checked={music}
            onChange={handleIndividualChange(setMusic)}
          />{" "}
          Music
        </span>
      </div>
      <div style={{ display: "flex" }}>
        <span className="text-white">
          <input
            type="checkbox"
            checked={readingBooks}
            onChange={handleIndividualChange(setReadingBooks)}
          />{" "}
          Reading books
        </span>
      </div>
      <h4 style={{ display: "flex" }}>You selected:</h4>
      <h4 style={{ display: "flex" }}>"coding": {coding ? "true" : "false"}, "music": {music ? "true" : "false"}, "reading books": {readingBooks ? "true" : "false"}</h4>
    </div>
  );
};

export default Checkboxs;
