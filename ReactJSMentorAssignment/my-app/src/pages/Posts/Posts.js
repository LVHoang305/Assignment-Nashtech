import React, { useState, useEffect } from "react";
import { apiGetPosts, apiDeletePosts } from "../../services/Posts";
import Button from "react-bootstrap/Button";
import { FaSort } from "react-icons/fa";
import { NavLink } from "react-router-dom";
import { Pagination } from "../../components";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";

const MySwal = withReactContent(Swal);

const Posts = () => {
  const [posts, setPosts] = useState([]);
  const postId = "";
  const [searchTerm, setSearchTerm] = useState("");
  const [sortConfig, setSortConfig] = useState({
    key: "id",
    direction: "ascending",
  });
  const [error, setError] = useState(null);
  const [currentPage, setCurrentPage] = useState(1);
  const postsPerPage = 13;

  const fetchPosts = async (postId) => {
    setPosts(undefined);
    setError(null);
    try {
      const response = await apiGetPosts(postId);
      setPosts(response.data);
    } catch (error) {
      setError("Failed to fetch Posts data");
    } finally {
      console.log(posts);
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

  const sortedPosts = posts?.sort((a, b) => {
    if (a[sortConfig.key] < b[sortConfig.key]) {
      return sortConfig.direction === "ascending" ? -1 : 1;
    }
    if (a[sortConfig.key] > b[sortConfig.key]) {
      return sortConfig.direction === "ascending" ? 1 : -1;
    }
    return 0;
  });

  const filteredPosts = sortedPosts?.filter((post) =>
    post?.title?.toLowerCase().includes(searchTerm.toLowerCase())
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
        await apiDeletePosts(id);
        fetchPosts(postId);
        MySwal.fire("Deleted!", "Your post has been deleted.", "success");
      } catch (error) {
        console.error("Error deleting post:", error);
        MySwal.fire("Error!", "There was an error deleting the post.", "error");
      }
    }
  };

  useEffect(() => {
    fetchPosts(postId);
  }, [postId]);

  const indexOfLastPost = currentPage * postsPerPage;
  const indexOfFirstPost = indexOfLastPost - postsPerPage;
  const currentPosts = filteredPosts?.slice(indexOfFirstPost, indexOfLastPost);

  // Change page
  const paginate = (pageNumber) => setCurrentPage(pageNumber);

  return (
    <div className="relative w-[1300px] h-[680px]">
      <div className="flex justify-between border-gray-200 border-b">
        <h1 className="text-white text-3xl font-medium py-4">Posts</h1>
        <NavLink
          to={"/posts/create"}
          className="text-green-200 text-2xl font-medium py-5"
        >
          New Post
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
                <div style={{ display: "flex", alignItems: "center" }}>
                  <span>ID</span>
                  <FaSort style={{ marginLeft: "4px" }} />
                </div>
              </th>
              <th
                onClick={() => handleSort("title")}
                className="border text-center p-2 cursor-pointer w-3/12"
              >
                <div style={{ display: "flex", alignItems: "center" }}>
                  <span>Title</span>
                  <FaSort style={{ marginLeft: "4px" }} />
                </div>
              </th>
              <th
                onClick={() => handleSort("body")}
                className="border text-center p-2 cursor-pointer w-5/12"
              >
                <div style={{ display: "flex", alignItems: "center" }}>
                  <span>Body</span>
                  <FaSort style={{ marginLeft: "4px" }} />
                </div>
              </th>
              <th className="border text-center p-2 w-3/12">Actions</th>
            </tr>
          </thead>
          <tbody>
            {!filteredPosts ? (
              <tr>
                <td>There is no post!</td>
              </tr>
            ) : (
              currentPosts?.map((item) => {
                return (
                  <tr className="h-[50px]" key={item.id}>
                    <td className="border text-center p-2">{item?.id}</td>
                    <td className="border text-center p-2 truncate">{item?.title}</td>
                    <td className="border text-center p-2 truncate">{item?.body}</td>
                    <td className="border text-center p-3 flex items-center h-full justify-around">
                      <NavLink
                        to={`/posts/${item.id}`}
                        className="text-blue-500 flex-1 text-center hover:underline"
                      >
                        Detail
                      </NavLink>
                      <span className="text-white"> - </span>
                      <NavLink
                        to={`/posts/${item.id}/edit`}
                        className="text-yellow-500 flex-1 text-center hover:underline"
                      >
                        Edit
                      </NavLink>
                      <span className="text-white"> - </span>
                      <Button
                        variant="danger"
                        onClick={() => handleDelete(item.id)}
                        className="flex-1 text-center "
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
        postsPerPage={postsPerPage}
        totalPosts={posts?.length}
        paginate={paginate}
        currentPage={currentPage}
      />
    </div>
  );
};

export default Posts;
