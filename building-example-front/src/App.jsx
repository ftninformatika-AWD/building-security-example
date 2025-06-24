import React from "react";
import BuildingsPreview from "./components/buildings/BuildingsPreview";
import ApartmentsPreview from "./components/apartments/ApartmentsPreview";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Navbar from "./components/Navbar";
import CreateApartment from "./components/apartments/CreateApartment";
import EditApartment from "./components/apartments/EditApartment";

export default () => (
  <>
    <Router>
      <Navbar /> 
      <Routes>
        <Route path="/" element={<BuildingsPreview />} />
        <Route path="/buildings" element={<BuildingsPreview />} />
        <Route path="/apartments" element={<ApartmentsPreview />} />
        <Route path="/apartments/add" element={<CreateApartment />} />
        <Route path="/apartments/:id" element={<EditApartment />} />
      </Routes>
    </Router>
  </>
);
