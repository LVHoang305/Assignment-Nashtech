import axiosConfig from "../configs/axiosConfig";

export const apiGetUser = (id) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "get",
        url: `https://localhost:7215/api/User/${id}`,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });

export const apiCreateUser = (user) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "post",
        url: `https://localhost:7215/api/User/`,
        data: user,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });

export const apiDeleteUser = (id) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "delete",
        url: `https://localhost:7215/api/User/${id}`,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });

export const apiUpdateUser = (user) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "put",
        url: `https://localhost:7215/api/Book/${user.id}`,
        data: user,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });

  export const apiLogin = (user) =>
    new Promise(async (resolve, reject) => {
      try {
        const response = await axiosConfig({
          method: "post",
          url: `https://localhost:7215/api/User/Login`,
          data: user,
        });
        resolve(response);
      } catch (error) {
        reject(error);
      }
    });
