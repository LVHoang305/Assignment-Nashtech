import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { apiGetToken } from "../services/Profile";

const Login = () => {
  const navigate = useNavigate();
  const [token, settoken] = useState("");
  const [formValues, setFormValues] = useState({
    email: "",
    password: "",
  });

  const [clicked, setClicked] = useState({
    email: false,
    password: false,
  });

  const [formErrors, setFormErrors] = useState({});

  const validate = (values) => {
    const errors = {};

    if (!values.email) {
      errors.email = "Email is required";
    } else if (!/\S+@\S+\.\S+/.test(values.email)) {
      errors.email = "Email address is invalid";
    }

    if (!values.password) {
      errors.password = "Password is required";
    } else if (values.password.length < 8) {
      errors.password = "Password must be at least 8 characters";
    }

    return errors;
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormValues({
      ...formValues,
      [name]: value,
    });
  };

  const handleClick = (e) => {
    const { name } = e.target;
    setClicked({
      ...clicked,
      [name]: true,
    });
  };

  useEffect(() => {
    setFormErrors(validate(formValues));
  }, [formValues]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = apiGetToken();
      navigate("/home");
    } catch (error) {
      console.error("Error logging:", error);
    }
  };

  return (
    <div className="w-[500px] mx-auto mt-10 items-center justify-center">
      <form onSubmit={handleSubmit}>
        <table className="w-full bg-blue-300 rounded-sm">
          <tbody>
            <tr>
              <td>Email:</td>
              <td>
                <input
                  type="email"
                  name="email"
                  value={formValues.email}
                  onClick={(e) => handleClick(e)}
                  onChange={handleChange}
                />
                {formErrors.email && clicked.email && (
                  <label style={{ color: "red" }}> - {formErrors.email}</label>
                )}
              </td>
            </tr>
            <tr>
              <td>Password:</td>
              <td>
                <input
                  type="password"
                  name="password"
                  value={formValues.password}
                  onChange={handleChange}
                  onClick={(e) => handleClick(e)}
                  minLength={8}
                />
                {formErrors.password && clicked.password && (
                  <label style={{ color: "red" }}>
                    {" "}
                    - {formErrors.password}
                  </label>
                )}
              </td>
            </tr>
            <div className="justify-center items-center">
              <button
                type="submit"
                className="bg-blue-500 rounded-md justify-center"
                disabled={Object.keys(formErrors).length > 0}
              >
                Login
              </button>
            </div>
          </tbody>
        </table>
      </form>
    </div>
  );
};

export default Login;
