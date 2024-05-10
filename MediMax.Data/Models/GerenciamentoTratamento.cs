using MediMax.Data.Dao;
using MediMax.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MediMax.Data.Models
{
    public class GerenciamentoTratamento
    {
        [Key]
        public int id { get; set; }
        public int? tratamento_id { get; set; }
        public string? horario_correto_tratamento { get; set; }
        public string? horario_ingestao_medicamento { get; set; }
        public string? data_ingestao_medicamento { get; set; }
        public bool? foi_tomado { get; set; }
    }
}
