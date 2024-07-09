using Google.Protobuf.WellKnownTypes;
using MediMax.Data.Dao;

namespace MediMax.Data.ResponseModels
{
    public class HistoricoResponseModel
    {
        public int Id { get; set; }
        public int TreatmentId { get; set; }
        public int UserId { get; set; }
        public string? CorrectTreatmentSchedule { get; set; }
        public string? MedicationIntakeSchedule { get; set; }
        public string? DateMedicationIntake { get; set; }
        public int WasTaken { get; set; }
        public string MedicineName{ get; set; }

        // Propriedade computada para retornar uma representação legível de WasTaken
        public string WasTakenDescription
        {
            get
            {
                return WasTaken == 1 ? "Tomado" : "Não Tomado";
            }
        }
    }
}
