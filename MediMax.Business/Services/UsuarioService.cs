using MediMax.Business.Exceptions;
using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Services.Interfaces;
using MediMax.Business.Validations;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace MediMax.Business.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioCreateMapper _usuarioCreateMapper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioDb _usuarioDb;

        public UsuarioService(
            IUsuarioCreateMapper usuarioCreateMapper,
            IUsuarioRepository usuarioRepository,
            IUsuarioDb usuarioDb)
        {
            _usuarioCreateMapper = usuarioCreateMapper;
            _usuarioRepository = usuarioRepository;
            _usuarioDb = usuarioDb;
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

        public async Task<UsuarioResponseModel> BuscarUsuarioPorId(int userId)
        {
            UsuarioResponseModel user = await _usuarioDb.GetUserById(userId);
            if (user == null)
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
    }
}
