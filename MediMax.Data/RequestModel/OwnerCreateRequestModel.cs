﻿using MediMax.Data.Enums;
using System.Diagnostics.Metrics;

namespace MediMax.Data.RequestModels
{
    public class OwnerCreateRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public int NumberAddress{ get; set; }
        public string Cpf_Cnpj { get; set; }
    }
}
