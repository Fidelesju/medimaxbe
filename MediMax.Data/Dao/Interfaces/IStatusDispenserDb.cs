using MediMax.Data.ApplicationModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface IStatusDispenserDb
    {
        Task<StatusDispenserResponseModel> BuscandoSeExisteAbastacimentoCadastrado ( int tratamentoId );
    }
}
