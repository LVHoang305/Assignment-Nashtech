import React, { useEffect, useState } from "react";
import { apiPokemon } from "../Services/Pokemon";
import Button from "react-bootstrap/Button";

const Pokemon = () => {
  const [pokemon, setPokemon] = useState({});
  const [number, setNumber] = useState(1);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const FetchPokemon = async (number) => {
    setLoading(true);
    setError(null);
    setPokemon(undefined);
    try {
      const response = await apiPokemon(number);
      setPokemon(response.data);
    } catch (error) {
      setError("Failed to fetch Pokemon data");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    FetchPokemon(number);
  }, [number]);
  console.log(pokemon);
  return (
    <div>
      {loading && <p>Loading...</p>}
      {error && <p style={{ color: "red" }}>{error}</p>}
      {pokemon && (
        <div>
          <h1>ID: {pokemon?.id}</h1>
          <h1>Name: {pokemon?.species?.name}</h1>
          <h1>Weight: {pokemon?.weight}</h1>
          <div>
            <img
              src={pokemon?.sprites?.front_default}
              alt="Front"
              style={{ width: "200px", height: "auto" }}
            />
            <img
              src={pokemon?.sprites?.back_default}
              alt="Back"
              style={{ width: "200px", height: "auto" }}
            />
          </div>
          <div style={{ display: "flex", gap: 30, justifyContent: "center" }}>
            <Button
              variant="light"
              onClick={() => setNumber(number - 1)}
              disabled={number === 1}
            >
              Previous
            </Button>
            <Button variant="light" onClick={() => setNumber(number + 1)}>
              Next
            </Button>
          </div>
        </div>
      )}
    </div>
  );
};

export default Pokemon;
