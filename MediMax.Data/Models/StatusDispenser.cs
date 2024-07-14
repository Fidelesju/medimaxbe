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

        public int TreatmentStatus { get; set; }

        public int TotalQuantityBoxTreatment { get; set; }

        public int TotalQuantityMedicationBox { get; set; }

        public int TotalQuantityMedicationDosageDay { get; set; }

        public int TotalQuantityMedicamentosTreatment { get; set; }

        public int WeeklyMedicationQuantity { get; set; }

        public int QuantityMedicinePerDosage { get; set; }

        public int MissingMedicineQuantityToEndTreatment { get; set; }

        public int QuantityDaysMissingToEndTreatment { get; set; }

        public int CurrentQuantityMedicationBoxTreatment { get; set; }

        public int TreatmentIntervalHours { get; set; }

        public int TreatmentIntervalDays { get; set; }

        public int DailyDosageFrequency { get; set; }

        [StringLength(25)]
        public string TreatmentStartDate { get; set; }

        [StringLength(25)]
        public string FinalDateExpectedTreatment { get; set; }

        [StringLength(25)]
        public string FinalDateMarkedTreatment { get; set; }

        [StringLength(25)]
        public string CreationData { get; set; }

        [StringLength(25)]
        public string WeeklyUpdateDate { get; set; }

        public int TreatmentId { get; set; }

        [ForeignKey("TreatmentId")]
        public Treatment Treatment { get; set; }

        public int MedicationId { get; set; }

        [ForeignKey("MedicationId")]
        public Medication Medication { get; set; }

        public int TreatmentUserId { get; set; }
    }
}
