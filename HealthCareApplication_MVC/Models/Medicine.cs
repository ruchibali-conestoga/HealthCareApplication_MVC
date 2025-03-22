namespace HealthCareApplication_MVC.Models
{
    public class Medicine
    {
        public int MedicineID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ExpiryDate { get; set; }
    }

    public class MedicineInventory
    {
        public int InventoryID { get; set; }
        public int MedicineID { get; set; }
        public int Quantity { get; set; }
    }

    public class PatientMedicine
    {
        public int PatientMedicineID { get; set; }
        public int PatientID { get; set; }
        public int MedicineID { get; set; }
        public int Quantity { get; set; }
        public DateTime PrescriptionDate { get; set; }
    }
}
