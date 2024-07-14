using MediMax.Data.ApplicationModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface IMedicationDb
    {
        Task<List<MedicationResponseModel>> GetAllMedicine(int userId);
        Task<List<MedicationResponseModel>> GetMedicationByName(string name, int userId);
        Task<List<MedicationResponseModel>> GetMedicationByExpirationDate( int userId );
        Task<MedicationResponseModel> GetMedicationByTreatmentId(int TreatmentId, int userId );
        Task<bool> DeletandoMedicamento(int id, int userId );
        Task<bool> AlterandoMedicamento(string nome, string data_vencimento, int quantidade_embalagem, float dosagem, int id, int userId );
        Task<List<MedicationResponseModel>> BuscarMedicamentosInativos ( int userId );
    }
}
