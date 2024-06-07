import React, { useState, useEffect } from "react";
import { apiGetBookBorrowingRequestByField } from "../../../services/BorrowingBookRequest";


const BorrowedBooks = () => {
  const id = window.sessionStorage.getItem("userId");
  const [request, setRequest] = useState(null);

  const fetchBooks = async (bookId) => {
    const response = await apiGetBookBorrowingRequestByField(bookId);
    setRequest(response.data);
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
    fetchBooks({ requestorId: id });
  }, [id]);

  return (
    <div className="relative w-[1400px] h-[680px]">
      <div className="flex justify-between border-gray-200 border-b">
        <h1 className="text-white text-3xl font-medium py-4">Borrowed Books</h1>
      </div>
      <div className="flex flex-col">
        {!request ? (
          <tr>
            <td colSpan="6" className="text-center">
              You haven't borrow any book yet!
            </td>
          </tr>
        ) : (
          request.map((item) => {
            return (
              <div className="flex flex-col">
                <span className="bg-gray-200 w-full">
                  {formatDate(item.dateRequested)}
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
                      <th className="border text-center p-2 w-2/12">State</th>
                    </tr>
                  </thead>
                  <tbody>
                    {!item?.requestDetails.length > 0 ? (
                      <tr>
                        <td colSpan="6" className="text-center">
                          There is no book in this request!
                        </td>
                      </tr>
                    ) : (
                      item?.requestDetails?.map((a) => {
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
                            <td className="border text-center p-3 flex items-center justify-around">
                              {item?.status}
                            </td>
                          </tr>
                        );
                      })
                    )}
                  </tbody>
                </table>
              </div>
            );
          })
        )}
      </div>
    </div>
  );
};

export default BorrowedBooks;
