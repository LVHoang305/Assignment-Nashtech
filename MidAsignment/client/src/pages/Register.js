import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { apiCreateUser } from "../services/User";

const Login = () => {
  const navigate = useNavigate();
  const [formValues, setFormValues] = useState({
    email: "",
    password: "",
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormValues({
      ...formValues,
      [name]: value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await apiCreateUser(formValues);
    navigate("/login")
    } catch (error) {
      console.error("Error logging:", error);
    }
  };

  return (
    <div className="w-full mt-10">
      <div className="text-center">
        <h2 className="text-3xl font-bold mb-4">Register</h2>
        <p className="text-lg text-blue-gray-600">
          Enter your email and password to Sign Up.
        </p>
      </div>
      <form
        onSubmit={handleSubmit}
        className="mt-8 mb-2 mx-auto w-80 max-w-screen-lg lg:w-1/2"
      >
        <div className="mb-6 flex flex-col gap-6">
          <label
            htmlFor="email"
            className="text-md text-blue-gray-600 font-bold mb-1"
          >
            EMAIL
          </label>
          <input
            id="email"
            type="email"
            placeholder="name@mail.com"
            name="email"
            value={formValues.email}
            onChange={handleChange}
            required
            minLength={5}
            maxLength={100}
            className="w-full px-4 py-3 border border-blue-gray-200 rounded-lg focus:border-gray-900 focus:outline-none"
          />
          <label
            htmlFor="password"
            className="text-md text-blue-gray-600 font-bold mb-1"
          >
            PASSWORD
          </label>
          <input
            id="password"
            type="password"
            placeholder="********"
            name="password"
            value={formValues.password}
            onChange={handleChange}
            required
            minLength={5}
            maxLength={100}
            className="w-full px-4 py-3 border border-blue-gray-200 rounded-lg focus:border-gray-900 focus:outline-none"
          />
        </div>
        <button className="w-full bg-blue-500 text-white font-medium py-3 rounded-lg hover:bg-blue-600 transition-colors">
          Sign Up
        </button>
      </form>
    </div>
  );
};

export default Login;
