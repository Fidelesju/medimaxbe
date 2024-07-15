using MediMax.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediMax.Data.Models
{
    public class User
    {

        protected User()
        {
            
        }

        public User(int id, string nameUser, string password, int typeUserId, int ownerId , int isActive) { 
            Id = id;
            Name_User = nameUser;
            Password = password;
            Type_User_Id = typeUserId;
            Owner_Id = ownerId;
            Is_Active = isActive;
        }

        [Key]
        public int Id { get; set; }
        public string Name_User { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Type_User_Id { get; set; }
        public int Is_Active { get; set;  }
        public int Owner_Id { get; set; }
    }
}
