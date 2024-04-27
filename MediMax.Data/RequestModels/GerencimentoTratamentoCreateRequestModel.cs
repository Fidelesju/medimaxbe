namespace MediMax.Data.RequestModels
{
    public class GerencimentoTratamentoCreateRequestModel
    {
        public int? tratamento_id { get; set; }
        public string? horario_correto_tratamento { get; set; }
        public string? horario_ingestao_medicamento { get; set; }
        public string? data_ingestao_medicamento { get; set; }
        public bool? foi_tomado { get; set; }
    }
}
