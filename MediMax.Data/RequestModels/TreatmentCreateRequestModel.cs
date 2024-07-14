using MediMax.Data.Enums;
using System.Runtime.InteropServices;

namespace MediMax.Data.RequestModels
{
    public class TreatmentCreateRequestModel
    {
        public int medicine_id { get; set; }
        public string medicine_name { get; set; }
        public int medication_quantity { get; set; }
        public string treatment_start_time { get; set; }
        public int treatment_interval_hours { get; set; }
        public int treatment_interval_days { get; set; }
        public string dietary_recommendations { get; set; }
        public string observation { get; set; }
    }
}
