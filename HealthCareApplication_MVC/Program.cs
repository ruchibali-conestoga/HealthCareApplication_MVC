using System;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HealthCareApplication_MVC.Database;

class Program
{
    static void Main(string[] args)
    {       
      
        string connectionString = "Server=ROHIT;Database=HealthCareApp;Trusted_Connection=True;TrustServerCertificate=True;";
        DatabaseHelper db = new DatabaseHelper(connectionString);
        #region
        //Console.Write("Enter Username: ");
        //string username = Console.ReadLine();

        //Console.Write("Enter Email: ");
        //string email = Console.ReadLine();

        //Console.Write("Enter Password: ");
        //string password = Console.ReadLine();

        //// Register User
        //if (db.RegisterUser(username, email, password))
        //{
        //    Console.WriteLine("✅ User registered successfully!");
        //}
        //else
        //{
        //    Console.WriteLine("❌ Registration failed.");
        //}

        //// Login User
        //Console.Write("\nEnter Email for Login: ");
        //string loginEmail = Console.ReadLine();

        //Console.Write("Enter Password for Login: ");
        //string loginPassword = Console.ReadLine();

        //if (db.ValidateUser(loginEmail, loginPassword))
        //{
        //    Console.WriteLine("✅ Login successful!");
        //}
        //else
        //{
        //    Console.WriteLine("❌ Invalid email or password.");
        //}
        #endregion

        try
        {
            // ✅ 1. CHECK DATABASE CONNECTION
            CheckDatabaseConnection();

            // ✅ 2. START ASP.NET CORE WEB APPLICATION
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            // Default route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            Console.WriteLine("🚀 Web application started successfully.");
            app.Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Fatal error in application startup: " + ex.Message);
        }
    }


    #region

    #endregion

    #region DatabaseConnection
    static void CheckDatabaseConnection()
    {
        string connectionString = "Server=ROHIT;Database=HealthCareApp;Trusted_Connection=True;TrustServerCertificate=True;";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("✅ Database Connection is OPEN.");
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("❌ Database Connection FAILED: SQL Error - " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Database Connection FAILED: " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine("🔴 Database Connection is now CLOSED.");
                }
            }
        }
    }
    #endregion
}



