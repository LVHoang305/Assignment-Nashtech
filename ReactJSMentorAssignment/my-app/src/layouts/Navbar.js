import React from "react";
import { NavLink, useNavigate } from "react-router-dom";

const nav = [
  { name: "HOME", path: "/home" },
  { name: "POSTS", path: "/posts" },
  { name: "PROFILE", path: "/profile" },
];

const Navbar = () => {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();

  return (
    <div className="w-full flex justify-center items-center h-[30px] bg-black text-white">
      <div className="w-1100 flex justify-around items-center h-[30px] text-16">
        {nav?.length > 0 &&
          nav.map((item, index) => {
            return (
              <div key={index} className="h-full justify-center items-center">
                <NavLink to={item.path} className="justify-around items-center">
                  {item.name}
                </NavLink>
              </div>
            );
          })}
        {!token ? (
          <div className="h-full justify-center items-center">
            <NavLink to={"/login"} className="justify-around items-center">
              LOGIN
            </NavLink>
          </div>
        ) : (
          <div
            onClick={() => {
              localStorage.removeItem("token");
              localStorage.removeItem("userId");
              navigate("/home");
            }}
            className="h-full justify-center items-center cursor-pointer"
          >
            <span className="justify-around items-center">LOGOUT</span>
          </div>
        )}
      </div>
    </div>
  );
};

export default Navbar;
