namespace MediMax.Data.ResponseModels
{
    public class MedicineResponseModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? PackageQuantity { get; set; }
        public double? Dosage{ get; set; }
        public string? ExpirationDate { get; set; }

    }
}
