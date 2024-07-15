using MediMax.Data.Enums;

namespace MediMax.Data.RequestModels
{
    public class NutritionCreateRequestModel
    {
        public int UserId{ get; set; }
        public string tipo_refeicao { get; set; }
        public string horario { get; set; }
        public List<DetalheAlimentacaoCreateRequestModel> detalhe_alimentacao{ get; set; }
        public int detalhe_alimentacao_id { get; set; }
    }
}
