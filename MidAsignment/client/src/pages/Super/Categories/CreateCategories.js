import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Button from "react-bootstrap/esm/Button";
import { apiCreateBooks } from "../../../services/Books";
import { apiGetCategories, apiCreateBookCategories } from "../../../services/Categories";
import Select from "react-select";
import makeAnimated from "react-select/animated";

const animatedComponents = makeAnimated();

const CreateCategories = () => {
  const [newCategory, setNewCategory] = useState({
    name: "",
  });
  let cateId = "";
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await apiCreateBooks(newCategory);
      navigate("/system/categories");
    } catch (error) {
      console.error("Error creating category:", error);
    }
  };

  return (
    <div className="mt-10 w-1100">
      <h1 className="text-2xl font-bold mb-4">Create New Category</h1>
      <form
        onSubmit={handleSubmit}
        className="bg-white p-6 rounded shadow-sm w-1100"
      >
        <div className="mb-4">
          <label className="block mb-2">Name </label>
          <input
            type="text"
            value={newCategory.name}
            onChange={(e) =>
              setNewCategory((prev) => ({ ...prev, name: e.target.value }))
            }
            minLength={5}
            maxLength={100}
            className="w-full p-2 border rounded"
            required
          />
        </div>
        <div className="mb-4">
          <label className="block mb-2">Descript </label>
          <input
            type="text"
            //value={newCategory.author}
            onChange={(e) =>
              //setNewBook((prev) => ({ ...prev, author: e.target.value }))
              console.log()
            }
            minLength={0}
            maxLength={100}
            className="w-full p-2 border rounded"
            required
          />
        </div>
        <button type="submit" className="bg-blue-500 text-white p-2 rounded">
          Create Category
        </button>
      </form>
      <Button
        className="bg-green-200 rounded-md mt-10 px-3 py-3"
        onClick={() => navigate("/system/categories")}
      >
        Back to Categories
      </Button>
    </div>
  );
};

export default CreateCategories;
