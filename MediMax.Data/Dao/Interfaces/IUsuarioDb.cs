using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface IUsuarioDb
    {
        Task<UsuarioResponseModel> GetUserById(int userId);
        Task<UsuarioResponseModel> GetUserByEmail(string name);
    }
}
