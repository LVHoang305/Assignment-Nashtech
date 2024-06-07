import axiosConfig from "../configs/axiosConfig";

export const apiGetCategories = (id) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "get",
        url: `https://localhost:7215/api/Category/${id}`,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });

export const apiCreateCategories = (cate) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "post",
        url: `https://localhost:7215/api/Category/`,
        data: cate,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });

export const apiDeleteCategories = (id) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "delete",
        url: `https://localhost:7215/api/Category/${id}`,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });

export const apiUpdateCategories = (cate) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "put",
        url: `https://localhost:7215/api/Category/${cate.id}`,
        data: cate,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });

export const apiGetBookCategories = (id) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "get",
        url: `https://localhost:7215/api/BookCategory/${id}`,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });

export const apiCreateBookCategories = (cate) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "post",
        url: `https://localhost:7215/api/BookCategory/`,
        data: cate,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });

export const apiDeleteBookCategories = (id) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "delete",
        url: `https://localhost:7215/api/BookCategory/${id}`,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });

export const apiUpdateBookCategories = (cate) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "put",
        url: `https://localhost:7215/api/BookCategory/${cate.id}`,
        data: cate,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });

  export const apiDeleteAllBookCategories = (data) =>
    new Promise(async (resolve, reject) => {
      try {
        const response = await axiosConfig({
          method: "delete",
          url: `https://localhost:7215/api/BookCategory/`,
          params: {json: JSON.stringify(data)},
        });
        resolve(response);
      } catch (error) {
        reject(error);
      }
    });