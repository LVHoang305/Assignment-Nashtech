import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { apiGetBooks } from "../services/Books";
import Button from "react-bootstrap/esm/Button";

const DetailBook = () => {
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
    }
  };

  useEffect(() => {
    fetchBook(id);
  }, [id]);
  return (
    <div>
      {error ? (
        <div>{error}</div>
      ) : (
        <div className="gap-4 py-10 justify-start items-start">
          <h1 className="text-3xl font-bold">Title: {book?.title}</h1>
          <h1 className="text-3xl font-bold">Author: {book?.author}</h1>
          <h1 className="text-3xl font-bold">
            Categories:
            {book?.bookCategories?.map((item) => {
              return <span>{item?.category?.name} </span>;
            })}
          </h1>
        </div>
      )}
      <Button
        className="bg-green-200 rounded-md mt-10 px-3 py-3"
        onClick={() => navigate("/system/books")}
      >
        Back to Books
      </Button>
    </div>
  );
};

export default DetailBook;
