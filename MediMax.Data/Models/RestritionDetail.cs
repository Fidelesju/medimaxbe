using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediMax.Data.Models
{
    public class RestritionDetail
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Restrition_Detail { get; set; }

        [StringLength(100)]
        public string Observation { get; set; }
    }
}
