using MediMax.Data.Enums;
using System.Runtime.InteropServices;

namespace MediMax.Data.RequestModels
{
    public class HorariosDosagemCreateRequestModel
    {
        public int? tratamento_id { get; set; }
        public string? horario_dosagem { get; set; }
    }
}
