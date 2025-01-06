import { Route, BrowserRouter as Router, Routes } from "react-router-dom";
import "./App.css";
import Home from "./pages/Home";
import Login from "./pages/Login";
import ErrorPage from "./pages/ErrorPage";
import Register from "./pages/Register.tsx";
import urlEndpoints from "./constants/urlEndpoints";
import "./styles/global.css";
import PasswordResetRequest from "./pages/PasswordResetRequest.tsx";
import { PasswordReset } from "./pages/PasswordReset.tsx";

function App() {
  return (
    <Router>
      <Routes>
        <Route path={urlEndpoints.home} element={<Home />} />
        <Route path={urlEndpoints.login} element={<Login />} />
        <Route path={urlEndpoints.register} element={<Register />} />
        <Route
          path={urlEndpoints.passwordResetRequest}
          element={<PasswordResetRequest />}
        />
        <Route path={urlEndpoints.passwordReset} element={<PasswordReset />} />
        <Route path={urlEndpoints.error} element={<ErrorPage />} />
      </Routes>
    </Router>
  );
}

export default App;
