using System.ComponentModel.DataAnnotations;

namespace MediMax.Data.Models
{
    public class Owner
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(15)]
        public string PostalCode { get; set; }

        [StringLength(25)]
        public string CpfCnpj { get; set; }

        public int IsActive { get; set; }

    }
}
