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
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioCreateMapper _usuarioCreateMapper;
        private readonly IUsuarioUpdateMapper _usuarioUpdateMapper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioDb _usuarioDb;

        public UsuarioService(
            IUsuarioCreateMapper usuarioCreateMapper,
            IUsuarioRepository usuarioRepository,
            IUsuarioDb usuarioDb,
            IUsuarioUpdateMapper usuarioUpdateMapper
            )
        {
            _usuarioCreateMapper = usuarioCreateMapper;
            _usuarioRepository = usuarioRepository;
            _usuarioDb = usuarioDb;
            _usuarioUpdateMapper = usuarioUpdateMapper;
        }

        public async Task<int> CriarUsuario(UsuarioCreateRequestModel request)
        {
            UsuarioCreateValidation validation = new UsuarioCreateValidation();
            if (!validation.IsValid(request))
            {
                Dictionary<string, string> errors = validation.GetErrors();
                throw new CustomValidationException(errors);
            }

            try
            {
                _usuarioCreateMapper.SetBaseMapping(request);
                Usuario user = _usuarioCreateMapper.GetUser();
                _usuarioRepository.Create(user);
                return user.id_usuario;
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
            bool success = await _usuarioDb.AlterarSenha(userId, hashMd5.EncryptMD5(password));

            if (!success)
            {
                throw new RecordNotFoundException("Senha não alterada!");
                return false;
            }
            return true;
            
    }

        public async Task<int> AtualizarUsuario( UsuarioUpdateRequestModel request )
        {
            UsuarioUpdateValidation validation = new UsuarioUpdateValidation();
            Usuario user;
            if (!validation.IsValid(request))
            {
                Dictionary<string, string> errors = validation.GetErrors();
                throw new CustomValidationException(errors);
            }

            try
            {
                _usuarioUpdateMapper.SetBaseMapping(request);
                await _usuarioDb.UpdateUser(request);
                user = _usuarioUpdateMapper.GetUser();
                return user.id_usuario;
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
        
        public async Task<UsuarioResponseModel> BuscarUsuarioPorId(int userId)
        {
            UsuarioResponseModel user = await _usuarioDb.GetUserById(userId);
            if (user == null)
            {
                throw new RecordNotFoundException("Usuário não encontrado.");
            }
            return user;
        }

        public async Task<int> DesativarUsuario ( int userId)
        {
            int user = await _usuarioDb.DesativarUsuario(userId);
            if (user == 0)
            {
                throw new RecordNotFoundException("Usuário não encontrado.");
            }
            return user;
        } 
        public async Task<int> ReativarUsuario( int userId)
        {
            int user = await _usuarioDb.ReativarUsuario(userId);
            if (user == 0)
            {
                throw new RecordNotFoundException("Usuário não encontrado.");
            }
            return user;
        }

        public async Task<UsuarioResponseModel> BuscarUsuarioPorEmail(string email)
        {
            UsuarioResponseModel user = await _usuarioDb.GetUserByEmail(email);
            if (user == null)
            {
                throw new RecordNotFoundException("Usuário não encontrado para o email fornecido.");
            }
            return user;
        }
        
        public async Task<UsuarioResponseModel> BuscarUsuarioPorNome(string name)
        {
            UsuarioResponseModel user = await _usuarioDb.GetUserByName(name);
            if (user == null)
            {
                throw new RecordNotFoundException("Usuário não encontrado para o email fornecido.");
            }
            return user;
        }

        public async Task<EmailCodigoResponseModel> EnviarEmailCodigo ( string email )
        {
            EmailCodigoResponseModel response;
            UsuarioResponseModel user = await _usuarioDb.GetUserByEmail(email);

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

        public async Task<List<UsuarioResponseModel>> BuscarUsuarioPorTipoDeUsuario( int typeUser )
        {
            List<UsuarioResponseModel> user = await _usuarioDb.GetUserByTypeUser(typeUser);

            if (user == null || user.Count == 0)
            {
                throw new RecordNotFoundException("Usuário não encontrado para o email fornecido.");
            }
            return user;
        }
        
        public async Task<List<UsuarioResponseModel>> BuscarUsuarioPorProprietarioeTipoDeUsuario( int typeUser, int ownerId)
        {
            List<UsuarioResponseModel> user = await _usuarioDb.GetUserByOwnerOfTypeUser(typeUser,ownerId);

            if (user == null || user.Count == 0)
            {
                throw new RecordNotFoundException("Usuário não encontrado para o email fornecido.");
            }
            return user;
        }
        
        public async Task<List<UsuarioResponseModel>> BuscarUsuarioPorProprietario( int ownerId)
        {
            List<UsuarioResponseModel> user = await _usuarioDb.GetUserByOwner(ownerId);

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
