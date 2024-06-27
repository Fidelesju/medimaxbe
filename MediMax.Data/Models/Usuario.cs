using MediMax.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MediMax.Data.Models
{
    public class Usuario
    {
        [Key]
        public int id_usuario { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public int id_tipo_usuario { get; set; }
        public int esta_ativo { get; set;  }
        public int id_proprietario { get; set;  }
    }
}
