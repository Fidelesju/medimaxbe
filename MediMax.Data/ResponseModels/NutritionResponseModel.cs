using MediMax.Data.Models;

namespace MediMax.Data.ResponseModels
{
    public class NutritionResponseModel
    {
        public int id { get; set; }
        public int UserId { get; set; }
        public string tipo_refeicao { get; set; }
        public string horario { get; set; }
        public NutritionDetail detalhe_alimentacao_id { get; set; }

    }
}
