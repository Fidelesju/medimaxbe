using MediMax.Data.Enums;
using System.Runtime.InteropServices;

namespace MediMax.Data.RequestModels
{
    public class TimeDosageCreateRequestModel
    {
        public int Treatment_Id { get; set; }
        public int Treatment_User_Id { get; set; }
        public string Time { get; set; }
    }
}
