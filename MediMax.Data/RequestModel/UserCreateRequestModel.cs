using MediatR;
using MediMax.Data.Enums;

namespace MediMax.Data.RequestModels
{
    public class UserCreateRequestModel
    {
        public string Name_User { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Type_User_Id { get; set; }
        public int Owner_Id { get; set; }
        public int Is_Active { get; set; }
    }
}
