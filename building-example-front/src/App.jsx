import React from "react";
import BuildingsPreview from "./components/buildings/BuildingsPreview";
import ApartmentsPreview from "./components/apartments/ApartmentsPreview";
import { BrowserRouter as Router, Routes, Route, Navigate } from "react-router-dom";
import Navbar from "./components/Navbar";
import CreateApartment from "./components/apartments/CreateApartment";
import EditApartment from "./components/apartments/EditApartment";
import SignIn from "./components/users/SignIn";
import RegisterForm from "./components/users/RegisterForm";
import { useAuth } from "./context/AuthContext";

const App = () => {
  const { isAuthenticated } = useAuth();
  
  return (
    <>
      <Router>
        <Navbar /> 
        <Routes>
          <Route path="/" element={<BuildingsPreview />} />
          <Route path="/buildings" element={<BuildingsPreview />} />
          <Route path="/apartments" element={<ApartmentsPreview />} />

          {isAuthenticated && (
            <>
              <Route path="/apartments/add" element={<CreateApartment />} />
              <Route path="/apartments/:id" element={<EditApartment />} />
            </>
          )}

          <Route path="/signin" element={isAuthenticated ? <Navigate replace to="/" /> : <SignIn />}/>
          <Route path="/register" element={isAuthenticated ? <Navigate replace to="/" /> : <RegisterForm />} />
          
          <Route path="*" element={<Navigate replace to="/signin" />} />
        </Routes>
      </Router>
    </>
  );
};

export default App;
