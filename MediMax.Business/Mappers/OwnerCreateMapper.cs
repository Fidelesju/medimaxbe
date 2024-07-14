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
            _owner.FirstName = BaseMapping.FirstName;
            _owner.LastName = BaseMapping.LastName;
            _owner.Email = BaseMapping.Email;
            _owner.PhoneNumber = BaseMapping.PhoneNumber;
            _owner.Address = BaseMapping.Address;
            _owner.State = BaseMapping.State;
            _owner.City = BaseMapping.City;
            _owner.Country = BaseMapping.Country;
            _owner.PostalCode = BaseMapping.PostalCode;
            _owner.CpfCnpj = BaseMapping.CpfCnpj;
            _owner.IsActive = 1;
            return _owner;
        }
    }
}
