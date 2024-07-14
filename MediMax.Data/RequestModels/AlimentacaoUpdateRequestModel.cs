using MediMax.Data.Enums;

namespace MediMax.Data.RequestModels
{
    public class AlimentacaoUpdateRequestModel
    {
        public int id { get; set; }
        public int UserId { get; set; }
        public string tipo_refeicao { get; set; }
        public string horario { get; set; }
        public List<DetalheAlimentaoUpdateRequestModel> detalhe_alimento { get; set; }
    }

    public class DetalheAlimentaoUpdateRequestModel
    {
        public int id { get; set; }
        public string alimento { get; set; }
        public string unidade_medida { get; set; }
        public string quantidade { get; set; }
    }
}
