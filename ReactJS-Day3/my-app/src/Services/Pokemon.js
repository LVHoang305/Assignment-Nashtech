import axiosConfig from "../axiosConfig";

export const apiPokemon = (query) =>
  new Promise(async (resolve, reject) => {
    try {
      const response = await axiosConfig({
        method: "get",
        url: `https://pokeapi.co/api/v2/pokemon/${query}`,
      });
      resolve(response);
    } catch (error) {
      reject(error);
    }
  });
