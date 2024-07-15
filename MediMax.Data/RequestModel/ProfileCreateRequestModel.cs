namespace MediMax.Data.RequestModels
{
    public class ProfileCreateRequestModel
    {
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public int[] Permissions { get; set; }
    }
}
