namespace MediMax.Data.ResponseModels
{
    public class LoginResponseModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public int? TypeUserId { get; set; }

    }
}
