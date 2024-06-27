using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Utils;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;
using MediMax.Data.Enums;

namespace MediMax.Business.Mappers
{
    public class OwnerCreateMapper : Mapper<OwnerCreateRequestModel>, IOwnerCreateMapper
    {
        private readonly Proprietarios _owner;

        public OwnerCreateMapper()
        {
            _owner = new Proprietarios();
        }

        public Proprietarios GetOwner()
        {
            _owner.primeiro_nome = BaseMapping.FirstName;
            _owner.ultimo_nome = BaseMapping.LastName;
            _owner.email = BaseMapping.Email;
            _owner.numero_telefone = BaseMapping.PhoneNumber;
            _owner.endereco = BaseMapping.Address;
            _owner.estado = BaseMapping.State;
            _owner.cidade = BaseMapping.City;
            _owner.pais = BaseMapping.Country;
            _owner.codigo_postal = BaseMapping.PostalCode;
            _owner.cpf_cnpj = BaseMapping.CpfCnpj;
            _owner.esta_ativo = 1;
            return _owner;
        }
    }
}
