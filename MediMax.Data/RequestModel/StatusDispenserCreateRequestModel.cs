using MediMax.Data.Enums;
using System.Runtime.InteropServices;

namespace MediMax.Data.RequestModels
{
    public class CalculadoraCaixasRequestModel
    {
        public int quantidade_total_medication_caixa { get; set; }
        public int intervalo_Treatment_horas { get; set; }
        public int intervalo_Treatment_dias { get; set; }
        public int quantidade_medication_por_dosagem { get; set; }
    }
}
