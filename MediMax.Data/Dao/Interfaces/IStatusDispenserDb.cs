using MediMax.Data.ApplicationModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface IDispenserStatusDb
    {
        Task<DispenserStatusResponseModel> BuscandoSeExisteAbastacimentoCadastrado ( int TreatmentId );
    }
}
