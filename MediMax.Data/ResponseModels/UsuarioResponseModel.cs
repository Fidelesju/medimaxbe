using MediMax.Data.Enums;

namespace MediMax.Data.ResponseModels
{
    public class UserResponseModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int IsActive{ get; set; }
        public int TypeUser { get; set; }
        public int OwnerId{ get; set; }
    }
}
