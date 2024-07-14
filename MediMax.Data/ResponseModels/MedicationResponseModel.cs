namespace MediMax.Data.ResponseModels
{
    public class MedicationResponseModel
    {
        public int Id { get; set; }
        public int UserId{ get; set; }
        public string Name { get; set; }
        public int PackageQuantity { get; set; }
        public double Dosage{ get; set; }
        public string ExpirationDate { get; set; }
        public int IsActive{ get; set; }

    }
}
