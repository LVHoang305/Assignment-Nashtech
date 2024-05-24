import React, { useState, useEffect } from "react";
import { apiPosts } from "../api/Posts";
import Button from "react-bootstrap/Button";
import { FaSort } from "react-icons/fa";
import { NavLink } from "react-router-dom";

const Posts = () => {
  const [posts, setPosts] = useState([]);
  const postId = "";
  const [searchTerm, setSearchTerm] = useState("");
  const [sortConfig, setSortConfig] = useState({
    key: "id",
    direction: "ascending",
  });
  const [error, setError] = useState(null);

  const fetchPosts = async (postId) => {
    setPosts(undefined);
    setError(null);
    try {
      const response = await apiPosts(postId);
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
    post.title.toLowerCase().includes(searchTerm.toLowerCase())
  );

  const handleDelete = (id) => {
    const updatedPosts = posts.filter((post) => post.id !== id);
    setPosts(updatedPosts);
  };

  useEffect(() => {
    fetchPosts(postId);
  }, [postId]);

  return (
    <div className="relative h-[680px]">
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
        <table className="w-full bg-white">
          <thead>
            <tr className="border-black text-center justify-center p-2">
              <th
                onClick={() => handleSort("id")}
                className="border text-center p-2 cursor-pointer"
              >
                <div style={{ display: "flex", alignItems: "center" }}>
                  <span>ID</span>
                  <FaSort style={{ marginLeft: "4px" }} />
                </div>
              </th>
              <th
                onClick={() => handleSort("title")}
                className="border text-center p-2 cursor-pointer"
              >
                <div style={{ display: "flex", alignItems: "center" }}>
                  <span>Title</span>
                  <FaSort style={{ marginLeft: "4px" }} />
                </div>
              </th>
              <th
                onClick={() => handleSort("body")}
                className="border text-center p-2 cursor-pointer"
              >
                <div style={{ display: "flex", alignItems: "center" }}>
                  <span>Body</span>
                  <FaSort style={{ marginLeft: "4px" }} />
                </div>
              </th>
              <th className="border text-center p-2">Actions</th>
            </tr>
          </thead>
          <tbody>
            {!filteredPosts ? (
              <tr>
                <td>There is no post!</td>
              </tr>
            ) : (
              filteredPosts?.map((item) => {
                return (
                  <tr className="h-[50px]" key={item.id}>
                    <td className="border text-center p-2">{item?.id}</td>
                    <td className="border text-center p-2">{item?.title}</td>
                    <td className="border text-center p-2">{item?.body}</td>
                    <td className="border h-full text-center item-center p-2 flex-1 justify-around">
                      <NavLink
                        to={`/posts/${item.id}`}
                        className="justify-around items-center"
                      >
                        Detail
                      </NavLink>
                      <p className="text-white"> - </p>
                      <NavLink
                        to={`/posts/edit/${item.id}`}
                        className="justify-around items-center"
                      >
                        Edit
                      </NavLink>
                      <p className="text-white"> - </p>
                      <Button
                        onClick={() => {
                          handleDelete(item.id);
                        }}
                      >
                        {" "}
                        Delete{" "}
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
  );
};

export default Posts;
