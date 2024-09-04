using MediMax.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MediMax.Data.RequestModels
{
    public class NutritionUpdateRequestModel
    {
        public string Nutrition_Type { get; set; }
        public string Time { get; set; }
        public string Title { get; set; }
        public int Id { get; set; }
        public int Is_Active { get; set; }
        public int User_Id { get; set; }
        public List<NutritionDetailUpdateRequestModel> Nutrition_Detail { get; set; }
    }
}
