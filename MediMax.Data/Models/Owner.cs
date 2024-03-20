namespace MediMax.Data.Models
{
    public class Owner
    {
        public int ownerId { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? email { get; set; }
        public string? phoneNumber { get; set; }
        public string? address { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? country { get; set; }
        public string? postalCode { get; set; }
        public string? CpfCnpj { get; set; }
        public int? isActive { get; set; }

    }
}
