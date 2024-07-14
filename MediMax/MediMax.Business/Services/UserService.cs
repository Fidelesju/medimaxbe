using Microsoft.EntityFrameworkCore;
using MediMax.Business.Exceptions;
using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Services.Interfaces;
using MediMax.Business.Validations;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Repositories.Interfaces;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using MediMax.Data.Models;

namespace MediMax.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserCreateMapper _userCreateMapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserDb _userDb;
        public UserService(
            IUserCreateMapper userCreateMapper,
            IUserRepository userRepository,
            IUserDb userDb) 
        {
            _userCreateMapper = userCreateMapper;
            _userRepository = userRepository;
            _userDb = userDb;
        }

        public async Task<int> CreateUsers(UserCreateRequestModel request)
        {
            User user;
            UserCreateValidation validation;
            Dictionary<string, string> errors;

            _userCreateMapper.SetBaseMapping(request);
            validation = new UserCreateValidation();
            if (!validation.IsValid(request))
            {
                errors = validation.GetErrors();
                throw new CustomValidationException(errors);
            }
            try
            {
                user = _userCreateMapper.GetUser();
                _userRepository.Create(user);
                return user.id_User;
            }
            catch (DbUpdateException exception)
            {
                errors = validation.GetPersistenceErrors(exception);
                if (errors.Count == 0)
                {
                    throw;
                }
                throw new CustomValidationException(errors);
            }
        }
        
        public async Task<UserResponseModel> GetUserById(int userId)
        {
            UserResponseModel user;
            user = await _userDb.GetUserById(userId);
            if (user == null)
            {
                throw new RecordNotFoundException();
            }
            return user;
        }

        public async Task<UserResponseModel> GetUserByEmail(string name)
        {
            UserResponseModel user;
            user = await _userDb.GetUserByEmail(name);
            if (user == null)
            {
                throw new RecordNotFoundException();
            }
            return user;
        }
    }
}
