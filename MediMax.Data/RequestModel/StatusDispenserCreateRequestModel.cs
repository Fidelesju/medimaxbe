using MediMax.Data.Enums;
using System.Runtime.InteropServices;

namespace MediMax.Data.RequestModels
{
    public class CalculadoraCaixasRequestModel
    {
        public int quantidade_total_medicamento_caixa { get; set; }
        public int intervalo_tratamento_horas { get; set; }
        public int intervalo_tratamento_dias { get; set; }
        public int quantidade_medicamento_por_dosagem { get; set; }
    }
}
