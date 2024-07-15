using MediMax.Data.Dao;
using MediMax.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediMax.Data.Models
{
    public class TimeDosage
    {
        [Key]
        public int Id { get; set; }
        public string Time { get; set; }

        public int Treatment_Id { get; set; }
    }
}
