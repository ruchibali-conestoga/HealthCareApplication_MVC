﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareApplication_MVC.Database;
using Microsoft.Data.SqlClient;
using System;
using HealthCareApplication_MVC.Models;


namespace HealthCareApplication_MVC.Controllers
{
    public class PatientsController : Controller
    {
        private readonly PatientDb _patientDAL;

        public PatientsController(IConfiguration configuration)
        {
            _patientDAL = new PatientDb(configuration);
        }

        //PatientDb dal;//= new PatientDb();

        // GET: Patient
        public ActionResult Index()
        {
            return View(_patientDAL.GetAllPatients());
        }

        // GET: Patient/Create
        public ActionResult SavePatient()
        {
            return View();
        }

        // POST: Patient/Create
        [HttpPost]
        public ActionResult SavePatient([FromBody] Patient patient)
        {
            if (patient == null || string.IsNullOrEmpty(patient.Name))
            {
                return BadRequest("Invalid patient data.");
            }

            _patientDAL.AddPatient(patient);
            //return Ok(new { message = "Patient saved successfully!" });
            return View();
        }
            
            
        //    (Patient patient)
        //{
        //    _patientDAL.AddPatient(patient);
        //    return RedirectToAction("Index");
        //}
        //public IActionResult PateintHomePage()
        //{
        //    var patients = _patientDAL.GetAllPatients();
        //    return View(patients);
        //}
        //[Route("Account")]
        //[HttpPost]
        //public IActionResult SavePatient1([FromBody] Patient patient)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState); // Send validation errors to AJAX
        //    }

        //    if (patient.Id == 0)
        //    {
        //        _patientDAL.AddPatient(patient); // Create
        //    }
        //    else
        //    {
        //        _patientDAL.UpdatePatient(patient); // Update
        //    }

        //    return Ok(new { message = "Patient saved successfully!" });
        //}


        //[Route("Patient")]
        //[HttpPost]
        //public IActionResult SavePatient(Patient patient)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (patient.Id == 0)
        //        {
        //            _patientDAL.AddPatient(patient); // Create
        //        }
        //        else
        //        {
        //            _patientDAL.UpdatePatient(patient); // Update
        //        }

        //    }
        //    return View("PateintHomePage");
        //}
        //return View("PateintHomePage", _patientDAL.GetAllPatients());
        #region
        //// 1️⃣ LIST ALL PATIENTS
        //public IActionResult Index()
        //{
        //    var patients = _patientDAL.GetAllPatients();
        //    return View(patients);
        //}

        //// 2️⃣ CREATE PATIENT - GET
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// 2️⃣ CREATE PATIENT - POST
        //[HttpPost]
        //public IActionResult Create(Patient patient)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _patientDAL.AddPatient(patient);
        //        return RedirectToAction("PatientHomePage");
        //    }
        //    return View(patient);
        //}

        //// 3️⃣ EDIT PATIENT - GET
        //public IActionResult Edit(int id)
        //{
        //    var patient = _patientDAL.GetPatientById(id);
        //    if (patient == null) return NotFound();
        //    return View(patient);
        //}

        //// 3️⃣ EDIT PATIENT - POST
        //[HttpPost]
        //public IActionResult Edit(Patient patient)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _patientDAL.UpdatePatient(patient);
        //        return RedirectToAction("PatientHomePage");
        //    }
        //    return View(patient);
        //}

        //// 4️⃣ DELETE PATIENT - GET
        //public IActionResult Delete(int id)
        //{
        //    var patient = _patientDAL.GetPatientById(id);
        //    if (patient == null) return NotFound();
        //    return View(patient);
        //}

        //// 4️⃣ DELETE PATIENT - POST
        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeleteConfirmed(int id)
        //{
        //    _patientDAL.DeletePatient(id);
        //    return RedirectToAction("PatientHomePage");
        //}
        //[HttpPost]
        //public IActionResult SavePatient(Patient patient)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (patient.Id == 0)
        //        {
        //            // New Patient (Create)
        //            _patientDAL.AddPatient(patient);
        //        }
        //        else
        //        {
        //            // Existing Patient (Update)
        //            _patientDAL.UpdatePatient(patient);
        //        }
        //        return RedirectToAction("PatientHomePage");
        //    }
        //    return View("PatientHomePage", _patientDAL.GetAllPatients());
        //}
        #endregion
    }
    }


