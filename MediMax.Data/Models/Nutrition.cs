using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediMax.Data.Models
{
    public class Nutrition
    {
        [Key]
        public int Id { get; set; }

        public string Nutrition_Type { get; set; }
        public string Title { get; set; }

        public string Time { get; set; }
        public int Is_Active { get; set; }

        public int User_Id { get; set; }

    }
}
