using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediMax.Data.Models
{
    public class Nutrition
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string NutritionType { get; set; }

        [StringLength(15)]
        public string Time { get; set; }
        public int IsActive { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("NutritionDetailId")]
        public NutritionDetail NutritionDetail { get; set; }
        public int NutritionDetailId { get; set; }
    }
}
