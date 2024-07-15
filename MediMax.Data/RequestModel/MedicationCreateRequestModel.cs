namespace MediMax.Data.RequestModels
{
    public class MedicationCreateRequestModel
    {
        public string name_medication { get; set; }
        public string expiration_date { get; set; }
        public int package_quantity { get; set; }
        public int user_id { get; set; }
        public string dosage { get; set; }
        public int is_active { get; set; }
    }
}
