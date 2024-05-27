import React, { useState, useEffect } from "react";
import { apiGetProfile } from "../services/Profile";

const Profile = () => {
  const userId = localStorage.getItem("userId");
  const [profile, setProfile] = useState({});

  const fetchPost = async (id) => {
    try {
      const response = await apiGetProfile(userId);
      setProfile(response.data);
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    fetchPost(userId);
  }, [userId]);
  return (
    <div>
      <h1>ID: {profile?.id}</h1>
      <h1>Name: {profile?.name}</h1>
      <h1>Created At: {profile?.createdAt}</h1>
    </div>
  );
};

export default Profile;
