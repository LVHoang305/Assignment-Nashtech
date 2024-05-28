import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { apiGetToken } from "../services/Profile";
import { useAuthContext } from "../context/authContext";

const Login = () => {
  const navigate = useNavigate();
  const { setIsAuthenticated } = useAuthContext();
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
      const response = await apiGetToken();
      localStorage.setItem("token", response?.data?.token);
      localStorage.setItem("userId", response?.data?.userId);
      setIsAuthenticated(true);
      navigate("/home");
    } catch (error) {
      console.error("Error logging:", error);
    }
  };

  return (
    <div className="w-full mt-10">
      <div className="text-center">
        <h2 className="text-3xl font-bold mb-4">Sign In</h2>
        <p className="text-lg text-blue-gray-600">
          Enter your email and password to Sign In.
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
            minLength={8}
            className="w-full px-4 py-3 border border-blue-gray-200 rounded-lg focus:border-gray-900 focus:outline-none"
          />
        </div>
        <div className="flex items-center mb-6">
          <input
            id="agree"
            type="checkbox"
            className="text-blue-500 rounded border-gray-300 focus:border-gray-900 focus:ring focus:ring-gray-200 focus:ring-opacity-50 mr-2"
          />
          <label
            htmlFor="agree"
            className="text-sm text-blue-gray-600 font-medium"
          >
            I agree the&nbsp;
            <a
              href="#"
              className="text-black transition-colors hover:text-gray-900 underline"
            >
              Terms and Conditions
            </a>
          </label>
        </div>
        <button className="w-full bg-blue-500 text-white font-medium py-3 rounded-lg hover:bg-blue-600 transition-colors">
          Sign In
        </button>
        <div className="flex items-center justify-end mt-6">
          <p className="text-sm text-gray-900 font-medium">
            <a href="#" className="hover:underline">
              Forgot Password
            </a>
          </p>
        </div>
      </form>
    </div>
  );
};

export default Login;
