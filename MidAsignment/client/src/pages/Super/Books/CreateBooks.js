import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Button from "react-bootstrap/esm/Button";
import { apiCreateBooks } from "../../../services/Books";
import {
  apiGetCategories,
  apiCreateBookCategories,
} from "../../../services/Categories";
import Select from "react-select";
import makeAnimated from "react-select/animated";

const animatedComponents = makeAnimated();

const CreateBook = () => {
  const [newBook, setNewBook] = useState({
    title: "",
    author: "",
  });
  let cateId = "";
  const [categories, setCategories] = useState([]);
  const [chosenCategories, setChosenCategories] = useState([]);
  const navigate = useNavigate();
  const handleChange = (option) => {
    setChosenCategories(option);
  };

  const transformData = (data) => {
    return data.map((item) => ({
      value: item.id,
      label: item.name,
    }));
  };

  const fetchCategories = async (id) => {
    const response = await apiGetCategories(id);
    setCategories(transformData(response.data));
  };

  useEffect(() => {
    fetchCategories(cateId);
  }, [cateId]);

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await apiCreateBooks(newBook);
      await chosenCategories.map((item) => {
        apiCreateBookCategories({
          BookId: response.data.result.id,
          CategoryId: item.id.value,
        });
      });
      navigate("/system/books");
    } catch (error) {
      console.error("Error creating book:", error);
    }
  };

  return (
    <div className="mt-10 w-1100">
      <h1 className="text-2xl font-bold mb-4">Create New Book</h1>
      <form
        onSubmit={handleSubmit}
        className="bg-white p-6 rounded shadow-sm w-1100"
      >
        <div className="mb-4">
          <label className="block mb-2">Title </label>
          <input
            type="text"
            value={newBook.title}
            onChange={(e) =>
              setNewBook((prev) => ({ ...prev, title: e.target.value }))
            }
            minLength={5}
            maxLength={100}
            className="w-full p-2 border rounded"
            required
          />
        </div>
        <div className="mb-4">
          <label className="block mb-2">Author </label>
          <input
            type="text"
            value={newBook.author}
            onChange={(e) =>
              setNewBook((prev) => ({ ...prev, author: e.target.value }))
            }
            minLength={5}
            maxLength={100}
            className="w-full p-2 border rounded"
            required
          />
        </div>
        <div className="mb-4">
          <label className="block mb-2">Category </label>
          <Select
            closeMenuOnSelect={false}
            components={animatedComponents}
            //defaultValue={[colourOptions[4], colourOptions[5]]}
            isMulti
            options={categories}
            onChange={handleChange}
            value={chosenCategories}
            className="w-[1000]"
          />
        </div>
        <button type="submit" className="bg-blue-500 text-white p-2 rounded">
          Create Post
        </button>
      </form>
      <Button
        className="bg-green-200 rounded-md mt-10 px-3 py-3"
        onClick={() => navigate("/system/books")}
      >
        Back to Books
      </Button>
    </div>
  );
};

export default CreateBook;
