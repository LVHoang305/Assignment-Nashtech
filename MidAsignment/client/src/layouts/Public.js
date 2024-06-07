import React from "react";
import { Navbar, Footer } from "./";
import { AppRoute } from "../route/AppRoute";
import bg from "../assets/img/bg.png";

const Public = () => {
  return (
    <div
      className="w-full flex flex-col items-center h-full"
      style={{
        backgroundImage: `url(${bg})`,
        width: "100%",
        minHeight: "100vh",
        backgroundSize: "cover",
        backgroundAttachment: "fixed",
        backgroundRepeat: "space",
      }}
    >
      <div className="bg-gray-800 w-full h-[50px]">
        <div className="container absolute left-2/4 mx-auto -translate-x-2/4 p-2">
          <Navbar />
        </div>
      </div>  
      <div className="container abbsolute w-1100 min-h-screen flex flex-col items-center justify-start p-[20px]">
        <AppRoute />
      </div>

      <Footer />
    </div>
  );
};

export default Public;
