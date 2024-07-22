using AutoMapper;
using MediMax.Business.Exceptions;
using MediMax.Business.Services.Interfaces;
using MediMax.Business.Validations;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using Microsoft.EntityFrameworkCore;
using ServiceStack;

namespace MediMax.Business.Services
{
    public class MedicationService : IMedicationService
    {
        private readonly IMedicationRepository _medicationRepository;
        private readonly ITimeDosageRepository _horarioDosagemRepository;
        private readonly IMedicationDb _medicationDb;
        private readonly ITreatmentDb _treatmentDb;
        private readonly ITimeDosageDb _horarioDosagemDb;
        private readonly IMapper _mapper;

        public MedicationService(
            IMedicationRepository medicationRepository,
            ITimeDosageRepository horarioDosagemRepository,
            ITimeDosageDb horarioDosagemDb,
            IMedicationDb medicationDb,
            ITreatmentDb TreatmentDb,
            IMapper mapper)
        {
            _medicationRepository = medicationRepository;
            _medicationDb = medicationDb;
            _horarioDosagemRepository = horarioDosagemRepository;
            _horarioDosagemDb = horarioDosagemDb;
            _treatmentDb = TreatmentDb;
            _mapper = mapper;
        }

      
        public async Task<BaseResponse<int>> CreateMedication(MedicationCreateRequestModel request)
        {
            var result = new BaseResponse<int>();
            MedicationCreateValidation validation;
            validation = new MedicationCreateValidation();
            
            var validationResult = validation.Validate(request);
            if (!validationResult.IsValid)
            {
                result.Message = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                result.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return result;
            }

            try
            {
                var medication = _mapper.Map<Medication>(request);
                _medicationRepository.Create(medication);
                result.Data = medication.Id;
                result.IsSuccess = true;
                result.SetMessage("Medicamento criado com sucesso!");
            }
            catch (DbUpdateException exception)
            {
                result.Errors.Add(exception.Message.ToString());
            }

            return result;
        }

        /// <summary>
        /// Alterando medicamentos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="CustomValidationException"></exception>
        public async Task<bool> UpdateMedication ( MedicationUpdateRequestModel request )
        {
            var result = new BaseResponse<bool>();
            MedicationtUpdateValidation validation = new MedicationtUpdateValidation();
            Dictionary<string, string> errors;

            var validationResult = validation.Validate(request);
            if (!validationResult.IsValid)
            {
                result.Message = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                result.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            }

            try
            {
                var medication = _mapper.Map<MedicationResponseModel>(request);
                await _medicationRepository.Update(medication);
                result.Data = true;
                result.IsSuccess = true;
                result.SetMessage("Medicamento atualizado com sucesso!");
                return result.Data;
            }
            catch (DbUpdateException exception)
            {
                errors = validation.GetPersistenceErrors(exception);
                throw new CustomValidationException(errors);
            }
        }

        /// <summary>
        /// Deletando medicamentos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="RecordNotFoundException"></exception>
        public async Task<bool> ReactiveMedication ( int medicineId, int userId )
        {
            var result = new BaseResponse<bool>();
            await _medicationRepository.Reactive(medicineId, userId);
            result.Data = true;
            result.IsSuccess = true;
            result.SetMessage("Medicamento reativado com sucesso!");
            return result.Data;
        } 
        
        /// <summary>
        /// Deletando medicamentos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="RecordNotFoundException"></exception>
        public async Task<bool> DesactiveMedication ( int medicineId, int userId )
        {
            var result = new BaseResponse<bool>();
            await _medicationRepository.Desactive(medicineId, userId);
            result.Data = true;
            result.IsSuccess = true;
            result.SetMessage("Medicamento desativado com sucesso!");
            return result.Data;
        }

        /// <summary>
        /// Obtém todos os medicamentos.
        /// </summary>
        public async Task<List<MedicationResponseModel>> GetAllMedicine( int userId )
        {
            List<MedicationResponseModel> medicamentoLista;
            medicamentoLista = await _medicationDb.GetAllMedicine( userId );

            if (medicamentoLista == null || medicamentoLista.Count == 0)
            {
                return null;
            }
            return medicamentoLista;
        }

        /// <summary>
        /// Obtém medicamento por nome.
        /// </summary>
        public async Task<List<MedicationResponseModel>> GetMedicationByName(string name, int userId )
        {
            List<MedicationResponseModel> medicamentoLista;
            medicamentoLista = await _medicationDb.GetMedicationByName(name, userId);

            if (medicamentoLista == null || medicamentoLista.Count == 0)
            {
                return null;
            }
            return medicamentoLista;
        }

        /// <summary>
        /// Obtém medicamento por nome.
        /// </summary>
        public async Task<MedicationResponseModel> GetMedicationByTreatmentId(int TreatmentId, int userId )
        {
            MedicationResponseModel medicamentoLista;
            medicamentoLista = await _medicationDb.GetMedicationByTreatmentId(TreatmentId, userId);

            if (medicamentoLista == null )
            {
                return null;
            }
            return medicamentoLista;
        }

        /// <summary>
        /// Obtém medicamento por data de vencimento.
        /// </summary>
        public async Task<List<MedicationResponseModel>> GetMedicationByExpirationDate( int userId )
        {
            List<MedicationResponseModel> medicamentoLista;
            medicamentoLista = await _medicationDb.GetMedicationByExpirationDate( userId );

            if (medicamentoLista == null || medicamentoLista.Count == 0)
            {
                return null;
            }
            return medicamentoLista;
        }
    }
}
