using MediMax.Data.Enums;

namespace MediMax.Data.ResponseModels
{
    public class SendNotificationToEmailResponseModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public TypeNotificationEnum Type { get; set; }
        public int UserId { get; set; }
        public int OwnerId { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int IsOpen { get; set; }
    }
}
