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

        [StringLength(50)]
        public string CorrectTimeTreatment { get; set; }

        [StringLength(50)]
        public string MedicationIntakeTime { get; set; }

        [StringLength(50)]
        public string MedicationIntakeDate { get; set; }

        public int WasTaken { get; set; }

        public int TreatmentId { get; set; }

        [ForeignKey("TreatmentId")]
        public Treatment Treatment { get; set; }

        public int TreatmentUserId { get; set; }

        public int MedicationId { get; set; }

        [ForeignKey("MedicationId")]
        public Medication Medication { get; set; }
    }
}
