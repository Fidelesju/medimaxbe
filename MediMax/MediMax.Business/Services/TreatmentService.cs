using Microsoft.EntityFrameworkCore;
using MediMax.Business.Exceptions;
using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Services.Interfaces;
using MediMax.Business.Validations;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Repositories.Interfaces;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using MediMax.Data.Models;
using MediMax.Data.ApplicationModels;

namespace MediMax.Business.Services
{
    public class TreatmentService : ITreatmentService
    {
        private readonly IMedicamentoCreateMapper _medicamentoCreateMapper;
        private readonly ITratamentoCreateMapper _tratamentoCreateMapper;
        private readonly IMedicamentosRepository _medicineRepository;
        private readonly ITratamentoRepository _tratamentoRepository;
        private readonly ITreatmentDb _treatementDb;

        public TreatmentService(
            IMedicamentoCreateMapper medicineCreateMapper,
            ITratamentoCreateMapper tratamentoCreateMapper,
            IMedicamentosRepository medicineRepository,
            ITratamentoRepository tratamentoRepository,
            ITreatmentDb treatementDb) 
        {
            _medicamentoCreateMapper = medicineCreateMapper;
            _medicineRepository = medicineRepository;
            _treatementDb = treatementDb;
            _tratamentoCreateMapper = tratamentoCreateMapper;
            _tratamentoRepository = tratamentoRepository;
        }

        public async Task<List<TreatmentResponseModel>> GetTreatmentByName(string name)
        {
            List<TreatmentResponseModel> treatmentList;
            treatmentList = await _treatementDb.GetTreatmentByName(name);

            if (treatmentList == null || treatmentList.Count == 0)
            {
                throw new RecordNotFoundException();
            }
            return treatmentList;
        }
    }
}
