import axiosConfig from "../configs/axiosConfig";

export const apiGetBooks = (id) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "get",
        url: `http://localhost:5000/books/${id}`,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });

export const apiCreateBooks = (book) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "post",
        url: `http://localhost:5000/books/`,
        data: book,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });

export const apiDeleteBooks = (id) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "delete",
        url: `http://localhost:5000/books/${id}`,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });

export const apiUpdateBooks = (book) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "put",
        url: `http://localhost:5000/books/${book.id}`,
        data: book,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });
