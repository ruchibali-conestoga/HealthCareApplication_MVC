using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using HealthCareApplication_MVC.Models;
namespace HealthCareApplication_MVC.Database
{
    public class PatientDb
    {
        private readonly string _connectionString;

        public PatientDb(IConfiguration configuration)
        {
           // _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connectionString = "Server=ROHIT;Database=HealthCareApp;Trusted_Connection=True;TrustServerCertificate=True;";
        }
        // 1️⃣ GET ALL PATIENTS
        public List<Patient> GetAllPatients()
        {
            List<Patient> patients = new List<Patient>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Patients", con);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    patients.Add(new Patient
                    {
                        Id = (int)rdr["Id"],
                        Name = rdr["Name"].ToString(),
                        Age = (int)rdr["Age"],
                        Gender = rdr["Gender"].ToString(),
                        Contact = rdr["Contact"].ToString(),
                        Address = rdr["Address"].ToString()
                    });
                }
            }
            return patients;
        }

        // 2️⃣ ADD NEW PATIENT
        public void AddPatient(Patient patient)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Patients (Name, Age, Gender, Contact, Address) VALUES (@Name, @Age, @Gender, @Contact, @Address)", con);
                cmd.Parameters.AddWithValue("@Name", patient.Name);
                cmd.Parameters.AddWithValue("@Age", patient.Age);
                cmd.Parameters.AddWithValue("@Gender", patient.Gender);
                cmd.Parameters.AddWithValue("@Contact", patient.Contact);
                cmd.Parameters.AddWithValue("@Address", patient.Address);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // 3️⃣ GET PATIENT BY ID
        public Patient GetPatientById(int id)
        {
            Patient patient = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Patients WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    patient = new Patient
                    {
                        Id = (int)rdr["Id"],
                        Name = rdr["Name"].ToString(),
                        Age = (int)rdr["Age"],
                        Gender = rdr["Gender"].ToString(),
                        Contact = rdr["Contact"].ToString(),
                        Address = rdr["Address"].ToString()
                    };
                }
            }
            return patient;
        }

        // 4️⃣ UPDATE PATIENT
        public void UpdatePatient(Patient patient)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Patients SET Name=@Name, Age=@Age, Gender=@Gender, Contact=@Contact, Address=@Address WHERE Id=@Id", con);
                cmd.Parameters.AddWithValue("@Id", patient.Id);
                cmd.Parameters.AddWithValue("@Name", patient.Name);
                cmd.Parameters.AddWithValue("@Age", patient.Age);
                cmd.Parameters.AddWithValue("@Gender", patient.Gender);
                cmd.Parameters.AddWithValue("@Contact", patient.Contact);
                cmd.Parameters.AddWithValue("@Address", patient.Address);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // 5️⃣ DELETE PATIENT
        public void DeletePatient(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Patients WHERE Id=@Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
