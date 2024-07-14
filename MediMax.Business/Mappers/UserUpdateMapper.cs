using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Utils;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Business.Mappers
{
    public class UserUpdateMapper : Mapper<UserUpdateRequestModel>, IUserUpdateMapper
    {
        private readonly User _user;

        public UserUpdateMapper ( )
        {
            _user = new User();
        }

        public User GetUser()
        {
            HashMd5 hashMd5 = new HashMd5();

            _user.id_User = BaseMapping.UserId;
            _user.email = BaseMapping.Email;
            _user.nome = BaseMapping.UserName;
            _user.id_tipo_User = BaseMapping.TypeUserId;
            _user.id_proprietario = _user.id_proprietario;
            _user.senha = _user.senha;
            _user.esta_ativo = _user.esta_ativo;
            return _user;
        }
    }
}
