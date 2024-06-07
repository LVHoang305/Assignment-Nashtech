import React, { useState, useEffect } from "react";
import { apiGetBooks } from "../../../services/Books";
import {
  apiCreateBookBorrowingRequest,
  apiCreateBookBorrowingRequestDetails,
} from "../../../services/BorrowingBookRequest";
import Button from "react-bootstrap/Button";
import { FaSort } from "react-icons/fa";
import { NavLink, useNavigate } from "react-router-dom";
import { Pagination } from "../../../components";
import { useRequestContext } from "../../../context/requestContext";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";
import { useAuthContext } from "../../../context/authContext";

const MySwal = withReactContent(Swal);

const Books = () => {
  const [books, setBooks] = useState([]);
  const id = window.sessionStorage.getItem("userId");
  const navigate = useNavigate();
  const { user, isAuthenticated } = useAuthContext();
  const { booksRequest, setBooksRequest, request, setRequest } = useRequestContext();
  const [borrowable, setBorrowable] = useState(false);

  const bookId = "";
  const [searchTerm, setSearchTerm] = useState("");
  const [sortConfig, setSortConfig] = useState({
    key: "id",
    direction: "ascending",
  });
  const [error, setError] = useState(null);
  const [currentPage, setCurrentPage] = useState(1);
  const booksPerPage = 8;

  const fetchBooks = async (bookId) => {
    setBooks(undefined);
    setError(null);
    try {
      const response = await apiGetBooks(bookId);
      setBooks(response.data);
    } catch (error) {
      setError("Failed to fetch books data");
    }
  };

  const handleSearch = (e) => {
    setSearchTerm(e.target.value);
  };

  const handleSort = (key) => {
    let direction = "ascending";
    if (sortConfig.key === key && sortConfig.direction === "ascending") {
      direction = "descending";
    }
    setSortConfig({ key, direction });
  };

  const sortedBooks = books?.sort((a, b) => {
    if (a[sortConfig.key] < b[sortConfig.key]) {
      return sortConfig.direction === "ascending" ? -1 : 1;
    }
    if (a[sortConfig.key] > b[sortConfig.key]) {
      return sortConfig.direction === "ascending" ? 1 : -1;
    }
    return 0;
  });

  const filteredBooks = sortedBooks?.filter((book) =>
    book?.title?.toLowerCase().includes(searchTerm.toLowerCase())
  );

  const handleDelete = async (id) => {
    const result = await MySwal.fire({
      title: "Are you sure?",
      text: "You won't be able to revert this!",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#d33",
      cancelButtonColor: "#3085d6",
      confirmButtonText: "Yes, remove it!",
      cancelButtonText: "No, cancel!",
    });

    if (result.isConfirmed) {
      try {
        setBooksRequest(booksRequest.filter((b) => b.id !== id));
        MySwal.fire(
          "Removed!",
          "Your book has been removed from your request.",
          "success"
        );
      } catch (error) {
        console.error("Error deleting book:", error);
        MySwal.fire(
          "Error!",
          "There was an error removed the book from your request.",
          "error"
        );
      }
    }
  };

  const handleRequest = async (id) => {
    if (!isAuthenticated) {
      navigate("/login");
    } else {
      const result = await MySwal.fire({
        title: "Are you sure?",
        text: "Do you really want to make this borrowing request?",
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#3085d6",
        confirmButtonText: "Yes, borrow them!",
        cancelButtonText: "No, cancel!",
      });

      if (result.isConfirmed) {
        try {
          const response = await apiCreateBookBorrowingRequest({
            dateRequested: new Date().toISOString(),
            requestorId: id,
            dateReturned: new Date().toISOString(),
            status: "Waiting",
          });
          if (0 === 0) {
            setRequest(prev => prev+1)
            booksRequest.map((item) => {
              const requestResponse = apiCreateBookBorrowingRequestDetails({
                BookBorrowingRequestId: response.data.result.id,
                BookId: item.id,
              });
            });
          }
          setBooksRequest([]);
          MySwal.fire(
            "Borrowed!",
            "Your request has been sent to NashTech Library.",
            "success"
          );
        } catch (error) {
          console.error("Error deleting book:", error);
          MySwal.fire(
            "Error!",
            `There was an error removed the book from your request.${error}`,
            "error"
          );
        }
      }
    }
  };

  useEffect(() => {
    fetchBooks(bookId);
  }, [bookId]);

  useEffect(() => {
    if (booksRequest.length > 4) {
      setBorrowable(true);
    } else {
      setBorrowable(false);
    }
  }, [booksRequest]);

  const indexOfLastBook = currentPage * booksPerPage;
  const indexOfFirstBook = indexOfLastBook - booksPerPage;
  const currentBooks = filteredBooks?.slice(indexOfFirstBook, indexOfLastBook);

  const paginate = (pageNumber) => setCurrentPage(pageNumber);

  return (
    <div className="relative w-[1400px] h-[680px]">
      <div className="flex justify-between border-gray-200 border-b">
        <h1 className="text-white text-3xl font-medium py-4">Books</h1>
        <Button
          className="text-green-200 text-2xl font-medium py-5 hover:underline"
          onClick={() => handleRequest(id)}
          disabled={request > 2}
        >
          New Book Borrow Request
        </Button>
      </div>
      <input
        className="font-medium bg-blue-200 border-black w-full px-4 py-2"
        type="text"
        placeholder="Search by title"
        value={searchTerm}
        onChange={handleSearch}
      />
      <div className="px-4 py-2"></div>
      <div className="flex justify-around">
        <div className="w-7/12">
          {error ? (
            <div>{error}</div>
          ) : (
            <table className="w-full bg-white table-fixed gap-3">
              <thead>
                <tr className="border-black text-center justify-center p-2">
                  <th
                    onClick={() => handleSort("title")}
                    className="border text-center p-2 cursor-pointer w-2/12"
                  >
                    <div className="flex items-center justify-center">
                      <span>Title</span>
                      <FaSort className="ml-2" />
                    </div>
                  </th>
                  <th
                    onClick={() => handleSort("author")}
                    className="border text-center p-2 cursor-pointer w-2/12"
                  >
                    <div className="flex items-center justify-center">
                      <span>Author</span>
                      <FaSort className="ml-2" />
                    </div>
                  </th>
                  <th
                    onClick={() => handleSort("category")}
                    className="border text-center p-2 cursor-pointer w-6/12"
                  >
                    <div className="flex items-center justify-center">
                      <span>Category</span>
                      <FaSort className="ml-2" />
                    </div>
                  </th>
                  <th className="border text-center p-2 w-2/12">Actions</th>
                </tr>
              </thead>
              <tbody>
                {!filteredBooks ? (
                  <tr>
                    <td colSpan="6" className="text-center">
                      There is no book!
                    </td>
                  </tr>
                ) : (
                  currentBooks?.map((item) => {
                    return (
                      <tr className="h-[50px]" key={item.id}>
                        <td className="border text-center p-2 truncate">
                          {item?.title}
                        </td>
                        <td className="border text-center p-2 truncate">
                          {item?.author}
                        </td>
                        <td className="border text-center p-2 truncate">
                          {item?.bookCategories?.map((a) => {
                            return <span>{a?.category?.name} </span>;
                          })}
                        </td>
                        <td className="border text-center p-3 flex items-center justify-around">
                          <NavLink
                            to={`/books/${item.id}`}
                            className="text-blue-500 hover:underline"
                          >
                            Detail
                          </NavLink>
                          <span className="text-white"> - </span>
                          <Button
                            disabled={borrowable}
                            className="text-blue-500 hover:underline"
                            onClick={() => {
                              setBooksRequest((prev) => [...prev, item]);
                              setBooksRequest((prev) => [...new Set(prev)]);
                            }}
                          >
                            Borrow
                          </Button>
                        </td>
                      </tr>
                    );
                  })
                )}
              </tbody>
            </table>
          )}
        </div>
        <div className="w-4/12">
          <table className="w-full bg-white table-fixed">
            <thead>
              <tr className="border-black text-center justify-center p-2">
                <th
                  onClick={() => handleSort("title")}
                  className="border text-center p-2 cursor-pointer w-4/12"
                >
                  <div className="flex items-center justify-center">
                    <span>Title</span>
                    <FaSort className="ml-2" />
                  </div>
                </th>
                <th
                  onClick={() => handleSort("author")}
                  className="border text-center p-2 cursor-pointer w-4/12"
                >
                  <div className="flex items-center justify-center">
                    <span>Author</span>
                    <FaSort className="ml-2" />
                  </div>
                </th>
                <th className="border text-center p-2 w-4/12">Actions</th>
              </tr>
            </thead>
            <tbody>
              {!booksRequest ? (
                <tr>
                  <td colSpan="6" className="text-center">
                    You haven't borrow any book yet!
                  </td>
                </tr>
              ) : (
                booksRequest?.map((item) => {
                  return (
                    <tr className="h-[50px]" key={item.id}>
                      <td className="border text-center p-2 truncate">
                        {item?.title}
                      </td>
                      <td className="border text-center p-2 truncate">
                        {item?.author}
                      </td>
                      <td className="border text-center p-3 flex items-center justify-around">
                        <Button
                          className="text-blue-500 hover:underline"
                          onClick={() => {
                            handleDelete(item.id);
                          }}
                        >
                          Remove
                        </Button>
                      </td>
                    </tr>
                  );
                })
              )}
            </tbody>
          </table>
        </div>
      </div>
      <Pagination
        postsPerPage={booksPerPage}
        totalPosts={books?.length}
        paginate={paginate}
        currentPage={currentPage}
      />
    </div>
  );
};

export default Books;
