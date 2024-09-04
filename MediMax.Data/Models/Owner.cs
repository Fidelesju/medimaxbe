using System.ComponentModel.DataAnnotations;

namespace MediMax.Data.Models
{
    public class Owner
    {
        [Key]
        public int Id { get; set; }

        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        public string Email { get; set; }

        public string Phone_Number { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string Postal_Code { get; set; }

        public string Cpf_Cnpj { get; set; }

        public int Is_Active { get; set; }
        public int Number_Address { get; set; }

    }
}
