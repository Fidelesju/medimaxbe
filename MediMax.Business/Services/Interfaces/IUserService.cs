using MediMax.Data.Enums;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task<int> CriarUser(UserCreateRequestModel request);
        Task<UserResponseModel> BuscarUserPorId(int userId);
        Task<UserResponseModel> BuscarUserPorEmail(string email);
        Task<UserResponseModel> BuscarUserPorNome ( string name );
        Task<List<UserResponseModel>> BuscarUserPorTipoDeUser ( int typeUser );
        Task<List<UserResponseModel>> BuscarUserPorProprietario ( int ownerId );
        Task<List<UserResponseModel>> BuscarUserPorProprietarioeTipoDeUser ( int typeUser, int ownerId );
        Task<int> AtualizarUser ( UserUpdateRequestModel request );
        Task<int> DesativarUser ( int userId );
        Task<bool> AlterarSenha ( string password, int userId );
        Task<int> ReativarUser ( int userId );
        Task<EmailCodigoResponseModel> EnviarEmailCodigo ( string email );
    }
}
