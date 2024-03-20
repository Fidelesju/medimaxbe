using MediMax.Data.Dao;
using MediMax.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MediMax.Data.Models
{
    public class Tratamento
    {
        [Key]
        public int id { get; set; }
        public string nome_medicamento { get; set; }
        public int? quantidade_medicamentos { get; set; }
        public string? horario_inicio { get; set; }
        public int? intervalo_tratamento { get; set; }
        public int? tempo_tratamento_dias { get; set; }
        public string? recomendacoes_alimentacao { get; set; }
        public string? observacao { get; set; }
        public int? esta_ativo { get; set; }

    }
}
