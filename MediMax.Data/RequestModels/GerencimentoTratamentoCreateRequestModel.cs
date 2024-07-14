namespace MediMax.Data.RequestModels
{
    public class GerencimentoTreatmentCreateRequestModel
    {
        public int? Treatment_id { get; set; }
        public string? horario_correto_Treatment { get; set; }
        public string? horario_ingestao_medication { get; set; }
        public string? data_ingestao_medication { get; set; }
        public bool? foi_tomado { get; set; }
    }
}
