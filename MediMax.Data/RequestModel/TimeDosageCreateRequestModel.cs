using MediMax.Data.Enums;
using System.Runtime.InteropServices;

namespace MediMax.Data.RequestModels
{
    public class TimeDosageCreateRequestModel
    {
        public int treatment_id { get; set; }
        public string time { get; set; }
    }
}
