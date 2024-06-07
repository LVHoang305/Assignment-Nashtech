import React, { useState, useEffect } from "react";
import { apiGetBooks, apiDeleteBooks } from "../../../services/Books";
import { apiDeleteAllBookCategories } from "../../../services/Categories";
import Button from "react-bootstrap/Button";
import { FaSort } from "react-icons/fa";
import { NavLink } from "react-router-dom";
import { Pagination } from "../../../components";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";

const MySwal = withReactContent(Swal);

const Books = () => {
  const [books, setBooks] = useState([]);
  const bookId = "";
  const [searchTerm, setSearchTerm] = useState("");
  const [sortConfig, setSortConfig] = useState({
    key: "id",
    direction: "ascending",
  });
  const [error, setError] = useState(null);
  const [currentPage, setCurrentPage] = useState(1);
  const booksPerPage = 13;

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
      confirmButtonText: "Yes, delete it!",
      cancelButtonText: "No, cancel!",
    });

    if (result.isConfirmed) {
      try {
        //await apiDeleteAllBookCategories({ bookId : id })
        await apiDeleteBooks(id);
        fetchBooks(bookId);
        MySwal.fire("Deleted!", "Your book has been deleted.", "success");
      } catch (error) {
        console.error("Error deleting book:", error);
        MySwal.fire("Error!", "There was an error deleting the book.", "error");
      }
    }
  };

  useEffect(() => {
    fetchBooks(bookId);
  }, [bookId]);

  const indexOfLastBook = currentPage * booksPerPage;
  const indexOfFirstBook = indexOfLastBook - booksPerPage;
  const currentBooks = filteredBooks?.slice(indexOfFirstBook, indexOfLastBook);

  const paginate = (pageNumber) => setCurrentPage(pageNumber);

  return (
    <div className="relative w-[1300px] h-[680px]">
      <div className="flex justify-between border-gray-200 border-b">
        <h1 className="text-white text-3xl font-medium py-4">Books</h1>
        <NavLink
          to={"create"}
          className="text-green-200 text-2xl font-medium py-5"
        >
          New Book
        </NavLink>
      </div>
      <input
        className="font-medium bg-blue-200 border-black w-full px-4"
        type="text"
        placeholder="Search by title"
        value={searchTerm}
        onChange={handleSearch}
      />
      {error ? (
        <div>{error}</div>
      ) : (
        <table className="w-full bg-white table-fixed">
          <thead>
            <tr className="border-black text-center justify-center p-2">
              <th
                onClick={() => handleSort("id")}
                className="border text-center p-2 cursor-pointer w-1/12"
              >
                <div className="flex items-center justify-center">
                  <span>ID</span>
                  <FaSort className="ml-2" />
                </div>
              </th>
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
                    <td className="border text-center p-2">{item?.id}</td>
                    <td className="border text-center p-2 truncate">
                      {item?.title}
                    </td>
                    <td className="border text-center p-2 truncate">
                      {item?.author}
                    </td>
                    <td className="border text-center p-3 flex items-center justify-around">
                      <NavLink
                        to={`/books/${item.id}`}
                        className="text-blue-500 hover:underline"
                      >
                        Detail
                      </NavLink>
                      <span className="text-white"> - </span>
                      <NavLink
                        to={`/system/books/${item.id}/edit`}
                        className="text-yellow-500 hover:underline"
                      >
                        Edit
                      </NavLink>
                      <span className="text-white"> - </span>
                      <Button
                        variant="danger"
                        onClick={() => handleDelete(item.id)}
                      >
                        Delete
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
        postsPerPage={booksPerPage}
        totalPosts={books?.length}
        paginate={paginate}
        currentPage={currentPage}
      />
    </div>
  );
};

export default Books;
