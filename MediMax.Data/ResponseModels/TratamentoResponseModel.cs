using Google.Protobuf.WellKnownTypes;

namespace MediMax.Data.ResponseModels
{
    public class TratamentoResponseModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? MedicineQuantity { get; set; }
        public string? StartTime{ get; set; }
        public int? TreatmentInterval { get; set; }
        public int? TreatmentDurationDays { get; set; }
        public string? DietaryRecommendations { get; set; }
        public string? Observation { get; set; }
        public int? IsActive { get; set; }
        public List<string> dosageTime { get; set; }
    }
}
