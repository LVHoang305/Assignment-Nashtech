import React from "react";
import { NavLink } from "react-router-dom";
import { useAuthContext } from "../context/authContext";

const nav = [
  { name: "Home", path: "/" },
  { name: "Posts", path: "/posts" },
  { name: "Books", path: "/books" },
  { name: "Profile", path: "/profile" },
];

function Navbar() {
  const { isAuthenticated, setIsAuthenticated } = useAuthContext();

  const handleLogout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("userId");
    setIsAuthenticated(false);
  };
  const navList = (
    <ul className="mb-4 mt-2 flex flex-col gap-2 text-inherit lg:mb-0 lg:mt-0 lg:flex-row lg:items-center lg:gap-6">
      {nav.map(({ name, path }) => (
        <li key={name} className="capitalize font-bold">
          <NavLink to={path} className="flex items-center gap-1 p-1">
            {name}
          </NavLink>
        </li>
      ))}
    </ul>
  );

  return (
    <nav className="text-white">
      <div className="container mx-auto flex items-center justify-between">
        <NavLink to="/">
          <span className="mr-4 ml-2 cursor-pointer py-1.5 font-bold">
            NashTech Post
          </span>
        </NavLink>
        <div className="hidden lg:block">{navList}</div>
        <div className="hidden gap-2 lg:flex">
          {!isAuthenticated ? (
            <NavLink className="hidden lg:inline-block" to="/login">
              <button className="bg-gradient-to-r from-purple-400 via-pink-500 to-red-500 text-white py-1 px-4 rounded">
                LOGIN
              </button>
            </NavLink>
          ) : (
            <button
              onClick={handleLogout}
              className="bg-gradient-to-r from-purple-400 via-pink-500 to-red-500 text-white py-1 px-4 rounded"
            >
              LOGOUT
            </button>
          )}
        </div>
      </div>
    </nav>
  );
}

export default Navbar;
