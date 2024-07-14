using MediMax.Data.Dao;
using MediMax.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediMax.Data.Models
{
    public class TimeDosage
    {
        [Key]
        public int Id { get; set; }

        [StringLength(15)]
        public string Time { get; set; }

        public int TreatmentId { get; set; }

        [ForeignKey("TreatmentId")]
        public Treatment Treatment { get; set; }
    }
}
