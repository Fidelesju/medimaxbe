using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Data;
using MediMax.Business.CoreServices.Interfaces;
using MediMax.Business.Exceptions;
using MediMax.Business.Services.Interfaces;
using MediMax.Business.Utils;
using MediMax.Business.Validations;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using MediMax.Data.Dao;

namespace MediMax.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAuthDb _authDb;
        private readonly IAdminAuthDb _adminAuthDb;
        private readonly IOwnerAuthDb _ownerAuthDb;
        private readonly IJwtService _jwtService;
        public AccountService(
            IAuthDb authDb,
            IJwtService jwtService,
            IAdminAuthDb adminAuthDb,
            IOwnerAuthDb ownerAuthDb) 
        {
            _authDb = authDb;
            _jwtService = jwtService;
            _adminAuthDb = adminAuthDb;
            _ownerAuthDb = ownerAuthDb;
        }

        public async Task<LoginResponseModel> AuthenticateUser(LoginRequestModel loginRequest)
        {
            HashMd5 hashMd5;
            LoginResponseModel loginResponse;
            hashMd5 = new HashMd5();
            string role;
            string encryptPassword = hashMd5.EncryptMD5(loginRequest.Password);

            loginResponse = await _authDb.AuthenticateUser(loginRequest.Email, loginRequest.Login, encryptPassword);

            switch (loginResponse.TypeUserId)
            {
                case 1:
                    role = "admin";
                    break;
                case 2:
                    role = "user";
                    break;
                default:
                    role = "owner";
                    break;
            }

            loginResponse.Token = _jwtService.GetJwtToken(role, role);
            return loginResponse;
        }

        public async Task<LoginAdminResponseModel> AuthenticateUserAdmin(LoginRequestModel loginRequest)
        {
            HashMd5 hashMd5;
            LoginAdminResponseModel loginResponse;
            hashMd5 = new HashMd5();
            string encryptPassword = hashMd5.EncryptMD5(loginRequest.Password);

            loginResponse = await _adminAuthDb.AuthenticateUserAdmin(loginRequest.Email, encryptPassword);
            if (loginResponse == null)
            {
                throw new RecordNotFoundException();
            }
            loginResponse.Token = _jwtService.GetJwtToken("admin", "admin");
            return loginResponse;
        }

        public async Task<LoginOwnerResponseModel> AuthenticateUserOwner(LoginRequestModel loginRequest)
        {
            HashMd5 hashMd5;
            LoginOwnerResponseModel loginResponse;
            string encryptPassword;

            hashMd5 = new HashMd5();
            encryptPassword = hashMd5.EncryptMD5(loginRequest.Password);

            loginResponse = await _ownerAuthDb.AuthenticateUserOwner(loginRequest.Email, encryptPassword);
            if (loginResponse == null)
            {
                throw new RecordNotFoundException();
            }
            loginResponse.Token = _jwtService.GetJwtToken("owner", "owner");
            return loginResponse;
        }
    }
}
