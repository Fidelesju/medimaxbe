using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Data.ResponseModels
{
    public class NutritionDetailResponseModel
    {
        public int Id { get; set; }
        public int Nutrition_Id { get; set; }

        public string Nutrition { get; set; }

        public int Quantity { get; set; }

        public string Unit_Measurement { get; set; }

    }
}
