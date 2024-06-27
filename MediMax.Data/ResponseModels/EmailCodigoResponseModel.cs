using MediMax.Data.Enums;

namespace MediMax.Data.ResponseModels
{
    public class EmailCodigoResponseModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
