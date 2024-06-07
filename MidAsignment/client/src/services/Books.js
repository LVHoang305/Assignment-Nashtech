import axiosConfig from "../configs/axiosConfig";

export const apiGetBooks = (id) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "get",
        url: `https://localhost:7215/api/Book/${id}`,
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
        url: `https://localhost:7215/api/Book/`,
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
        url: `https://localhost:7215/api/Book/${id}`,
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
        url: `https://localhost:7215/api/Book/${book.id}`,
        data: book,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });
