using MediMax.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MediMax.Data.RequestModels
{
    public class NutritionDesativeRequestModel
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public List<NutritionDetailDeleteRequestModel> Nutrition_Detail { get; set; }
    }
}
