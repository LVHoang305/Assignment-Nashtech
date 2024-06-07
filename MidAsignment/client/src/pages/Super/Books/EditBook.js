import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { apiGetBooks } from "../../../services/Books";
import Button from "react-bootstrap/esm/Button";
import { apiUpdateBooks } from "../../../services/Books";
import { apiGetCategories, apiCreateBookCategories, apiDeleteAllBookCategories } from "../../../services/Categories";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";
import Select from "react-select";
import makeAnimated from "react-select/animated";

const animatedComponents = makeAnimated();

const MySwal = withReactContent(Swal);

const EditBook = () => {
  const [book, setBook] = useState({});
  const [error, setError] = useState(null);
  const { id } = useParams();
  let cateId = "";
  const [categories, setCategories] = useState([]);
  const [chosenCategories, setChosenCategories] = useState([]);
  const navigate = useNavigate();

  const handleChange = (option) => {
    setChosenCategories(option);
  };

  const fetchBook = async (id) => {
    setBook(undefined);
    setError(null);
    try {
      const response = await apiGetBooks(id);
      setBook(response.data);
      setBook((prev) => ({ ...prev, id: id }))      
      setChosenCategories(transformData(response.data.bookCategories?.map(item => item.category)))
    } catch (error) {
      setError("Failed to fetch Books data");
    } finally {
    }
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

  useEffect(() => {
    fetchBook(id);
  }, [id]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const result = await MySwal.fire({
      title: "Are you sure?",
      text: "You won't be able to revert this!",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#d33",
      cancelButtonColor: "#3085d6",
      confirmButtonText: "Yes, edit it!",
      cancelButtonText: "No, cancel!",
    });

    if (result.isConfirmed) {
      try {
        await apiUpdateBooks(book);
        await apiDeleteAllBookCategories(book)
        chosenCategories.map(item => {
          const bookCategoryResponse = apiCreateBookCategories({
            BookId: book.id,
            CategoryId: item.id.value
          })
        })
        console.log("Book edited:", book);
        navigate("/system/books");
      } catch (error) {
        console.error("Error editing book:", error);
      }
    }
  };

  return (
    <div className="w-1100 mx-auto mt-10">
      <h1 className="text-2xl font-bold mb-4">Edit Book</h1>
      <form onSubmit={handleSubmit} className="bg-white p-6 rounded shadow-sm">
        <div className="mb-4">
          <label className="block mb-2">Title</label>
          <input
            type="text"
            value={book?.title}
            defaultValue={book?.title}
            onChange={(e) =>
              setBook((prev) => ({ ...prev, title: e.target.value }))
            }
            minLength={5}
            maxLength={100}
            className="w-full p-2 border rounded"
            required
          />
        </div>
        <div className="mb-4">
          <label className="block mb-2">Author</label>
          <input
            type="text"
            value={book?.author}
            defaultValue={book?.author}
            onChange={(e) =>
              setBook((prev) => ({ ...prev, author: e.target.value }))
            }
            minLength={5}
            maxLength={100}
            className="w-full p-2 border rounded"
            required
          />
        </div>
        <div className="mb-4">
          <label className="block mb-2">Categories</label>
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
          Edit Post
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

export default EditBook;
