import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { apiPosts } from "../api/Posts";
import Button from "react-bootstrap/esm/Button";

const EditPost = () => {
  const [post, setPost] = useState({});
  const [error, setError] = useState(null);
  const { id } = useParams();
  const navigate = useNavigate();

  const fetchPost = async (id) => {
    setPost(undefined);
    setError(null);
    try {
      const response = await apiPosts(id);
      setPost(response.data);
    } catch (error) {
      setError("Failed to fetch Posts data");
    } finally {
      console.log(post);
    }
  };

  useEffect(() => {
    fetchPost(id);
  }, [id]);

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      //call API
      console.log("Post edited:", post);
      navigate("/posts");
    } catch (error) {
      console.error("Error editing post:", error);
    }
  };

  return (
    <div className="w-1100 mx-auto mt-10">
      <h1 className="text-2xl font-bold mb-4">Edit Post</h1>
      <form onSubmit={handleSubmit} className="bg-white p-6 rounded shadow-sm">
        <div className="mb-4">
          <label className="block mb-2">Title</label>
          <input
            type="text"
            value={post?.title}
            defaultValue={post?.title}
            onChange={(e) =>
              setPost((prev) => ({ ...prev, title: e.target.value }))
            }
            minLength={5}
            maxLength={100}
            className="w-full p-2 border rounded"
            required
          />
        </div>
        <div className="mb-4">
          <label className="block mb-2">Body</label>
          <textarea
            value={post?.body}
            defaultValue={post?.value}
            onChange={(e) =>
              setPost((prev) => ({ ...prev, body: e.target.value }))
            }
            className="w-full p-2 border rounded"
            rows="8"
            minLength={5}
            maxLength={800}
            required
          ></textarea>
        </div>
        <button type="submit" className="bg-blue-500 text-white p-2 rounded">
          Edit Post
        </button>
      </form>
      <Button
        className="bg-green-200 rounded-md mt-10 px-3 py-3"
        onClick={() => navigate("/posts")}
      >
        Back to Posts
      </Button>
    </div>
  );
};

export default EditPost;
