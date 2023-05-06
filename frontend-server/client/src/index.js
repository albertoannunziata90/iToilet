import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import ReviewPage from "./ReviewPage";
import ToiletsNearYou from "./toiletsNearYou";
import "./index.css";
import reportWebVitals from "./reportWebVitals";
import "bulma/css/bulma.min.css";

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <div>
    <div>
      <nav class="navbar" role="navigation" aria-label="main navigation">
        <div class="navbar-brand">
          <a class="navbar-item" href="/">
            <img src="/logo.png" width="112" height="40" alt="itoilet logo" />
          </a>

          <a
            role="button"
            class="navbar-burger"
            aria-label="menu"
            aria-expanded="false"
            data-target="navbarBasicExample"
          >
            <span aria-hidden="true"></span>
            <span aria-hidden="true"></span>
            <span aria-hidden="true"></span>
          </a>
        </div>
        <div id="navbarBasicExample" class="navbar-menu">
          <a class="navbar-item" href="/review">
            Review
          </a>
        </div>
      </nav>

      <React.StrictMode>
        <BrowserRouter>
          <Routes>
            <Route path="/" element={<ToiletsNearYou />}></Route>
            <Route path="/review/*" element={<ReviewPage />}></Route>
          </Routes>
        </BrowserRouter>
      </React.StrictMode>
    </div>
  </div>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals(console.log);
