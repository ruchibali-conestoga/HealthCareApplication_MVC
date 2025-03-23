using HealthCareApplication_MVC.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;

namespace HealthCareApplication_MVC.Controllers
{
    public class AccountController : Controller
    {        
        private readonly string connectionString = "Server=ROHIT;Database=HealthCareApp;Trusted_Connection=True;TrustServerCertificate=True;";

        public IActionResult Login() => View();
        public IActionResult Register() => View();
        public IActionResult patient()
        {
            return View();  
        }
        public IActionResult appointment()
        {
            return View();
        }
        public IActionResult medicine()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            return View();  
        }

        [HttpPost]
        public IActionResult Register(string Username, string Email, string Password)
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ViewBag.Error = "All fields are required!";
                return View();
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Users (Username, Email, Password) VALUES (@Username, @Email, @Password)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", Username);
                        cmd.Parameters.AddWithValue("@Email", Email);
                        cmd.Parameters.AddWithValue("@Password", Password);

                        cmd.ExecuteNonQuery();
                    }
                }
                TempData["SuccessMessage"] = "Registration successful! Please log in.";
                return RedirectToAction("Login");//return RedirectToAction("PatientHomePage", "Account");
            }
            catch (SqlException ex)
            {
                // Handle database-related errors
                ViewBag.Error = $"Database error: {ex.Message}";
                return View();
            }
            catch (Exception ex)
            {
                // Handle any other errors
                ViewBag.Error = $"An error occurred: {ex.Message}";
                return View();
            }
        }

        [HttpPost]  
        public IActionResult Login(string Email, string Password)
        {
            Console.WriteLine("🔍 Login method called!"); // Debugging


            DatabaseHelper dbHelper = new DatabaseHelper("Server=ROHIT;Database=HealthCareApp;Trusted_Connection=True;TrustServerCertificate=True;");
            //DatabaseHelper dbHelper = new DatabaseHelper("Server=ROHIT;Database=HealthCareApp;Integrated Security=True;");
            //private readonly string _connectionString = "Server=ROHIT;Database=HealthCareApp;Trusted_Connection=True;TrustServerCertificate=True;";

            bool isValid = dbHelper.ValidateUser(Email, Password);

            if (isValid)
            {
                Console.WriteLine("✅ Login Successful!");
                //return RedirectToAction("Dashboard");
                return RedirectToAction("patient");
            }
            else
            {
                Console.WriteLine("❌ Invalid Credentials.");
                ViewBag.ErrorMessage = "Invalid email or password.";
                return View();
            }
        }
    }
}
