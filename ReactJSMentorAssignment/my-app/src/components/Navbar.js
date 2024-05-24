import React, { useState } from "react";
import { NavLink } from "react-router-dom";

const nav = [
  { name: "HOME", path: "/home" },
  { name: "POSTS", path: "/posts"},
  { name: "PROFILE", path: "/profile" },
];


const Navbar = () => {
  const [isLogined, setIsLogined] = useState(false);
  console.log(isLogined)

  return (
    <div className="w-full flex justify-center items-center h-[30px] bg-black text-white">
      <div className="w-1100 flex justify-around items-center h-[30px] text-16">
        {nav?.length > 0 &&
          nav.map((item, index) => {
            return (
              <div
                key={index}
                className="h-full justify-center items-center"
              >
                <NavLink
                  to={item.path}
                  className="justify-around items-center"
                >
                  {item.name}
                </NavLink>
              </div>
            );
          })}
          {!isLogined ? 
          <div
                onClick={() => setIsLogined(true)}
                className="h-full justify-center items-center"
              >
                <span
                  className="justify-around items-center cursor-pointer"
                >
                  LOGIN
                </span>
              </div>
          :
              <div
                onClick={() => setIsLogined(false)}
                className="h-full justify-center items-center cursor-pointer"
              >
                <span
                  className="justify-around items-center"
                >
                  LOGOUT
                </span>
              </div>}
      </div>
    </div>
  );
};

export default Navbar;
