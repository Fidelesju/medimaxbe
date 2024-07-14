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
        private readonly IMedicamentoCreateMapper _medicationCreateMapper;
        private readonly ITreatmentCreateMapper _TreatmentCreateMapper;
        private readonly IMedicamentosRepository _medicineRepository;
        private readonly ITreatmentRepository _TreatmentRepository;
        private readonly ITreatmentDb _treatementDb;

        public TreatmentService(
            IMedicamentoCreateMapper medicineCreateMapper,
            ITreatmentCreateMapper TreatmentCreateMapper,
            IMedicamentosRepository medicineRepository,
            ITreatmentRepository TreatmentRepository,
            ITreatmentDb treatementDb) 
        {
            _medicationCreateMapper = medicineCreateMapper;
            _medicineRepository = medicineRepository;
            _treatementDb = treatementDb;
            _TreatmentCreateMapper = TreatmentCreateMapper;
            _TreatmentRepository = TreatmentRepository;
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
