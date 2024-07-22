namespace MediMax.Data.ResponseModels
{
    public class SendCodeToEmailResponseModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public int UserId { get; set; }
        public int OwnerId { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
