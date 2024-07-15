using System.ComponentModel.DataAnnotations;

namespace MediMax.Data.Models
{
    public class Medication
    {
        [Key]
         public int Id { get; set; }

        public string Name_Medication { get; set; }

        public string Expiration_Date { get; set; }

        public int Package_Quantity { get; set; }

        public string Dosage { get; set; }

        public int Is_Active { get; set; }

        public int User_Id { get; set; }

    }
}
