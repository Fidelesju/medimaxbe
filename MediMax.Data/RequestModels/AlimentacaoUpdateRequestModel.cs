using MediMax.Data.Enums;

namespace MediMax.Data.RequestModels
{
    public class AlimentacaoUpdateRequestModel
    {
        public int id { get; set; }
        public string? tipo_refeicao { get; set; }
        public string? horario { get; set; }
        public string? alimento { get; set; }
        public double? quantidade { get; set; }
        public string? unidade_medida { get; set; }
    }
}
