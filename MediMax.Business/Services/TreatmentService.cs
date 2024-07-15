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
using Microsoft.IdentityModel.Tokens;

namespace MediMax.Business.Services
{
    /// <summary>
    /// Serviço para operações relacionadas a Treatments.
    /// </summary>
    public class TreatmentService : ITreatmentService
    {
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly ITreatmentDb _treatmentDb;
        private readonly ITimeDosageDb _timeDosageDb;
        private readonly ITimeDosageRepository _timeDosageRepository;
        private IMapper _mapper;


        /// <summary>
        /// Inicializa uma nova instância do serviço de Treatment.
        /// </summary>
        public TreatmentService(
            IMedicationRepository medicamentoRepository,
            ITreatmentRepository TreatmentRepository,
            ITreatmentDb treatmentDb,
            ITimeDosageDb timeDosageDb,
            ITimeDosageRepository timeDosageRepository ,
            IMapper mapper)
        {
            _treatmentRepository = TreatmentRepository ?? throw new ArgumentNullException(nameof(TreatmentRepository));
            _treatmentDb = treatmentDb ?? throw new ArgumentNullException(nameof(treatmentDb));
            _timeDosageDb = timeDosageDb;
            _timeDosageRepository = timeDosageRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Cria um novo tratamento
        /// </summary>
        public async Task<int> CreateTreatment ( TreatmentCreateRequestModel request )
        {
            TimeDosageResponseModel existTime;
            TreatmentCreateValidation validation;
            List<string> timeDosageList;
            Dictionary<string, string> errors;

            validation = new TreatmentCreateValidation();
            if (!validation.IsValid(request))
            {
                return 0;
            }

            try
            {
                if (request.Observation.IsNullOrEmpty())
                    request.Observation = "Não há observações";
                timeDosageList = CalcularHorariosDoses(request.Start_Time, request.Treatment_Interval_Hours);


                var treatment = _mapper.Map<Treatment>(request);
                _treatmentRepository.Create(treatment);

                foreach (string time in timeDosageList)
                {
                    TimeDosageCreateRequestModel horarioDosagemRequest = new TimeDosageCreateRequestModel
                    {
                        treatment_id = treatment.Id,
                        time = time
                    };

                    // Verifica se já existe um horário de dosagem com os mesmos valores na tabela
                    existTime = await _timeDosageDb.BuscarHorarioDosagemExistente(treatment.Id, time);
                    if (existTime != null)
                    {
                        // O horário de dosagem já existe, pode optar por atualizar ou pular a inserção
                        continue; // Neste caso, optamos por pular a inserção se o horário já existir
                    }

                    var timeDosage = _mapper.Map<TimeDosage>(request);
                    _timeDosageRepository.Create(timeDosage);
                }

                return treatment.Id;
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
            TimeDosageResponseModel horarioExistente = new TimeDosageResponseModel();
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
                await _timeDosageDb.DeletandoHorarioDosagem(request.treatment_id);
                horariosDosagem = CalcularHorariosDoses(request.treatment_start_time, request.treatment_interval_hours);

                if (horariosDosagem != null) {
                    _treatmentRepository.Update(treatment);
                    result.Data = true;
                    result.IsSuccess = true;
                }

                if (result.IsSuccess)
                {
                    foreach (string horario in horariosDosagem)
                    {
                        TimeDosageCreateRequestModel horarioDosagemRequest = new TimeDosageCreateRequestModel
                        {
                            treatment_id = request.treatment_id,
                            time = horario
                        };

                        horarioExistente = await _timeDosageDb.BuscarHorarioDosagemExistente(request.treatment_id, horario);
                        if (horarioExistente != null)
                        {
                            continue;
                        }


                        var horarioDosagem = _mapper.Map<TimeDosage>(request);
                        _timeDosageRepository.Create(horarioDosagem);
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
            treatmentList = await _treatmentDb.GetTreatmentByMedicationId(medicineId, userId);
            return treatmentList;
        } /// <inheritdoc/>
        public async Task<List<TimeDosageResponseModel>> GetDosageTimeByTreatmentId ( int treatmentId )
        {
            List<TimeDosageResponseModel> treatmentList;
            treatmentList = await _timeDosageDb.GetDosageTimeByTreatmentId(treatmentId);
            return treatmentList;
        }
        public async Task<TreatmentResponseModel> GetTreatmentById ( int treatmentId, int userId )
        {
            TreatmentResponseModel treatmentList;
            treatmentList = await _treatmentDb.GetTreatmentById(treatmentId, userId);
            return treatmentList;
        }

        /// <inheritdoc/>
        public async Task<List<TreatmentResponseModel>> GetTreatmentActives ( int userId )
        {

            List<TreatmentResponseModel> treatmentList;
            treatmentList = await _treatmentDb.GetTreatmentActives(userId);
            return treatmentList;
        } 
        
        /// <inheritdoc/>
        public async Task<List<TreatmentResponseModel>> GetTreatmentByInterval(string startTime, string finishTime, int userId )
        {
            List<TreatmentResponseModel> treatmentList;
            treatmentList = await _treatmentDb.GetTreatmentByInterval(startTime, finishTime, userId);
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
