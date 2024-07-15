namespace MediMax.Data.ResponseModels
{
    public class MedicationResponseModel
    {
        public int id { get; set; }
        public string medicine_name { get; set; }
        public string expiration_date { get; set; }
        public int package_quantity { get; set; }
        public int user_id { get; set; }
        public int is_active { get; set; }
        public string dosage { get; set; }
    }
}
