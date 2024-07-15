namespace MediMax.Data.RequestModels
{
    public class NotificationCreateRequestModel
    {
        public string titulo { get; set; }
        public string descricao { get; set; }
        public string horario { get; set; }
        public string tipo { get; set; }
        public int Treatment_id { get; set; }
        public int remedio_id { get; set; }
    }
}
