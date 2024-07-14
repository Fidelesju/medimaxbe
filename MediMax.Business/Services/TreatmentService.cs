using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediMax.Business.Exceptions;
using MediMax.Business.Mappers;
using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Services.Interfaces;
using MediMax.Business.Validations;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Dao;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MediMax.Business.Services
{
    /// <summary>
    /// Serviço para operações relacionadas a Treatments.
    /// </summary>
    public class TreatmentService : ITreatmentService
    {
        private readonly ITreatmentCreateMapper _treatmentCreateMapper;
        private readonly ITreatmentUpdateMapper _treatmentUpdateMapper;
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly ITreatmentDb _TreatmentDb;
        private readonly IHorariosDosagemDb _horariosDosagemDb;
        private readonly IHorarioDosagemCreateMapper _horarioDosagemCreateMapper;
        private readonly IHorarioDosagemRepository _horarioDosagemRepository;


        /// <summary>
        /// Inicializa uma nova instância do serviço de Treatment.
        /// </summary>
        public TreatmentService(
            IMedicationCreateMapper medicamentoCreateMapper,
            ITreatmentCreateMapper treatmentCreateMapper,
            ITreatmentUpdateMapper treatmentUpdateMapper,
            IMedicationRepository medicamentoRepository,
            ITreatmentRepository TreatmentRepository,
            ITreatmentDb TreatmentDb,
            IHorariosDosagemDb horariosDosagemDb,
            IHorarioDosagemCreateMapper horarioDosagemCreateMapper,
            IHorarioDosagemRepository horarioDosagemRepository )
        {
            _treatmentCreateMapper = treatmentCreateMapper ?? throw new ArgumentNullException(nameof(treatmentCreateMapper));
            _treatmentRepository = TreatmentRepository ?? throw new ArgumentNullException(nameof(TreatmentRepository));
            _TreatmentDb = TreatmentDb ?? throw new ArgumentNullException(nameof(TreatmentDb));
            _horariosDosagemDb = horariosDosagemDb;
            _horarioDosagemCreateMapper = horarioDosagemCreateMapper;
            _treatmentUpdateMapper = treatmentUpdateMapper;
            _horarioDosagemRepository = horarioDosagemRepository;
        }

        /// <summary>
        /// Cria um novo tratamento
        /// </summary>
        public async Task<int> CreateTreatment ( TreatmentCreateRequestModel request )
        {
            Treatment treatments;
            TimeDosage horarioDosagem;
            HorariosDosagemCreateRequestModel requestHorario;
            HorariosDosagemResponseModel horarioExistente;
            TreatmentCreateValidation validation;
            List<string> horariosDosagem;
            Dictionary<string, string> errors;

            validation = new TreatmentCreateValidation();
            if (!validation.IsValid(request))
            {
                return 0;
            }

            try
            {
                if (request.observation.IsNullOrEmpty())
                    request.observation = "Não há observações";
                horariosDosagem = CalcularHorariosDoses(request.treatment_start_time, request.treatment_interval_hours);
                treatments = _treatmentCreateMapper.GetTreatment();
                _treatmentRepository.Create(treatments);

                // Criar horários de dosagem e salvar no banco de dados
                foreach (string horario in horariosDosagem)
                {
                    HorariosDosagemCreateRequestModel horarioDosagemRequest = new HorariosDosagemCreateRequestModel
                    {
                        tratamento_id = treatments.Id,
                        horario_dosagem = horario
                    };

                    // Verifica se já existe um horário de dosagem com os mesmos valores na tabela
                    horarioExistente = await _horariosDosagemDb.BuscarHorarioDosagemExistente(treatments.Id, horario);
                    if (horarioExistente != null)
                    {
                        // O horário de dosagem já existe, pode optar por atualizar ou pular a inserção
                        continue; // Neste caso, optamos por pular a inserção se o horário já existir
                    }

                    horarioDosagem = _horarioDosagemCreateMapper.BuscarHorariosDosagem(horarioDosagemRequest);
                    _horarioDosagemRepository.Create(horarioDosagem);
                }

                return treatments.Id;
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

        /// <summary>
        /// Alterando medicamentos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="CustomValidationException"></exception>
        public async Task<bool> UpdateMedication ( TreatmentUpdateRequestModel request )
        {
            var result = new BaseResponse<bool>();
            TreatmentUpdateValidation validation = new TreatmentUpdateValidation();
            Medication medication = new Medication();
            HorariosDosagemResponseModel horarioExistente = new HorariosDosagemResponseModel();
            TimeDosage horarioDosagem = new TimeDosage();
            Treatment treatment = new Treatment();
            List<string> horariosDosagem;
            Dictionary<string, string> errors;

            var validationResult = validation.Validate(request);
            if (!validationResult.IsValid)
            {
                result.Message = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                result.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            }

            try
            {
                _treatmentUpdateMapper.SetBaseMapping(request);
                await _horariosDosagemDb.DeletandoHorarioDosagem(request.treatment_id);
                horariosDosagem = CalcularHorariosDoses(request.treatment_start_time, request.treatment_interval_hours);

                if (horariosDosagem != null) {
                    treatment = _treatmentUpdateMapper.GetTreatment();
                    _treatmentRepository.Update(treatment);
                    result.Data = true;
                    result.IsSuccess = true;
                }

                if (result.IsSuccess)
                {
                    foreach (string horario in horariosDosagem)
                    {
                        HorariosDosagemCreateRequestModel horarioDosagemRequest = new HorariosDosagemCreateRequestModel
                        {
                            tratamento_id = request.treatment_id,
                            horario_dosagem = horario
                        };

                        horarioExistente = await _horariosDosagemDb.BuscarHorarioDosagemExistente(request.treatment_id, horario);
                        if (horarioExistente != null)
                        {
                            continue;
                        }

                        horarioDosagem = _horarioDosagemCreateMapper.BuscarHorariosDosagem(horarioDosagemRequest);
                        _horarioDosagemRepository.Create(horarioDosagem);
                    }

                }

                return result.Data;

            }
            catch (DbUpdateException exception)
            {
                errors = validation.GetPersistenceErrors(exception);
                throw new CustomValidationException(errors);
            }
        }
        
        /// <inheritdoc/>
        public async Task<List<TreatmentResponseModel>> GetTreatmentByMedicationId( int medicineId, int userId )
        {
           List<TreatmentResponseModel> treatmentList;
            treatmentList = await _TreatmentDb.GetTreatmentByMedicationId(medicineId, userId);
            return treatmentList;
        } /// <inheritdoc/>
        public async Task<List<HorariosDosagemResponseModel>> GetDosageTimeByTreatmentId ( int treatmentId )
        {
            List<HorariosDosagemResponseModel> treatmentList;
            treatmentList = await _horariosDosagemDb.GetDosageTimeByTreatmentId(treatmentId);
            return treatmentList;
        }
        public async Task<TreatmentResponseModel> GetTreatmentById ( int treatmentId, int userId )
        {
            TreatmentResponseModel treatmentList;
            treatmentList = await _TreatmentDb.GetTreatmentById(treatmentId, userId);
            return treatmentList;
        }

        /// <inheritdoc/>
        public async Task<List<TreatmentResponseModel>> GetTreatmentActives ( int userId )
        {

            List<TreatmentResponseModel> treatmentList;
            treatmentList = await _TreatmentDb.GetTreatmentActives(userId);
            return treatmentList;
        } 
        
        /// <inheritdoc/>
        public async Task<List<TreatmentResponseModel>> GetTreatmentByInterval(string startTime, string finishTime, int userId )
        {
            List<TreatmentResponseModel> treatmentList;
            treatmentList = await _TreatmentDb.GetTreatmentByInterval(startTime, finishTime, userId);
            return treatmentList;
        }

        private List<string> CalcularHorariosDoses(string startTime, int intervaloEmHoras)
        {
            List<string> dosageTimes = new List<string>();
            DateTime startDateTime = DateTime.Parse(startTime);
            dosageTimes.Add(startDateTime.ToString("HH:mm"));
            for (int i = 1; i < 24 / intervaloEmHoras; i++)
            {
                DateTime nextDoseTime = startDateTime.AddHours(intervaloEmHoras * i);
                dosageTimes.Add(nextDoseTime.ToString("HH:mm"));
            }

            return dosageTimes;
        }

     
        public async Task<bool> DeleteTreatment ( int medicineId, int treatmentId )
        {
            var result = new BaseResponse<bool>();
            await _treatmentRepository.Delete(medicineId, treatmentId);
            result.Data = true;
            result.IsSuccess = true;
            return result.Data;
        }
    }
}
