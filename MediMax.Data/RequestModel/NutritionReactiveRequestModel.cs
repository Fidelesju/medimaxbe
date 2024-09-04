using MediMax.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MediMax.Data.RequestModels
{
    public class NutritionReactiveRequestModel
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public List<NutritionDetailCreateRequestModel> Nutrition_Detail { get; set; }
    }
}
