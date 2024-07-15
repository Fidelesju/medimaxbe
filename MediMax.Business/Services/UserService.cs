using AutoMapper;
using MailKit.Net.Smtp;
using MediMax.Business.Exceptions;
using MediMax.Business.Services.Interfaces;
using MediMax.Business.Utils;
using MediMax.Business.Validations;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace MediMax.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserDb _userDb;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository UserRepository,
            IUserDb UserDb,
            IMapper mapper
            )
        {
            _userRepository = UserRepository;
            _userDb = UserDb;
            _mapper = mapper;
        }


        public async Task<int> CreateUser ( UserCreateRequestModel request )
        {
            var result = new BaseResponse<int>();
            UserCreateValidation validation = new UserCreateValidation();
            var validationResult = validation.Validate(request);
            HashMd5 hashMd5 = new HashMd5();

            if (!validationResult.IsValid)
            {
                result.Message = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                result.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            }
            try
            {
                var passwordEncrypt = hashMd5.EncryptMD5(request.Password);
                request.Password = passwordEncrypt;
                var user = _mapper.Map<User>(request);
                _userRepository.Create(user);

                if(user.Id != 0)
                {
                    result.IsSuccess = true;
                    result.Data = user.Id;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Data = 0;
                    return result.Data;
                }
                return result.Data;
            }
            catch (DbUpdateException ex)
            {
                Dictionary<string, string> errors = validation.GetPersistenceErrors(ex);
                if (errors.Count > 0)
                {
                    throw new CustomValidationException(errors);
                }
                throw;
            }
        }
   
        public async Task<bool> UpdatePassword (string password, int id, int owner_id)
        {
            HashMd5 hashMd5 = new HashMd5();
            var result = new BaseResponse<bool>();

            bool success = await _userRepository.UpdatePassword(hashMd5.EncryptMD5(password), id, owner_id);

            if (!success)
            {
                result.Data = success;
                result.Message = "Senha não alterada!";
                return result.Data;
            }
            return true;
            
        }

        public async Task<int> UpdateUser ( UserUpdateRequestModel request )
        {
            var result = new BaseResponse<int>();
            UserUpdateValidation validation = new UserUpdateValidation();
            var validationResult = validation.Validate(request);

            if (!validationResult.IsValid)
            {
                result.Message = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                result.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            }
            
            try
            {
                var user = _mapper.Map<UserResponseModel>(request);
                await _userRepository.Update(user);
                if (user.Id != 0)
                {
                    result.IsSuccess = true;
                    result.Data = user.Id;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Data = 0;
                    return result.Data;
                }
                return result.Data;
            }
            catch (DbUpdateException ex)
            {
                Dictionary<string, string> errors = validation.GetPersistenceErrors(ex);
                if (errors.Count > 0)
                {
                    throw new CustomValidationException(errors);
                }
                throw;
            }
        }

        public async Task<EmailCodigoResponseModel> SendCodeToEmail ( string email , string name, int id)
        {
            EmailCodigoResponseModel response;
          
            string code = GenerateRandomCode();
            string subject = "Código de recuperação de senha";
            string body = $@"
            <!DOCTYPE html>
            <html lang='en'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Código de Verificação</title>
                <style>
                    body {{ font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 20px; }}
                    .container {{ background-color: #ffffff; width: 100%; max-width: 600px; margin: 0 auto; padding: 20px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); }}
                    .header {{ background-color: #004a99; color: #ffffff; padding: 10px 20px; text-align: center; }}
                    .body {{ padding: 20px; text-align: center; line-height: 1.6; }}
                    .footer {{ font-size: 12px; text-align: center; color: #777; padding: 20px; }}
                    .code {{ font-size: 24px; color: #333333; font-weight: bold; margin: 20px 0; }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <div class='header'>Bem-vindo à Medimax</div>
                    <div class='body'>
                        <p>Olá, {name},</p>
                        <p>Aqui está o seu código de verificação:</p>
                        <p class='code'>{code}</p>
                    </div>
                    <div class='footer'>© 2024 Medimax. Todos os direitos reservados.</div>
                </div>
            </body>
            </html>";

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("MediMax", "suportemedimax@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            emailMessage.Body = bodyBuilder.ToMessageBody();

            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, false);
                    await client.AuthenticateAsync("suportemedimax@gmail.com", "maodxmtcbrxgomhy");
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                }
                response = new EmailCodigoResponseModel()
                {
                    Code = code,
                    UserId = id,
                    Email = email,
                    Message = "Codigo enviado com sucesso!",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                response = new EmailCodigoResponseModel()
                {
                    Code = code,
                    Email = email,
                    Message = ex.Message,
                    Success = false
                };
            }

            return response;
        }

        public async Task<int> DesactiveUser ( int id, int owner_id)
        {
            var result = new BaseResponse<int>();
            var user = await _userRepository.Desactive(id, owner_id);
            if (!user)
            {
                result.Message = "Usuário não desativado.";
                result.Data = 0;
                result.IsSuccess = false;
            }
            else
            {
                result.IsSuccess = true;
                result.Data = id;
            }

            return result.Data;
        } 

        public async Task<int> ReactiveUser( int id , int owner_id)
        {
            var result = new BaseResponse<int>();
            var user = await _userRepository.Reactive(id, owner_id);
            if (!user)
            {
                result.Message = "Usuário não reativado.";
                result.Data = 0;
                result.IsSuccess = false;
            }
            else
            {
                result.IsSuccess = true;
                result.Data = id;
            }

            return result.Data;
        }

        public async Task<UserResponseModel> GetUserById ( int userId )
        {
            UserResponseModel user = await _userDb.GetUserById(userId);

            var result = new BaseResponse<UserResponseModel>();
            if (user == null)
            {
                result.Message  = "Usuário não encontrado.";
                result.Data = user;
            }
            else
            {
                result.Message = "Usuário encontrado com sucesso.";
                result.Data = user;
            }
            return result.Data;
        }
        
        public async Task<UserResponseModel> GetUserByEmail ( string email )
        {
            UserResponseModel user = await _userDb.GetUserByEmail(email);

            var result = new BaseResponse<UserResponseModel>();
            if (user == null)
            {
                result.Message = "Usuário não encontrado.";
                result.Data = user;
            }
            else
            {
                result.Message = "Usuário encontrado com sucesso.";
                result.Data = user;
            }
            return result.Data;
        }
        
        public async Task<UserResponseModel> GetUserByName( string name )
        {
            UserResponseModel user = await _userDb.GetUserByName(name);

            var result = new BaseResponse<UserResponseModel>();
            if (user == null)
            {
                result.Message = "Usuário não encontrado.";
                result.Data = user;
            }
            else
            {
                result.Message = "Usuário encontrado com sucesso.";
                result.Data = user;
            }
            return result.Data;
        }

        public async Task<List<UserResponseModel>> GetUserByType( int typeUser )
        {
            List<UserResponseModel> user = await _userDb.GetUserByType(typeUser);
            var result = new BaseResponse<List<UserResponseModel>>();
            if (user == null)
            {
                result.Message = "Usuário não encontrado.";
                result.Data = user;
            }
            else
            {
                result.Message = "Usuário encontrado com sucesso.";
                result.Data = user;
            }
            return result.Data;
        }
        
        public async Task<List<UserResponseModel>> GetUserByTypeAndOwnerId( int typeUser, int ownerId)
        {
            List<UserResponseModel> user = await _userDb.GetUserByTypeAndOwnerId(typeUser,ownerId);
            var result = new BaseResponse<List<UserResponseModel>>();
            if (user == null)
            {
                result.Message = "Usuário não encontrado.";
                result.Data = user;
            }
            else
            {
                result.Message = "Usuário encontrado com sucesso.";
                result.Data = user;
            }
            return result.Data;
        }
        
        public async Task<List<UserResponseModel>> GetUserByOwner( int ownerId)
        {
            List<UserResponseModel> user = await _userDb.GetUserByOwner(ownerId);
            var result = new BaseResponse<List<UserResponseModel>>();
            if (user == null)
            {
                result.Message = "Usuário não encontrado.";
                result.Data = user;
            }
            else
            {
                result.Message = "Usuário encontrado com sucesso.";
                result.Data = user;
            }
            return result.Data;
        }

        private static string GenerateRandomCode ( )
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString(); 
        }
    }
}
