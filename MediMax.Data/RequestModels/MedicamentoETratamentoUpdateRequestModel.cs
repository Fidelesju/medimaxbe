using MediMax.Data.Enums;
using System.Runtime.InteropServices;

namespace MediMax.Data.RequestModels
{
    public class MedicationUpdateRequestModel
    {
        public int medication_id { get; set; }
        public string medicine_name { get; set; }
        public string expiration_date { get; set; }
        public int package_quantity { get; set; }
        public int user_id { get; set; }
        public float dosage { get; set; }
    }
}
