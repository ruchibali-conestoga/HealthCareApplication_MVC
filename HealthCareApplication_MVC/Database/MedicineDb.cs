using HealthCareApplication_MVC.Models;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using HealthCareApplication_MVC.Models;


namespace HealthCareApplication_MVC.Database
{
    public class MedicineDb
    {
        private readonly string _connectionString;

        public MedicineDb(IConfiguration configuration)
        {
            // _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connectionString = "Server=ROHIT;Database=HealthCareApp;Trusted_Connection=True;TrustServerCertificate=True;";
        }
        // Add Medicine to Inventory
        public bool AddMedicine(string name, string description, DateTime expiryDate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Medicines (Name, Description, ExpiryDate) VALUES (@Name, @Description, @ExpiryDate)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@ExpiryDate", expiryDate);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // Add Medicine Inventory
        public bool AddMedicineInventory(int medicineId, int quantity)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Inventory (MedicineID, Quantity) VALUES (@MedicineID, @Quantity)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MedicineID", medicineId);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // Fetch Medicine Details
        public Medicine GetMedicineDetails(int medicineId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Medicines WHERE MedicineID = @MedicineID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MedicineID", medicineId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Medicine
                                {
                                    MedicineID = (int)reader["MedicineID"],
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    ExpiryDate = (DateTime)reader["ExpiryDate"]
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return null;
        }

        // Get Medicine Inventory
        public int GetMedicineInventory(int medicineId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT Quantity FROM Inventory WHERE MedicineID = @MedicineID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MedicineID", medicineId);
                        return (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return 0;
        }

        // Register Medicine Requested by Patient
        public bool RegisterPatientMedicine(int patientId, int medicineId, int quantity)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO PatientMedicines (PatientID, MedicineID, Quantity, PrescriptionDate) VALUES (@PatientID, @MedicineID, @Quantity, @PrescriptionDate)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@PatientID", patientId);
                        cmd.Parameters.AddWithValue("@MedicineID", medicineId);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@PrescriptionDate", DateTime.Now);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return false;
        }
    }
}
