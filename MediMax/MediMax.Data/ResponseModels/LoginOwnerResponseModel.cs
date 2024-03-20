namespace MediMax.Data.ResponseModels
{
    public class LoginOwnerResponseModel
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        private int? _typeUserId;
        public int? TypeUserId
        {
            get { return _typeUserId; }
            set { _typeUserId = value; }
        }
    }
}
