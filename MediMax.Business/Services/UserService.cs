using MailKit.Net.Smtp;
using MediMax.Business.Exceptions;
using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Services.Interfaces;
using MediMax.Business.Utils;
using MediMax.Business.Validations;
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
        private readonly IUserCreateMapper _UserCreateMapper;
        private readonly IUserUpdateMapper _UserUpdateMapper;
        private readonly IUserRepository _UserRepository;
        private readonly IUserDb _UserDb;

        public UserService(
            IUserCreateMapper UserCreateMapper,
            IUserRepository UserRepository,
            IUserDb UserDb,
            IUserUpdateMapper UserUpdateMapper
            )
        {
            _UserCreateMapper = UserCreateMapper;
            _UserRepository = UserRepository;
            _UserDb = UserDb;
            _UserUpdateMapper = UserUpdateMapper;
        }

        public async Task<int> CriarUser(UserCreateRequestModel request)
        {
            UserCreateValidation validation = new UserCreateValidation();
            if (!validation.IsValid(request))
            {
                Dictionary<string, string> errors = validation.GetErrors();
                throw new CustomValidationException(errors);
            }

            try
            {
                _UserCreateMapper.SetBaseMapping(request);
                User user = _UserCreateMapper.GetUser();
                _UserRepository.Create(user);
                return user.Id;
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
      
        public async Task<bool> AlterarSenha(string password, int userId)
        {
            HashMd5 hashMd5 = new HashMd5();
            bool success = await _UserDb.AlterarSenha(userId, hashMd5.EncryptMD5(password));

            if (!success)
            {
                throw new RecordNotFoundException("Senha não alterada!");
                return false;
            }
            return true;
            
    }

        public async Task<int> AtualizarUser( UserUpdateRequestModel request )
        {
            UserUpdateValidation validation = new UserUpdateValidation();
            User user;
            if (!validation.IsValid(request))
            {
                Dictionary<string, string> errors = validation.GetErrors();
                throw new CustomValidationException(errors);
            }

            try
            {
                _UserUpdateMapper.SetBaseMapping(request);
                await _UserDb.UpdateUser(request);
                user = _UserUpdateMapper.GetUser();
                return user.Id;
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
        
        public async Task<UserResponseModel> BuscarUserPorId(int userId)
        {
            UserResponseModel user = await _UserDb.GetUserById(userId);
            if (user == null)
            {
                throw new RecordNotFoundException("Usuário não encontrado.");
            }
            return user;
        }

        public async Task<int> DesativarUser ( int userId)
        {
            int user = await _UserDb.DesativarUser(userId);
            if (user == 0)
            {
                throw new RecordNotFoundException("Usuário não encontrado.");
            }
            return user;
        } 
        public async Task<int> ReativarUser( int userId)
        {
            int user = await _UserDb.ReativarUser(userId);
            if (user == 0)
            {
                throw new RecordNotFoundException("Usuário não encontrado.");
            }
            return user;
        }

        public async Task<UserResponseModel> BuscarUserPorEmail(string email)
        {
            UserResponseModel user = await _UserDb.GetUserByEmail(email);
            if (user == null)
            {
                throw new RecordNotFoundException("Usuário não encontrado para o email fornecido.");
            }
            return user;
        }
        
        public async Task<UserResponseModel> BuscarUserPorNome(string name)
        {
            UserResponseModel user = await _UserDb.GetUserByName(name);
            if (user == null)
            {
                throw new RecordNotFoundException("Usuário não encontrado para o email fornecido.");
            }
            return user;
        }

        public async Task<EmailCodigoResponseModel> EnviarEmailCodigo ( string email )
        {
            EmailCodigoResponseModel response;
            UserResponseModel user = await _UserDb.GetUserByEmail(email);

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
                        <p>Olá, {user.Name},</p>
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
                    UserId = user.Id,
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

        public async Task<List<UserResponseModel>> BuscarUserPorTipoDeUser( int typeUser )
        {
            List<UserResponseModel> user = await _UserDb.GetUserByTypeUser(typeUser);

            if (user == null || user.Count == 0)
            {
                throw new RecordNotFoundException("Usuário não encontrado para o email fornecido.");
            }
            return user;
        }
        
        public async Task<List<UserResponseModel>> BuscarUserPorProprietarioeTipoDeUser( int typeUser, int ownerId)
        {
            List<UserResponseModel> user = await _UserDb.GetUserByOwnerOfTypeUser(typeUser,ownerId);

            if (user == null || user.Count == 0)
            {
                throw new RecordNotFoundException("Usuário não encontrado para o email fornecido.");
            }
            return user;
        }
        
        public async Task<List<UserResponseModel>> BuscarUserPorProprietario( int ownerId)
        {
            List<UserResponseModel> user = await _UserDb.GetUserByOwner(ownerId);

            if (user == null || user.Count == 0)
            {
                throw new RecordNotFoundException("Usuário não encontrado para o email fornecido.");
            }
            return user;
        }

        private static string GenerateRandomCode ( )
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString(); 
        }
    }
}
