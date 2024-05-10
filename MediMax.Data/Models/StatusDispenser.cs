using MediMax.Data.Dao;
using MediMax.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MediMax.Data.Models
{
    public class StatusDispenser
    {
        [Key]
        public int id { get; set; }
        public int tratamento_id { get; set; }
        public int medicamento_id { get; set; }
        public int quantidade_total_medicamento_caixa { get; set; }
        public int quantidade_total_caixa_tratamento { get; set; }
        public int intervalo_tratamento_horas { get; set; }
        public int intervalo_tratamento_dias { get; set; }
        public int quantidade_medicamento_por_dosagem { get; set; }
        public int frenquecia_dosagem_diaria { get; set; }
        public int quantidade_total_medicamento_dosagem_dia { get; set; }
        public int quantidade_total_medicamentos_tratamento { get; set; }
        public int quantidade_medicamento_semanal { get; set; }
        public int quantidade_atual_medicamento_caixa_tratamento { get; set; }
        public int quantidade_medicamento_faltante_para_fim_tratamento { get; set; }
        public int quantidade_dias_faltante_para_fim_tratamento { get; set; }
        public string data_criacao { get; set; }
        public string data_atualizacao_semanal { get; set; }
        public string data_inicio_tratamento { get; set; }
        public string data_final_previsto_tratamento { get; set; }
        public string data_final_marcado_tratamento { get; set; }
        public int status_tratamento { get; set; }
    }
}
