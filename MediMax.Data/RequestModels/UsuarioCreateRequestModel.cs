using MediMax.Data.Enums;

namespace MediMax.Data.RequestModels
{
    public class UsuarioCreateRequestModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int TypeUserId { get; set; }
        public int IsActive { get; set; }
        public int OwnerId { get; set; }
    }
}
