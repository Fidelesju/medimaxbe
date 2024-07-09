namespace MediMax.Data.Models
{
    public class Alimentacao
    {
        public int id { get; set; }
        public int usuarioId { get; set; }
        public string tipo_refeicao { get; set; }
        public string horario { get; set; }
        public int detalhe_alimentacao_id { get; set; }
    }
}
