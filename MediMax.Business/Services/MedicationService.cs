using MediMax.Business.Exceptions;
using MediMax.Business.Mappers;
using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Services.Interfaces;
using MediMax.Business.Validations;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Dao;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.Repositories;
using MediMax.Data.Repositories.Interfaces;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using ServiceStack;

namespace MediMax.Business.Services
{
    public class MedicationService : IMedicationService
    {
        private readonly IMedicationCreateMapper _medicationCreateMapper;
        private readonly IMedicationUpdateMapper _medicationUpdateMapper;
        private readonly IMedicationRepository _medicationRepository;
        private readonly IHorarioDosagemRepository _horarioDosagemRepository;
        private readonly IMedicationDb _medicationDb;
        private readonly ITreatmentDb _treatmentDb;
        private readonly IHorariosDosagemDb _horarioDosagemDb;
        private readonly IHorarioDosagemCreateMapper _horarioDosagemCreateMapper;
        public MedicationService(
            IMedicationCreateMapper medicationCreateMapper,
            IMedicationRepository medicationRepository,
            IHorarioDosagemCreateMapper horarioDosagemCreateMapper,
            IHorarioDosagemRepository horarioDosagemRepository,
            IHorariosDosagemDb horarioDosagemDb,
            IMedicationDb medicationDb,
            ITreatmentDb TreatmentDb,
            IMedicationUpdateMapper medicationUpdateMapper)
        {
            _medicationCreateMapper = medicationCreateMapper;
            _medicationRepository = medicationRepository;
            _medicationDb = medicationDb;
            _horarioDosagemCreateMapper = horarioDosagemCreateMapper;
            _horarioDosagemRepository = horarioDosagemRepository;
            _horarioDosagemDb = horarioDosagemDb;
            _treatmentDb = TreatmentDb;
            _medicationUpdateMapper = medicationUpdateMapper;
        }

      
        public async Task<BaseResponse<int>> CreateMedication(MedicationCreateRequestModel request)
        {
            var result = new BaseResponse<int>();
            Medicamentos medication;
            MedicationCreateValidation validation = new MedicationCreateValidation();
            Dictionary<string, string> errors;

            _medicationCreateMapper.SetBaseMapping(request);
            var validationResult = validation.Validate(request);
            if (!validationResult.IsValid)
            {
                result.Message = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                result.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return result;
            }

            try
            {
                medication = _medicationCreateMapper.GetMedication();
                _medicationRepository.Create(medication);
                result.Data = medication.id;
                result.IsSuccess = true;
            }
            catch (DbUpdateException exception)
            {
                // Handle persistence errors
                result.Errors.Add("Database update failed.");
            }

            return result;
        }

        /// <summary>
        /// Alterando medicamentos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="CustomValidationException"></exception>
        public async Task<bool> UpdateMedication (MedicationUpdateRequestModel request)
        {
            var result = new BaseResponse<bool>();
            MedicationtUpdateValidation validation = new MedicationtUpdateValidation();
            Medicamentos medication;
            Dictionary<string, string> errors;
            bool successMedicamento;

            var validationResult =  validation.Validate(request);
            if (!validationResult.IsValid)
            {
                result.Message = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                result.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            }

                try
                {
                    _medicationUpdateMapper.SetBaseMapping(request);
                     medication = _medicationUpdateMapper.GetMedication();
                    _medicationRepository.Update(medication);
                    result.Data = true;
                    result.IsSuccess = true;
                    return result.IsSuccess;
              
            }
            catch (DbUpdateException exception)
            {
                errors = validation.GetPersistenceErrors(exception);
                throw new CustomValidationException(errors);
            }
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

        /// <summary>
        /// Deletando medicamentos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="RecordNotFoundException"></exception>
        public async Task<bool> DeleteMedication(int medicineId,  int userId )
        {
            var result = new BaseResponse<bool>();
            await _medicationRepository.Delete(medicineId, userId);
            result.Data = true;
            result.IsSuccess = true;
            return result.Data;
        }
    }
}
