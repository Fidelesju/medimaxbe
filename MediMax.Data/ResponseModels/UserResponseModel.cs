namespace MediMax.Data.ResponseModels
{
    public class UserResponseModel
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public int? IsActive{ get; set; }
    }
}
