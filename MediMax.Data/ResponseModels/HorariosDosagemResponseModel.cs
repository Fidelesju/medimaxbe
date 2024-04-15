using Google.Protobuf.WellKnownTypes;

namespace MediMax.Data.ResponseModels
{
    public class HorariosDosagemResponseModel
    {
        public int Id { get; set; }
        public int? TratamentoId { get; set; }
        public string? HorarioDosagem  { get; set; }
    }
}
