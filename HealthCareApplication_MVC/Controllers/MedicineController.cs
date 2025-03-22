using HealthCareApplication_MVC.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HealthCareApplication_MVC.Controllers
{
    public class MedicineController : Controller
    {
        private readonly DatabaseHelper _dbHelper;

        public MedicineController()
        {
            _dbHelper = new DatabaseHelper(configuration);           
        }

        // Add New Medicine
        [HttpPost]
        public IActionResult AddMedicine(string name, string description, DateTime expiryDate)
        {
            if (_dbHelper.AddMedicine(name, description, expiryDate))
                return RedirectToAction("Index");
            else
                return View("Error");
        }

        // Add Medicine to Inventory
        [HttpPost]
        public IActionResult AddMedicineInventory(int medicineId, int quantity)
        {
            if (_dbHelper.AddMedicineInventory(medicineId, quantity))
                return RedirectToAction("Inventory");
            else
                return View("Error");
        }

        // Get Medicine Details
        public IActionResult MedicineDetails(int id)
        {
            var medicine = _dbHelper.GetMedicineDetails(id);
            if (medicine != null)
                return View(medicine);
            else
                return View("Error");
        }

        // Get Inventory Details for Medicine
        public IActionResult Inventory(int id)
        {
            int inventory = _dbHelper.GetMedicineInventory(id);
            ViewBag.Inventory = inventory;
            return View();
        }

        // Patient Request Medicine
        [HttpPost]
        public IActionResult RequestMedicine(int patientId, int medicineId, int quantity)
        {
            if (_dbHelper.RegisterPatientMedicine(patientId, medicineId, quantity))
                return RedirectToAction("PatientHomePage");
            else
                return View("Error");
        }
    }
}
