using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediMax.Data.Models
{
    public class Medication
    {
        [Key]
         public int Id { get; set; }

        [StringLength(50)]
        public string NameMedication { get; set; }

        [StringLength(15)]
        public string ExpirationDate { get; set; }

        public int PackageQuantity { get; set; }

        [StringLength(5)]
        public string Dosage { get; set; }

        public int IsActive { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
