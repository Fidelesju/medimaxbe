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
    public class MedicineService : IMedicineService
    {
        private readonly IMedicamentoCreateMapper _medicationCreateMapper;
        private readonly ITreatmentCreateMapper _TreatmentCreateMapper;
        private readonly IMedicamentosRepository _medicineRepository;
        private readonly ITreatmentRepository _TreatmentRepository;
        private readonly IMedicineDb _medicineDb;

        public MedicineService(
            IMedicamentoCreateMapper medicineCreateMapper,
            ITreatmentCreateMapper TreatmentCreateMapper,
            IMedicamentosRepository medicineRepository,
            ITreatmentRepository TreatmentRepository,
            IMedicineDb medicineDb) 
        {
            _medicationCreateMapper = medicineCreateMapper;
            _medicineRepository = medicineRepository;
            _medicineDb = medicineDb;
            _TreatmentCreateMapper = TreatmentCreateMapper;
            _TreatmentRepository = TreatmentRepository;
        }

        public async Task<int> CreateMedicineAndTreatment(MedicamentoETreatmentCreateRequestModel request)
        {
            Treatment Treatments;
            Medicamentos medicamentos;
            MedicineCreateValidation validation;
            Dictionary<string, string> errors;

            _medicationCreateMapper.SetBaseMapping(request);
            validation = new MedicineCreateValidation();
            if (!validation.IsValid(request))
            {
                errors = validation.GetErrors();
                throw new CustomValidationException(errors);
            }
            try
            {
                medicamentos = _medicationCreateMapper.GetMedicamentos();
                Treatments = _TreatmentCreateMapper.GetTratemento(request);
                _medicineRepository.Create(medicamentos);
                _TreatmentRepository.Create(Treatments);
                return medicamentos.id;
            }
            catch (DbUpdateException exception)
            {
                errors = validation.GetPersistenceErrors(exception);
                if (errors.Count == 0)
                {
                    throw;
                }
                throw new CustomValidationException(errors);
            }
        }

        public async Task<List<MedicineResponseModel>> GetAllMedicine()
        {
            List<MedicineResponseModel> medicineList;
            medicineList = await _medicineDb.GetAllMedicine();

            if (medicineList == null || medicineList.Count == 0)
            {
                throw new RecordNotFoundException();
            }
            return medicineList;
        }

        //public async Task<UserResponseModel> GetUserByName(string name)
        //{
        //    UserResponseModel user;
        //    user = await _userDb.GetUserByName(name);
        //    if (user == null)
        //    {
        //        throw new RecordNotFoundException();
        //    }
        //    return user;
        //}
    }
}
