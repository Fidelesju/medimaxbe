using Google.Protobuf.WellKnownTypes;

namespace MediMax.Data.ResponseModels
{
    public class TimeDosageResponseModel
    {
        public int Id { get; set; }
        public int? TreatmentId { get; set; }
        public int? UserId { get; set; }
        public string? TimeDosage { get; set; }
    }
}
