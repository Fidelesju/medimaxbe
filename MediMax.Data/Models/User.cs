using MediMax.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediMax.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string NameUser { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(25)]
        public string Password { get; set; }
        public int TypeUserId { get; set; }
        public int IsActive { get; set;  }
        public int OwnerId { get; set;  }

        [ForeignKey("OwnerId")]
        public Owner Owner { get; set; }
    }
}
