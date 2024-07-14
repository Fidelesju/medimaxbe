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

            _user.Id = BaseMapping.UserId;
            _user.Email = BaseMapping.Email;
            _user.NameUser = BaseMapping.UserName;
            _user.TypeUserId = BaseMapping.TypeUserId;
            _user.OwnerId = _user.OwnerId;
            _user.Password = _user.Password;
            _user.IsActive = _user.IsActive;
            return _user;
        }
    }
}
