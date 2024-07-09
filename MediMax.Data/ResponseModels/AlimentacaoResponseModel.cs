using MediMax.Data.Models;

namespace MediMax.Data.ResponseModels
{
    public class AlimentacaoResponseModel
    {
        public int id { get; set; }
        public int usuarioId { get; set; }
        public string tipo_refeicao { get; set; }
        public string horario { get; set; }
        public DetalheAlimentacao detalhe_alimentacao_id { get; set; }

    }
}
