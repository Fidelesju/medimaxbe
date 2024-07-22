using MediMax.Data.Enums;
using System.Runtime.InteropServices;

namespace MediMax.Data.RequestModels
{
    public class TreatmentUpdateRequestModel
    {
        public string Name_Medication { get; set; }

        public int Medication_Quantity { get; set; }

        public string Start_Time { get; set; }

        public int Treatment_Interval_Days { get; set; }

        public int Treatment_Interval_Hours { get; set; }

        public string Dietary_Recommendations { get; set; }

        public string Observation { get; set; }

        public int Continuous_Use { get; set; }

        public int Is_Active { get; set; }

        public int Medication_Id { get; set; }

        public int User_Id { get; set; }
        public int Id { get; set; }
    }
}
