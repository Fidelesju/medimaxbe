using MediMax.Data.Enums;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface IUsuarioDb
    {
        Task<UsuarioResponseModel> GetUserById(int userId);
        Task<UsuarioResponseModel> GetUserByEmail(string name);
        Task<UsuarioResponseModel> GetUserByName ( string name );
        Task<List<UsuarioResponseModel>> GetUserByTypeUser ( int typeUser );
        Task<List<UsuarioResponseModel>> GetUserByOwner ( int ownerId );
        Task<List<UsuarioResponseModel>> GetUserByOwnerOfTypeUser ( int typeUser, int ownerId );
        Task<int> UpdateUser ( UsuarioUpdateRequestModel request );
        Task<int> DesativarUsuario ( int userId );
        Task<bool> AlterarSenha ( int userId, string password );
        Task<int> ReativarUsuario ( int userId );
    }
}
