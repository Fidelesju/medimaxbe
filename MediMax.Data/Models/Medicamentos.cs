using System.ComponentModel.DataAnnotations;

namespace MediMax.Data.Models
{
    public class Medicamentos
    {
        [Key]
        public int id { get; set; }
        public int usuarioId { get; set; }
        public string?nome { get; set; }
        public int quantidade_embalagem { get; set; }
        public float dosagem { get; set; }
        public string data_vencimento { get; set; }
        public int esta_ativo { get; set; }

    }
}
