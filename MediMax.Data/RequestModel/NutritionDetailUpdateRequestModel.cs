using MediMax.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MediMax.Data.RequestModels
{
    public class NutritionDetailUpdateRequestModel
    {
        public string Nutrition { get; set; }
        public int Quantity { get; set; }
        public string Unit_Measurement { get; set; }
        public int Nutrition_Id { get; set; }
        public int Id { get; set; }
    }
}
