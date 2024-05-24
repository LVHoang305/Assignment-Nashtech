import axiosConfig from "../axiosConfig";

export const apiPosts = (id) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "get",
        url: `https://jsonplaceholder.typicode.com/posts/${id}`,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });
