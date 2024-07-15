using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Utils;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;
using MediMax.Data.Enums;

namespace MediMax.Business.Mappers
{
    public class OwnerCreateMapper : Mapper<OwnerCreateRequestModel>, IOwnerCreateMapper
    {
        private readonly Owner _owner;

        public OwnerCreateMapper()
        {
            _owner = new Owner();
        }

        public Owner GetOwner()
        {
            _owner.First_Name = BaseMapping.FirstName;
            _owner.Last_Name = BaseMapping.LastName;
            _owner.Email = BaseMapping.Email;
            _owner.Phone_Number = BaseMapping.PhoneNumber;
            _owner.Address = BaseMapping.Address;
            _owner.State = BaseMapping.State;
            _owner.City = BaseMapping.City;
            _owner.Country = BaseMapping.Country;
            _owner.Postal_Code = BaseMapping.PostalCode;
            _owner.Cpf_Cnpj = BaseMapping.Cpf_Cnpj;
            _owner.Is_Active = 1;
            return _owner;
        }
    }
}
