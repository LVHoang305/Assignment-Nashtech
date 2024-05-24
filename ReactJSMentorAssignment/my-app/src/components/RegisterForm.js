import React, { useEffect, useState } from "react";

const RegisterForm = () => {
  const [formValues, setFormValues] = useState({
    username: "",
    email: "",
    gender: "",
    password: "",
    confirmPassword: "",
    agreement: false,
  });

  const [clicked, setClicked] = useState({
    username: false,
    email: false,
    gender: false,
    password: false,
    confirmPassword: false,
    agreement: false,
  });

  const [formErrors, setFormErrors] = useState({});

  const validate = (values) => {
    const errors = {};

    if (!values.username) {
      errors.username = "Username is required";
    } else if (values.username.length < 4) {
      errors.username = "Username must be at least 4 characters";
    } else if (!/^[A-Za-z0-9]+$/.test(values.username)) {
      errors.username = "Username can only contain letters and numbers";
    }

    if (!values.email) {
      errors.email = "Email is required";
    } else if (!/\S+@\S+\.\S+/.test(values.email)) {
      errors.email = "Email address is invalid";
    }

    if (!values.gender) {
      errors.gender = "Gender is required";
    }

    if (!values.password) {
      errors.password = "Password is required";
    } else if (values.password.length < 8) {
      errors.password = "Password must be at least 8 characters";
    }

    if (!values.confirmPassword) {
      errors.confirmPassword = "Confirm password is required";
    } else if (values.confirmPassword !== values.password) {
      errors.confirmPassword = "Passwords do not match";
    }

    if (!values.agreement) {
      errors.agreement = "You must agree to the terms";
    }

    return errors;
  };

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setFormValues({
      ...formValues,
      [name]: type === "checkbox" ? checked : value,
    });
  };

  const handleClick = (e) => {
    const { name } = e.target;
    setClicked({
      ...clicked,
      [name]: true,
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log(formValues)
  };

  useEffect(() => {
    setFormErrors(validate(formValues));
  }, [formValues]);

  return (
    <div>
      <h2>Register</h2>
      <div style={{ justifyContent: "center", justifyItems: "center" }}>
        <form onSubmit={handleSubmit}>
          <div>
            <label>Username:</label>
            <input
              type="text"
              name="username"
              value={formValues.username}
              onClick={() =>
                setClicked({
                  ...clicked,
                  username: true,
                })
              }
              onChange={handleChange}
            />
            {formErrors.username && clicked.username && (
              <label style={{ color: "red" }}> - {formErrors.username}</label>
            )}
          </div>
          <div>
            <label>Email:</label>
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
          </div>
          <div>
            <label>Gender:</label>
            <select
              name="gender"
              value={formValues.gender}
              onChange={handleChange}
              onClick={(e) => handleClick(e)}
            >
              <option value="">Gender</option>
              <option value="male">Male</option>
              <option value="female">Female</option>
            </select>
            {formErrors.gender && clicked.gender && (
              <label style={{ color: "red" }}> - {formErrors.gender}</label>
            )}
          </div>
          <div>
            <label>Password:</label>
            <input
              type="password"
              name="password"
              value={formValues.password}
              onChange={handleChange}
              onClick={(e) => handleClick(e)}
            />
            {formErrors.password && clicked.password && (
              <label style={{ color: "red" }}> - {formErrors.password}</label>
            )}
          </div>
          <div>
            <label>Retype Password:</label>
            <input
              type="password"
              name="confirmPassword"
              value={formValues.confirmPassword}
              onChange={handleChange}
              onClick={(e) => handleClick(e)}
            />
            {formErrors.confirmPassword && clicked.confirmPassword && (
              <label style={{ color: "red" }}>
                {" "}
                - {formErrors.confirmPassword}
              </label>
            )}
          </div>
          <div>
            <label>
              <input
                type="checkbox"
                name="agreement"
                checked={formValues.agreement}
                onChange={handleChange}
                onClick={(e) => handleClick(e)}
              />
              I have read and agree to the terms
            </label>
            {formErrors.agreement && clicked.agreement && (
              <label style={{ color: "red" }}> - {formErrors.agreement}</label>
            )}
          </div>
          <div>
            <button type="submit" disabled={Object.keys(formErrors).length > 0}>
              Register
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default RegisterForm;
