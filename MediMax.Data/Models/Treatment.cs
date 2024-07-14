using MediMax.Data.Dao;
using MediMax.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediMax.Data.Models
{
    public class Treatment
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string NameMedication { get; set; }

        public int MedicationQuantity { get; set; }

        [StringLength(15)]
        public string StartTime { get; set; }

        public int TreatmentIntervalDays { get; set; }

        public int TreatmentIntervalHours { get; set; }

        [StringLength(50)]
        public string DietaryRecommendations { get; set; }

        [StringLength(100)]
        public string Observation { get; set; }

        public int ContinuousUse { get; set; }

        public int IsActive { get; set; }

        public int MedicationId { get; set; }

        [ForeignKey("MedicationId")]
        public Medication Medication { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }

}
