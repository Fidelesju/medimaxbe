using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Utils;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;
using MediMax.Data.Enums;

namespace MediMax.Business.Mappers
{
    public class OwnerUpdateMapper : Mapper<OwnerUpdateRequestModel>, IOwnerUpdateMapper
    {
        private readonly Owner? _owner;

        public OwnerUpdateMapper()
        {
            _owner = new Owner();
        }

        public Owner GetOwner()
        {
            _owner.firstName = BaseMapping.FirstName;
            _owner.lastName = BaseMapping.LastName;
            _owner.email = BaseMapping.Email;
            _owner.phoneNumber = BaseMapping.PhoneNumber;
            _owner.address = BaseMapping.Address;
            _owner.state = BaseMapping.State;
            _owner.city = BaseMapping.City;
            _owner.country = BaseMapping.Country;
            _owner.postalCode = BaseMapping.PostalCode;
            _owner.CpfCnpj = BaseMapping.CpfCnpj;
            _owner.isActive = 1;
            return _owner;
        }
    }
}
