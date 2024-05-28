import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import Button from "react-bootstrap/esm/Button";
import { apiCreateBooks } from "../../services/Books";

const CreateBook = () => {
  const [newBook, setNewBook] = useState({
    title: "",
    author: "",
    country: "",
    language: "",
    year: 0,
    pages: 0,
    imageLink: "",
  });
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = apiCreateBooks(newBook);
      console.log("Book created:", newBook);
      navigate("/Books");
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
          <div className="mb-4">
            <label className="block mb-2">Country </label>
            <input
              type="text"
              value={newBook.country}
              onChange={(e) =>
                setNewBook((prev) => ({ ...prev, country: e.target.value }))
              }
              minLength={2}
              maxLength={100}
              className="w-full p-2 border rounded"
              required
            />
          </div>
          <div className="mb-4">
            <label className="block mb-2">Language </label>
            <input
              type="text"
              value={newBook.language}
              onChange={(e) =>
                setNewBook((prev) => ({ ...prev, language: e.target.value }))
              }
              minLength={2}
              maxLength={100}
              className="w-full p-2 border rounded"
              required
            />
          </div>
          <div className="mb-4">
            <label className="block mb-2">Pages </label>
            <input
              type="number"
              value={newBook.title}
              onChange={(e) =>
                setNewBook((prev) => ({ ...prev, pages: e.target.value }))
              }
              minLength={1}
              maxLength={4}
              className="w-full p-2 border rounded"
              required
            />
          </div>
          <div className="mb-4">
            <label className="block mb-2">Year </label>
            <input
              type="number"
              value={newBook.title}
              onChange={(e) =>
                setNewBook((prev) => ({ ...prev, year: e.target.value }))
              }
              minLength={1}
              maxLength={4}
              className="w-full p-2 border rounded"
              required
            />
          </div>
        </div>
        <div className="mb-4">
          <label className="block mb-2">Image</label>
          <textarea
            value={newBook.image}
            onChange={(e) =>
              setNewBook((prev) => ({ ...prev, imageLink: e.target.value }))
            }
            className="w-full p-2 border rounded"
            minLength={5}
            maxLength={800}
            rows="2"
            required
          ></textarea>
        </div>
        <button type="submit" className="bg-blue-500 text-white p-2 rounded">
          Create Post
        </button>
      </form>
      <Button
        className="bg-green-200 rounded-md mt-10 px-3 py-3"
        onClick={() => navigate("/Books")}
      >
        Back to Books
      </Button>
    </div>
  );
};

export default CreateBook;
