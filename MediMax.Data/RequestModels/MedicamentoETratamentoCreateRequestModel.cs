using MediMax.Data.Enums;
using System.Runtime.InteropServices;

namespace MediMax.Data.RequestModels
{
    public class MedicamentoETratamentoCreateRequestModel
    {
        public string nome { get; set; }
        public string data_vencimento_medicamento { get; set; }
        public int quantidade_embalagem { get; set; }
        public int usuarioId { get; set; }
        public float dosagem { get; set; }
        public int quantidade_medicamento_dosagem { get; set; }
        public string horario_inicial_tratamento { get; set; }
        public int intervalo_tratamento_horas { get; set; }
        public int intervalo_tratamento_dias { get; set; }
        public string recomendacoes_alimentacao { get; set; }
        public string? observacao { get; set; }
    }
}
