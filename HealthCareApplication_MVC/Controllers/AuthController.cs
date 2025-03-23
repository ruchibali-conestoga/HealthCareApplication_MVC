using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using HealthCareApplication_MVC.Database;

namespace HealthCareApplication_MVC.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly DatabaseHelper _db;

        public AuthController(IConfiguration config)
        {
            _db = new DatabaseHelper(config.GetConnectionString("DefaultConnection"));
        }

        [HttpPost]
        public IActionResult Signup(string username, string email, string password)
        {
            if (_db.RegisterUser(username, email, password))
                return Ok("User registered successfully");
            else
                return BadRequest("User already exists");
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (_db.ValidateUser(email, password))
                return Ok("Login successful");
            else
                return Unauthorized("Invalid credentials");
        }
    }
}

