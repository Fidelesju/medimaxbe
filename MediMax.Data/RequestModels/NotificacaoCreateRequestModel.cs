namespace MediMax.Data.RequestModels
{
    public class NotificacaoCreateRequestModel
    {
        public string titulo { get; set; }
        public string descricao { get; set; }
        public string horario { get; set; }
        public string tipo { get; set; }
        public int tratamento_id { get; set; }
        public int remedio_id { get; set; }
    }
}
