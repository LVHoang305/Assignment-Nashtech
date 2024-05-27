import axiosConfig from "../configs/axiosConfig";

export const apiGetProfile = (id) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "get",
        url: `https://60dff0ba6b689e001788c858.mockapi.io/users/${id}`,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });

export const apiGetToken = () =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "get",
        url: `https://60dff0ba6b689e001788c858.mockapi.io/tokens`,
      });
      resolve(response);
      localStorage.setItem("token", response?.data?.token);
      localStorage.setItem("userId", response?.data?.userId);
    } catch (error) {
      reject(error);
    }
  });
