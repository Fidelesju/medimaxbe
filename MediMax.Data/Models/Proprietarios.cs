using System.ComponentModel.DataAnnotations;

namespace MediMax.Data.Models
{
    public class Proprietarios
    {
        [Key]
        public int id_proprietario { get; set; }
        public string primeiro_nome { get; set; }
        public string ultimo_nome { get; set; }
        public string email { get; set; }
        public string numero_telefone { get; set; }
        public string endereco { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string codigo_postal { get; set; }
        public string pais { get; set; }
        public string cpf_cnpj { get; set; }
        public int esta_ativo { get; set; }
      
    }
}
