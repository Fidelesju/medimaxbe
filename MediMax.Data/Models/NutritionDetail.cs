using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediMax.Data.Models
{
    public class NutritionDetail
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Nutrition { get; set; }

        public int Quantity { get; set; }

        [StringLength(25)]
        public string UnitMeasurement { get; set; }
    }
}
