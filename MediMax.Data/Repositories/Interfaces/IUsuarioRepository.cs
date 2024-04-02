using MediMax.Data.Models;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        int Create(Usuario user);
        void Update(Usuario user);
    }
}
