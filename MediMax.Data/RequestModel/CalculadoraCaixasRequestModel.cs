using MediMax.Data.Enums;
using System.Runtime.InteropServices;

namespace MediMax.Data.RequestModels
{
    public class DispenserStatusCreateRequestModel
    {
        public int Treatment_id { get; set; }
        public int medicamento_id { get; set; }
        public int quantidade_total_medication_caixa { get; set; }
        public int quantidade_total_caixa_Treatment { get; set; }
        public int intervalo_Treatment_horas { get; set; }
        public int intervalo_Treatment_dias { get; set; }
        public int quantidade_medication_por_dosagem { get; set; }
        public int frenquecia_dosagem_diaria { get; set; }
        public int quantidade_total_medication_dosagem_dia { get; set; }
        public int quantidade_total_medications_Treatment { get; set; }
        public int quantidade_medication_semanal { get; set; }
        public int quantidade_atual_medication_caixa_Treatment { get; set; }
        public int quantidade_medication_faltante_para_fim_Treatment { get; set; }
        public int quantidade_dias_faltante_para_fim_Treatment { get; set; }
        public string data_criacao { get; set; }
        public string data_atualizacao_semanal { get; set; }
        public string data_inicio_Treatment { get; set; }
        public string data_final_previsto_Treatment { get; set; }
        public string data_final_marcado_Treatment { get; set; }
        public int status_Treatment { get; set; }
    }
}
