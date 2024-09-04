using MediMax.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MediMax.Data.RequestModels
{
    public class NutritionDetailDeleteRequestModel
    {
        public int Nutrition_Id { get; set; }
        public int Id { get; set; }
    }
}
