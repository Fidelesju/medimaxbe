using MediMax.Data.Dao;
using MediMax.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MediMax.Data.Models
{
    public class TreatmentManagement
    {
        [Key]
        public int id { get; set; }
        public int? Treatment_id { get; set; }
        public string? horario_correto_Treatment { get; set; }
        public string? horario_ingestao_medication { get; set; }
        public string? data_ingestao_medication { get; set; }
        public bool? foi_tomado { get; set; }
    }
}
