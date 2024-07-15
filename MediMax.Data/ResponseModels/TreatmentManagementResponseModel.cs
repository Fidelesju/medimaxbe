using Google.Protobuf.WellKnownTypes;
using MediMax.Data.Dao;

namespace MediMax.Data.ResponseModels
{
    public class TreatmentManagementResponseModel
    {
        public int Id { get; set; }
        public int Treatment_Id { get; set; }
        public int Treatment_User_Id { get; set; }
        public string Correct_Time_Treatment { get; set; }
        public string Medication_Intake_Time { get; set; }
        public string Medication_Intake_Date { get; set; }
        public int Was_Taken { get; set; }
        public int Medication_Id { get; set; }

        // Propriedade computada para retornar uma representação legível de WasTaken
        public string WasTakenDescription
        {
            get
            {
                return Was_Taken == 1 ? "Tomado" : "Não Tomado";
            }
        }
    }
}
