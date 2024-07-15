using MediMax.Data.Dao;
using MediMax.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediMax.Data.Models
{
    public class TreatmentManagement
    {
        [Key]
        public int Id { get; set; }

        public string Correct_Time_Treatment { get; set; }

        public string Medication_Intake_Time { get; set; }

        public string Medication_Intake_Date { get; set; }

        public int Was_Taken { get; set; }

        public int Treatment_Id { get; set; }
        public int Treatment_User_Id { get; set; }
        public int Medication_Id { get; set; }

    }
}
