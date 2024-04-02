using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<int> CriarUsuario(UsuarioCreateRequestModel request);
        Task<UsuarioResponseModel> BuscarUsuarioPorId(int userId);
        Task<UsuarioResponseModel> BuscarUsuarioPorEmail(string name);
    }
}
