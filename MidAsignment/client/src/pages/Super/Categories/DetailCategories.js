import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { apiGetCategories} from "../../../services/Categories";
import Button from "react-bootstrap/esm/Button";

const DetailCategories = () => {
  const [category, setCategory] = useState({});
  const [error, setError] = useState(null);
  const { id } = useParams();
  const navigate = useNavigate();

  const fetchCategory = async (id) => {
    setCategory(undefined);
    setError(null);
    try {
      const response = await apiGetCategories(id);
      setCategory(response.data);
    } catch (error) {
      setError("Failed to fetch Category data");
    }
  };

  useEffect(() => {
    fetchCategory(id);
  }, [id]);
  return (
    <div>
      {error ? (
        <div>{error}</div>
      ) : (
        <div className="gap-4 py-10 justify-start items-start">
          <h1 className="text-3xl font-bold">Title: {category?.name}</h1>
          <h1 className="text-3xl font-bold">Describe: {category?.describe}</h1>
        </div>
      )}
      <Button
        className="bg-green-200 rounded-md mt-10 px-3 py-3"
        onClick={() => navigate("/system/categories")}
      >
        Back to Categories
      </Button>
    </div>
  );
};

export default DetailCategories;
