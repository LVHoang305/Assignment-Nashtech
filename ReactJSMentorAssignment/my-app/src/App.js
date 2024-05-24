import "./App.css";
import React from "react";
import { Route, Routes } from 'react-router-dom';
import { Public, Home, Posts, Profile, DetailPost, EditPost, CreatePost, Login } from "./components"

function App() {
  return (
    <div className="App h-full w-full" >
      <Routes>
        <Route path="/" element= {<Public/>}>
          <Route path={'/home'} element= {<Home />}/>
          <Route path={'/posts'} element= {<Posts/>}/>
          <Route path={'/profile'} element= {<Profile />}/>
          <Route path={"/posts/:id"} element= {<DetailPost/>}/>
          <Route path={"/posts/edit/:id"} element= {<EditPost/>}/>
          <Route path={"/posts/create"} element= {<CreatePost/>}/>
          <Route path={'/login'} element= {<Login />}/>
        </Route>
      </Routes>
    </div>
  );
}

export default App;
