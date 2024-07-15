using Google.Protobuf.WellKnownTypes;

namespace MediMax.Data.ResponseModels
{
    public class TimeDosageResponseModel
    {
        public int Id { get; set; }
        public int? Treatment_Id { get; set; }
        public string? Time  { get; set; }
    }
}
