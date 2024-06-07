import axiosConfig from "../configs/axiosConfig";

export const apiGetBookBorrowingRequest = (id) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "get",
        url: `https://localhost:7215/api/BookBorrowingRequest/${id}`,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });

  export const apiGetBookBorrowingRequestThisMonth = (id) =>
    new Promise(async (resolve, reject) => {
      try {
        const response = await axiosConfig({
          method: "get",
          url: `https://localhost:7215/api/BookBorrowingRequest/ThisMonth/${id}`,
        });
        resolve(response);
      } catch (error) {
        reject(error);
      }
    });

    export const apiGetBookBorrowingRequestByField = (field) =>
      new Promise(async (resolve, reject) => {
        try {
          const response = await axiosConfig({
            method: "get",
            url: `https://localhost:7215/api/BookBorrowingRequest/ByField`,
            params: {json: JSON.stringify(field)},
          });
          resolve(response);
        } catch (error) {
          reject(error);
        }
      });

export const apiCreateBookBorrowingRequest = (request) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "post",
        url: `https://localhost:7215/api/BookBorrowingRequest/`,
        data: request,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });

export const apiDeleteBookBorrowingRequest = (id) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "delete",
        url: `https://localhost:7215/api/BookBorrowingRequest/${id}`,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });

export const apiUpdateBookBorrowingRequest = (request) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "put",
        url: `https://localhost:7215/api/BookBorrowingRequest/${request.id}`,
        data: request,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });

export const apiGetBookBorrowingRequestDetails = (id) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "get",
        url: `https://localhost:7215/api/BookBorrowingRequestDetails/${id}`,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });

export const apiCreateBookBorrowingRequestDetails = (request) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "post",
        url: `https://localhost:7215/api/BookBorrowingRequestDetails/`,
        data: request,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });

export const apiDeleteBookBorrowingRequestDetails = (id) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "delete",
        url: `https://localhost:7215/api/BookBorrowingRequestDetails/${id}`,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });

export const apiUpdateBookBorrowingRequestDetails = (request) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "put",
        url: `https://localhost:7215/api/BookBorrowingRequestDetails/${request.id}`,
        data: request,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });
