using MediMax.Data.Enums;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<int> CriarUsuario(UsuarioCreateRequestModel request);
        Task<UsuarioResponseModel> BuscarUsuarioPorId(int userId);
        Task<UsuarioResponseModel> BuscarUsuarioPorEmail(string email);
        Task<UsuarioResponseModel> BuscarUsuarioPorNome ( string name );
        Task<List<UsuarioResponseModel>> BuscarUsuarioPorTipoDeUsuario ( int typeUser );
        Task<List<UsuarioResponseModel>> BuscarUsuarioPorProprietario ( int ownerId );
        Task<List<UsuarioResponseModel>> BuscarUsuarioPorProprietarioeTipoDeUsuario ( int typeUser, int ownerId );
        Task<int> AtualizarUsuario ( UsuarioUpdateRequestModel request );
        Task<int> DesativarUsuario ( int userId );
        Task<string> AlterarSenha ( string password, string codeSend, string codeCorrect, int userId );
        Task<int> ReativarUsuario ( int userId );
        Task<EmailCodigoResponseModel> EnviarEmailCodigo ( string email );
    }
}
