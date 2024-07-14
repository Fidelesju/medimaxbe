using MediMax.Data.Dao;
using MediMax.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MediMax.Data.Models
{
    public class Treatment
    {
        [Key]
        public int id { get; set; }
        public string nome_medication { get; set; }
        public int? quantidade_medications { get; set; }
        public string? horario_inicio { get; set; }
        public int? intervalo_Treatment { get; set; }
        public int? tempo_Treatment_dias { get; set; }
        public string? recomendacoes_alimentacao { get; set; }
        public string? observacao { get; set; }
        public int? esta_ativo { get; set; }

    }
}
