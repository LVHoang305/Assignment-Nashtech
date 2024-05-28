import axiosConfig from "../configs/axiosConfig";

export const apiGetPosts = (id) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "get",
        url: `http://localhost:5000/posts/${id}`,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });

export const apiCreatePosts = (post) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "post",
        url: `http://localhost:5000/posts/`,
        data: post,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });

export const apiDeletePosts = (id) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "delete",
        url: `http://localhost:5000/posts/${id}`,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });

export const apiUpdatePosts = (post) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "put",
        url: `http://localhost:5000/posts/${post.id}`,
        data: post,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });
