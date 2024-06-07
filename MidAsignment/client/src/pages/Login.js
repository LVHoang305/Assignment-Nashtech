import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { apiLogin } from "../services/User";
import { apiGetBookBorrowingRequestByField } from "../services/BorrowingBookRequest";
import { useAuthContext } from "../context/authContext";
import { useRequestContext } from "../context/requestContext";
import Swal from "sweetalert2";

const Login = () => {
  const navigate = useNavigate();
  const { isAuthenticated, setIsAuthenticated, setIsSuperUser } =
    useAuthContext();
  const { setRequest } = useRequestContext();

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
      const response = await apiLogin(formValues);
      response?.data?.user?.isSuperUser &&
        window.sessionStorage.setItem(
          "isSuperUser",
          response?.data?.user?.isSuperUser
        );
      window.sessionStorage.setItem("token", response?.data?.token);
      window.sessionStorage.setItem("userId", response?.data?.user?.id);
      const requestResponse = await apiGetBookBorrowingRequestByField({
        requestorId: response.data.user.id,
      });
      setRequest(requestResponse.data.length);
      setIsAuthenticated(true);
      setIsSuperUser(response?.data?.user?.isSuperUser);
    } catch (error) {
      console.error("Error logging:", error);
      Swal.fire('Oops !', 'Something went wrong' , 'error')
    }
  };
  useEffect(() => {
    isAuthenticated && navigate("/");
  }, [isAuthenticated]);

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
          Sign In
        </button>
        <div className="flex flex-auto w-full justify-between">
          <div className="flex items-center mt-6">
            <p className="text-sm text-gray-900 font-medium">
              <a href="register" className="hover:underline">
                Create new account
              </a>
            </p>
          </div>
          <div className="flex items-center mt-6">
            <p className="text-sm text-gray-900 font-medium">
              <a href="#" className="hover:underline">
                Forgot Password
              </a>
            </p>
          </div>
        </div>
      </form>
    </div>
  );
};

export default Login;
