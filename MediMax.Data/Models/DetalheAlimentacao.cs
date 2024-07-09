using MediMax.Data.Dao;
using MediMax.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MediMax.Data.Models
{
    public class DetalheAlimentacao
    {
        [Key]
        public int id { get; set; }
        public string alimento { get; set; }
        public string quantidade { get; set; }
        public string unidade_medida{ get; set; }
    }
}
