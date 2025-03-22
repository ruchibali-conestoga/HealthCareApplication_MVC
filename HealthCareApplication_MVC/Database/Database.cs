using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace HealthCareApplication_MVC.Database
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Register User (Without Hashing)
        public bool RegisterUser(string username, string email, string password)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string query = "INSERT INTO Users (Username, Email, Password) VALUES (@Username, @Email, @Password)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Username", username));
                        cmd.Parameters.Add(new SqlParameter("@Email", email));
                        cmd.Parameters.Add(new SqlParameter("@Password", password));

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Unique constraint violation
                {
                    Console.WriteLine("❌ User with this email already exists.");
                    return false;
                }
                Console.WriteLine($"❌ SQL Error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Unexpected Error: {ex.Message}");
                return false;
            }
        }

        // Validate User Login (Without Hashing)
        public bool ValidateUser(string email, string password)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email AND Password = @Password";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Email", email));
                        cmd.Parameters.Add(new SqlParameter("@Password", password));

                        int userExists = (int)cmd.ExecuteScalar();
                        return userExists > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"❌ SQL Error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Unexpected Error: {ex.Message}");
                return false;
            }
        }
    }
}
