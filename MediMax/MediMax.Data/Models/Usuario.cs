using System.ComponentModel.DataAnnotations;

namespace MediMax.Data.Models
{
    public class User
    {
        [Key]
        public int id_User { get; set; }
        public string? nome { get; set; }
        public string? email { get; set; }
        public string? senha { get; set; }
        public int? id_tipo_User { get; set; }
        public int? esta_ativo { get; set;  }
    }
}
