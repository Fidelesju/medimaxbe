using MediMax.Data.Enums;
using System.Runtime.InteropServices;

namespace MediMax.Data.RequestModels
{
    public class MedicamentoETreatmentCreateRequestModel
    {
        public string? nome { get; set; }
        public string? data_vencimento_medication { get; set; }
        public int? quantidade_embalagem { get; set; }
        public float? dosagem { get; set; }
        public int? quantidade_medication_por_dia { get; set; }
        public string? horario_inicial_Treatment { get; set; }
        public int? intervalo_Treatment_horas { get; set; }
        public int? intervalo_Treatment_dias { get; set; }
        public string? recomendacoes_alimentacao { get; set; }
        public string? observacao { get; set; }

    }
}
