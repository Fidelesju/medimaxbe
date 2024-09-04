using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Data.ResponseModels
{
    public class NutritionGetResponseModel
    {
        public string Nutrition_Type { get; set; }
        public string Time { get; set; }
        public string Title { get; set; }
        public int Id { get; set; }
        public int Is_Active { get; set; }
        public int User_Id { get; set; }

    }
}
