using MediMax.Data.Dao;
using MediMax.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediMax.Data.Models
{
    public class StatusDispenser
    {
        [Key]
        public int Id { get; set; }

        public int Treatment_Status { get; set; }

        public int Total_Quantity_Box_Treatment { get; set; }

        public int Total_Quantity_Medication_Box { get; set; }

        public int Total_Quantity_Medication_Dosage_Day { get; set; }

        public int Total_Quantity_Medicamentos_Treatment { get; set; }

        public int Weekly_Medication_Quantity { get; set; }

        public int Quantity_Medicine_Per_Dosage { get; set; }

        public int Missing_Medicine_Quantity_To_End_Treatment { get; set; }

        public int Quantity_Days_Missing_To_End_Treatment { get; set; }

        public int Current_Quantity_Medication_Box_Treatment { get; set; }

        public int Treatment_Interval_Hours { get; set; }

        public int Treatment_Interval_Days { get; set; }

        public int Daily_Dosage_Frequency { get; set; }

        public string Treatment_Start_Date { get; set; }

        public string Final_Date_Expected_Treatment { get; set; }

        public string Final_Date_Marked_Treatment { get; set; }

        public string Creation_Data { get; set; }

        public string Weekly_Update_Date { get; set; }

        public int Treatment_Id { get; set; }

        public int Medication_Id { get; set; }

        public int Treatment_User_Id { get; set; }
    }
}
