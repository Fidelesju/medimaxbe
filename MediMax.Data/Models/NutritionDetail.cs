using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediMax.Data.Models
{
    public class NutritionDetail
    {
        [Key]
        public int Id { get; set; }
        public int Nutrition_Id { get; set; }

        public string Nutrition { get; set; }

        public int Quantity { get; set; }

        public string Unit_Measurement { get; set; }
    }
}
