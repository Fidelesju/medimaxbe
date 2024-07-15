using MediMax.Data.Enums;

namespace MediMax.Data.ResponseModels
{
    public class UserResponseModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name_User { get; set; }
        public int Is_Active { get; set; }
        public int Type_User_Id { get; set; }
        public int Owner_Id { get; set; }
    }
}
