using MediMax.Data.ApplicationModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface ITreatmentDb
    {
        Task<List<TreatmentResponseModel>> GetTreatmentByName(string name);
    }
}
