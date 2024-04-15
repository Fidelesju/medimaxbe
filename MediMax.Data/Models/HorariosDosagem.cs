using MediMax.Data.Dao;
using MediMax.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MediMax.Data.Models
{
    public class HorariosDosagem
    {
        [Key]
        public int id { get; set; }
        public int? tratamento_id { get; set; }
        public string? horario_dosagem { get; set; }
    }
}
