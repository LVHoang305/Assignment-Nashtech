import React, { useState, useEffect } from "react";
import { apiGetBookBorrowingRequest, apiUpdateBookBorrowingRequest } from "../../../services/BorrowingBookRequest";
import Button from "react-bootstrap/Button";
import { NavLink } from "react-router-dom";
import { Pagination } from "../../../components";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";

const MySwal = withReactContent(Swal);

const BorrowingRequests = () => {
  const [requests, setRequest] = useState([]);

  const requestId = "";
  const [error, setError] = useState(null);
  const [currentPage, setCurrentPage] = useState(1);
  const requestsPerPage = 13;

  const fetchRequest = async (id) => {
    setRequest(undefined);
    setError(null);
    try {
      const response = await apiGetBookBorrowingRequest(id);
      setRequest(response.data);
    } catch (error) {
      setError("Failed to fetch request data");
    }
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

  const handleClicked = async (item, state) => {
    const result = await MySwal.fire({
      title: "Are you sure?",
      icon: "question",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#3085d6",
      confirmButtonText: "Yes, do it!",
      cancelButtonText: "No, cancel!",
    });

    if (result.isConfirmed) {
      try {
        await apiUpdateBookBorrowingRequest({...item, status: state});
        fetchRequest(requestId);
        MySwal.fire("Updated!", "Request has been updated.", "success");
      } catch (error) {
        console.error("Error update request:", error);
        MySwal.fire("Error!", "There was an error update the request.", "error");
      }
    }
  };

  useEffect(() => {
    fetchRequest(requestId);
  }, [requestId]);

  const indexOfLastRequest = currentPage * requestsPerPage;
  const indexOfFirstrequest = indexOfLastRequest - requestsPerPage;
  const currentRequests = requests?.slice(indexOfFirstrequest, indexOfLastRequest);

  const paginate = (pageNumber) => setCurrentPage(pageNumber);

  return (
    <div className="relative w-[1300px] h-[680px]">
      <div className="flex justify-between border-gray-200 border-b">
        <h1 className="text-white text-3xl font-medium py-4">Requests</h1>
      </div>
      {error ? (
        <div>{error}</div>
      ) : (
        <table className="w-full bg-white table-fixed">
          <thead>
            <tr className="border-black text-center justify-center p-2">
              <th
                className="border text-center p-2 cursor-pointer w-1/12"
              >
                <div className="flex items-center justify-center">
                  <span>ID</span>
                </div>
              </th>
              <th
                className="border text-center p-2 cursor-pointer w-2/12"
              >
                <div className="flex items-center justify-center">
                  <span>Requestor</span>
                </div>
              </th>
              <th
                className="border text-center p-2 cursor-pointer w-3/12"
              >
                <div className="flex items-center justify-center">
                  <span>Date Requested</span>
                </div>
              </th>
              <th
                className="border text-center p-2 cursor-pointer w-3/12"
              >
                <div className="flex items-center justify-center">
                  <span>Status</span>
                </div>
              </th>
              <th className="border text-center p-2 w-3/12">Actions</th>
            </tr>
          </thead>
          <tbody>
            {!requests ? (
              <tr>
                <td colSpan="6" className="text-center">
                  There is no Request!
                </td>
              </tr>
            ) : (
              currentRequests?.map((item) => {
                return (
                  <tr className="h-[50px]" key={item.id}>
                    <td className="border text-center p-2">{item?.id}</td>
                    <td className="border text-center p-2 truncate">
                      {item?.requestor?.email}
                    </td>
                    <td className="border text-center p-2 truncate">
                      {formatDate(item?.dateRequested)}
                    </td>
                    <td className="border text-center p-2 truncate">
                      {item?.status}
                    </td>
                    <td className="border text-center p-3 flex items-center justify-around">
                      <NavLink
                        to={`/system/requests/${item.id}`}
                        className="text-white bg-blue-300 px-5 py-2 rounded-md hover:underline"
                      >
                        Detail
                      </NavLink>
                      <Button
                        className="text-white rounded-md bg-green-600 px-5 py-2 hover:underline"
                        onClick={() => {
                          handleClicked(item, "Approved")                     
                        }}
                      >
                        Approve
                      </Button>
                      <Button
                        className="text-white rounded-md bg-red-600 px-5 py-2 hover:underline"
                        onClick={() => {
                          handleClicked(item, "Rejected")
                        }}
                      >
                        Reject
                      </Button>
                    </td>
                  </tr>
                );
              })
            )}
          </tbody>
        </table>
      )}
      <Pagination
        postsPerPage={requestsPerPage}
        totalPosts={requests?.length}
        paginate={paginate}
        currentPage={currentPage}
      />
    </div>
  );
};

export default BorrowingRequests;
