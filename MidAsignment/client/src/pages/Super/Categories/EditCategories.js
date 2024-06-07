import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import Button from "react-bootstrap/esm/Button";
import { apiGetCategories, apiUpdateCategories } from "../../../services/Categories";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";

const MySwal = withReactContent(Swal);

const EditCategories = () => {
  const [category, setCategory] = useState({});
  const [error, setError] = useState(null);
  const { id } = useParams();
  const navigate = useNavigate();

  const fetchCategories = async (id) => {
    setCategory(undefined);
    setError(null);
    try {
      const response = await apiGetCategories(id);
      setCategory(response.data);
      setCategory((prev) => ({ ...prev, id: id }))      
    } catch (error) {
      setError("Failed to fetch Categories data");
    } finally {
    }
  };

  useEffect(() => {
    fetchCategories(id);
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
        apiUpdateCategories(category);
        console.log("Categgory edited:", category);
        navigate("/system/categories");
      } catch (error) {
        console.error("Error editing category:", error);
      }
    }
  };

  return (
    <div className="w-1100 mx-auto mt-10">
      <h1 className="text-2xl font-bold mb-4">Edit Category</h1>
      <form onSubmit={handleSubmit} className="bg-white p-6 rounded shadow-sm">
        <div className="mb-4">
          <label className="block mb-2">Name</label>
          <input
            type="text"
            value={category?.name}
            defaultValue={category?.name}
            onChange={(e) =>
              setCategory((prev) => ({ ...prev, name: e.target.value }))
            }
            minLength={4}
            maxLength={100}
            className="w-full p-2 border rounded"
            required
          />
        </div>
        <div className="mb-4">
          <label className="block mb-2">Describe</label>
          <input
            type="text"
            minLength={0}
            maxLength={100}
            className="w-full p-2 border rounded"
            required
          />
        </div>
        <button type="submit" className="bg-blue-500 text-white p-2 rounded">
          Edit Categories
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

export default EditCategories;
