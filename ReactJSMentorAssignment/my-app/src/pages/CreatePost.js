import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Button from "react-bootstrap/esm/Button";
import { apiCreatePosts, apiGetPosts } from "../services/Posts";

const CreatePost = () => {
  const [title, setTitle] = useState("");
  const [body, setBody] = useState("");
  const postId = "";
  const [id, setId] = useState(0);
  const navigate = useNavigate();

  const fetchPosts = async (postId) => {
    try {
      const response = await apiGetPosts(postId);
      setId(String(response.data.length + 1));
    } catch (error) {}
  };

  useEffect(() => {
    fetchPosts(postId);
  }, [postId]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const newPost = {
      userId: 1,
      id,
      title,
      body,
    };

    try {
      const response = apiCreatePosts(newPost);
      console.log("Post created:", newPost);
      navigate("/posts");
    } catch (error) {
      console.error("Error creating post:", error);
    }
  };

  return (
    <div className="mt-10 w-1100">
      <h1 className="text-2xl font-bold mb-4">Create New Post</h1>
      <form
        onSubmit={handleSubmit}
        className="bg-white p-6 rounded shadow-sm w-1100"
      >
        <div className="mb-4">
          <label className="block mb-2">Title </label>
          <input
            type="text"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            minLength={5}
            maxLength={100}
            className="w-full p-2 border rounded"
            required
          />
        </div>
        <div className="mb-4">
          <label className="block mb-2">Body</label>
          <textarea
            value={body}
            onChange={(e) => setBody(e.target.value)}
            className="w-full p-2 border rounded"
            minLength={5}
            maxLength={800}
            rows="8"
            required
          ></textarea>
        </div>
        <button type="submit" className="bg-blue-500 text-white p-2 rounded">
          Create Post
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

export default CreatePost;
