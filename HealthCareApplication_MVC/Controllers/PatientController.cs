using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareApplication_MVC.Database;
using Microsoft.Data.SqlClient;
using System;
using HealthCareApplication_MVC.Models;
using Newtonsoft.Json;

namespace HealthCareApplication_MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
        //private readonly PatientDb _patientDAL;

        //public PatientsController(IConfiguration configuration)
        //{
        //    _patientDAL = new PatientDb(configuration);
        //}

        //[Route("/Account")]
        //public ActionResult Index()
        //{
        //    return View(_patientDAL.GetAllPatients());
        //}

        //[Route("/Account/SavePatient")]
        //public ActionResult SavePatient()
        //{
        //    return View();
        //}

        //[Route("/Account/SavePatient")]
        //[HttpPost("SavePatient")]
        //public ActionResult SavePatient([FromBody] Patient patient)
        //{
        //    if (patient == null || string.IsNullOrEmpty(patient.Name))
        //    {
        //        return BadRequest("Invalid patient data.");
        //    }

        //    _patientDAL.AddPatient(patient);
        //    //return Ok(new { message = "Patient saved successfully!" });
        //    return View();
        //}



        private readonly PatientDb _patientDAL;

            public PatientController(IConfiguration configuration)
            {
                _patientDAL = new PatientDb(configuration);
            }

            public ActionResult Index()
            {
                return View(_patientDAL.GetAllPatients());
            }

        [HttpPost("SavePatient")]
        public IActionResult SavePatient([FromBody] Patient patient)
        {
            if (patient == null)
            {
                return BadRequest("Patient object is null. Ensure the request is correctly formatted.");
            }

            if (string.IsNullOrEmpty(patient.Name))
            {
                return BadRequest("Invalid patient data: Name is required.");
            }

            // Debugging log
            Console.WriteLine($"Received Patient: {JsonConvert.SerializeObject(patient)}");

            if (patient.Id == 0)
            {
                _patientDAL.AddPatient(patient);
            }
            else
            {
                _patientDAL.UpdatePatient(patient);
            }

            //return Ok(new { message = "Patient saved successfully!" });
            //return View();
            return View("Dashboard");
        }


        [HttpGet]
            public ActionResult Delete(int id)
            {
                _patientDAL.DeletePatient(id);
                return RedirectToAction("Index");
            }
        }
    }


#region 
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


