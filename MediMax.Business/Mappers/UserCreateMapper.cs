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

            _user.Email = BaseMapping.Email;
            _user.Password = hashMd5.EncryptMD5(BaseMapping.Password);
            _user.NameUser = BaseMapping.UserName;
            _user.IsActive = 1;
            _user.TypeUserId = BaseMapping.TypeUserId;
            _user.OwnerId = BaseMapping.OwnerId;
            return _user;
        }
    }
}
