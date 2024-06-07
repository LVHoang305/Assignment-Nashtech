import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { apiGetBookBorrowingRequestByField } from "../../../services/BorrowingBookRequest";
import Button from "react-bootstrap/esm/Button";

const DetailBorrowingRequests = () => {
  const [request, setRequest] = useState(null);
  const { id } = useParams();
  const navigate = useNavigate();

  const fetchBooks = async (id) => {
    const response = await apiGetBookBorrowingRequestByField({ id: id });
    setRequest(response.data[0]);
  };

  function formatDate(dateString) {
    const date = new Date(dateString);
    const options = {
      year: "numeric",
      month: "long",
      day: "numeric",
    };
    return date.toLocaleDateString("en-US", options);
  }

  useEffect(() => {
    fetchBooks(id);
  }, [id]);

  return (
    <div className="relative w-[1400px] h-[680px]">
      <div className="flex justify-between border-gray-200 border-b">
        <h1 className="text-white text-3xl font-medium py-4">Request Detail</h1>
      </div>
      <div className="flex flex-col">
        <div className="flex flex-col">
          <span className="bg-gray-200 w-full">
            Date Requested: {formatDate(request?.dateRequested)}
          </span>
          <span className="bg-gray-200 w-full">
            Requested By: {request?.requestor?.email}
          </span>
          <table className="w-full bg-white table-fixed gap-3">
            <thead>
              <tr className="border-black text-center justify-center p-2">
                <th className="border text-center p-2 w-2/12">
                  <div className="flex items-center justify-center">
                    <span>Title</span>
                  </div>
                </th>
                <th className="border text-center p-2 w-2/12">
                  <div className="flex items-center justify-center">
                    <span>Author</span>
                  </div>
                </th>
                <th className="border text-center p-2 w-6/12">
                  <div className="flex items-center justify-center">
                    <span>Category</span>
                  </div>
                </th>
              </tr>
            </thead>
            <tbody>
              {!request?.requestDetails?.length > 0 ? (
                <tr>
                  <td colSpan="6" className="text-center">
                    There is no book in this request!
                  </td>
                </tr>
              ) : (
                request?.requestDetails?.map((a) => {
                  return (
                    <tr className="h-[50px]" key={a.id}>
                      <td className="border text-center p-2 truncate">
                        {a?.book?.title}
                      </td>
                      <td className="border text-center p-2 truncate">
                        {a?.book?.author}
                      </td>
                      <td className="border text-center p-2 truncate">
                        {a?.book?.bookCategories?.map((a) => {
                          return <span>{a?.category?.name} </span>;
                        })}
                      </td>
                    </tr>
                  );
                })
              )}
            </tbody>
          </table>
        </div>
      </div>
      <Button
        className="bg-green-200 rounded-md mt-10 px-3 py-3"
        onClick={() => navigate("/system/requests")}
      >
        Back to Requests
      </Button>
    </div>
  );
};

export default DetailBorrowingRequests;
