namespace MediMax.Data.RequestModels
{
    public class UsuarioCreateRequestModel
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int typeUserId{ get; set; }

    }
}
