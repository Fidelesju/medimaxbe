namespace MediMax.Data.ResponseModels
{
    public class TreatmentResponseModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? MedicineQuantity { get; set; }
        public int? MedicineId{ get; set; }
        public string? StartTime{ get; set; }
        public int? TreatmentInterval { get; set; }
        public int? TreatmentDurationDays { get; set; }
        public string? DietaryRecommendations { get; set; }
        public string? Observation { get; set; }
        public int? IsActive { get; set; }
    }
}
