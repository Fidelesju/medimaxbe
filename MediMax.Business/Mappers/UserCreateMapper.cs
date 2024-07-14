using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Utils;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Mappers
{
    public class UserCreateMapper : Mapper<UserCreateRequestModel>, IUserCreateMapper
    {
        private readonly User? _user;

        public UserCreateMapper()
        {
            _user = new User();
        }

        public User GetUser()
        {
            HashMd5 hashMd5 = new HashMd5();

            _user.email = BaseMapping.Email;
            _user.senha = hashMd5.EncryptMD5(BaseMapping.Password);
            _user.nome = BaseMapping.UserName;
            _user.esta_ativo = 1;
            _user.id_tipo_User = BaseMapping.TypeUserId;
            _user.id_proprietario = BaseMapping.OwnerId;
            return _user;
        }
    }
}
