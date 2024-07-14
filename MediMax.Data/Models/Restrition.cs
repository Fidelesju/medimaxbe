using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediMax.Data.Models
{
    public class Restrition
    {
        [Key]
        public int Id { get; set; }

        public int IsActive { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public int RestritionDetailId { get; set; }

        [ForeignKey("RestritionDetailId")]
        public RestritionDetail RestritionDetail { get; set; }
    }
}
