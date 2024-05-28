import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { apiGetBooks } from "../../services/Books";
import Button from "react-bootstrap/esm/Button";
import { apiUpdateBooks } from "../../services/Books";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";

const MySwal = withReactContent(Swal);

const EditPost = () => {
  const [book, setBook] = useState({});
  const [error, setError] = useState(null);
  const { id } = useParams();
  const navigate = useNavigate();

  const fetchBook = async (id) => {
    setBook(undefined);
    setError(null);
    try {
      const response = await apiGetBooks(id);
      setBook(response.data);
    } catch (error) {
      setError("Failed to fetch Books data");
    } finally {
    }
  };

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
        const response = apiUpdateBooks(book);
        console.log("Book edited:", book);
        navigate("/books");
      } catch (error) {
        console.error("Error editing book:", error);
      }
    }
  };

  return (
    <div className="w-1100 mx-auto mt-10">
      <h1 className="text-2xl font-bold mb-4">Edit Post</h1>
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
          <label className="block mb-2">Country</label>
          <input
            type="text"
            value={book?.country}
            defaultValue={book?.country}
            onChange={(e) =>
              setBook((prev) => ({ ...prev, country: e.target.value }))
            }
            minLength={5}
            maxLength={100}
            className="w-full p-2 border rounded"
            required
          />
        </div>
        <div className="mb-4">
          <label className="block mb-2">Language</label>
          <input
            type="text"
            value={book?.language}
            defaultValue={book?.language}
            onChange={(e) =>
              setBook((prev) => ({ ...prev, language: e.target.value }))
            }
            minLength={5}
            maxLength={100}
            className="w-full p-2 border rounded"
            required
          />
        </div>
        <div className="mb-4">
          <label className="block mb-2">Year</label>
          <input
            type="number"
            value={book?.year}
            defaultValue={book?.year}
            onChange={(e) =>
              setBook((prev) => ({ ...prev, year: e.target.value }))
            }
            minLength={5}
            maxLength={100}
            className="w-full p-2 border rounded"
            required
          />
        </div>
        <div className="mb-4">
          <label className="block mb-2">Pages</label>
          <input
            type="number"
            value={book?.pages}
            defaultValue={book?.pages}
            onChange={(e) =>
              setBook((prev) => ({ ...prev, pages: e.target.value }))
            }
            minLength={5}
            maxLength={100}
            className="w-full p-2 border rounded"
            required
          />
        </div>
        <div className="mb-4">
          <label className="block mb-2">Image</label>
          <textarea
            value={book?.imageLink}
            defaultValue={book?.imageLink}
            onChange={(e) =>
              setBook((prev) => ({ ...prev, imageLink: e.target.value }))
            }
            className="w-full p-2 border rounded"
            rows="1"
            minLength={5}
            maxLength={800}
            required
          ></textarea>
        </div>
        <button type="submit" className="bg-blue-500 text-white p-2 rounded">
          Edit Post
        </button>
      </form>
      <Button
        className="bg-green-200 rounded-md mt-10 px-3 py-3"
        onClick={() => navigate("/books")}
      >
        Back to Books
      </Button>
    </div>
  );
};

export default EditPost;
