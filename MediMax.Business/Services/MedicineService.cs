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
        private readonly IMedicamentoCreateMapper _medicamentoCreateMapper;
        private readonly ITratamentoCreateMapper _tratamentoCreateMapper;
        private readonly IMedicamentosRepository _medicineRepository;
        private readonly ITratamentoRepository _tratamentoRepository;
        private readonly IMedicineDb _medicineDb;

        public MedicineService(
            IMedicamentoCreateMapper medicineCreateMapper,
            ITratamentoCreateMapper tratamentoCreateMapper,
            IMedicamentosRepository medicineRepository,
            ITratamentoRepository tratamentoRepository,
            IMedicineDb medicineDb) 
        {
            _medicamentoCreateMapper = medicineCreateMapper;
            _medicineRepository = medicineRepository;
            _medicineDb = medicineDb;
            _tratamentoCreateMapper = tratamentoCreateMapper;
            _tratamentoRepository = tratamentoRepository;
        }

        public async Task<int> CreateMedicineAndTreatment(MedicamentoETratamentoCreateRequestModel request)
        {
            Tratamento tratamentos;
            Medicamentos medicamentos;
            MedicineCreateValidation validation;
            Dictionary<string, string> errors;

            _medicamentoCreateMapper.SetBaseMapping(request);
            validation = new MedicineCreateValidation();
            if (!validation.IsValid(request))
            {
                errors = validation.GetErrors();
                throw new CustomValidationException(errors);
            }
            try
            {
                medicamentos = _medicamentoCreateMapper.GetMedicamentos();
                tratamentos = _tratamentoCreateMapper.GetTratemento(request);
                _medicineRepository.Create(medicamentos);
                _tratamentoRepository.Create(tratamentos);
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
