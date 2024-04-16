using MediMax.Business.Exceptions;
using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Services.Interfaces;
using MediMax.Business.Validations;
using MediMax.Data.Dao;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.Repositories;
using MediMax.Data.Repositories.Interfaces;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace MediMax.Business.Services
{
    public class MedicamentoService : IMedicamentoService
    {
        private readonly IMedicamentoCreateMapper _medicamentoCreateMapper;
        private readonly ITratamentoCreateMapper _tratamentoCreateMapper;
        private readonly IMedicamentosRepository _medicineRepository;
        private readonly ITratamentoRepository _tratamentoRepository;
        private readonly IHorarioDosagemRepository _horarioDosagemRepository;
        private readonly IMedicamentoDb _medicamentoDb;
        private readonly ITratamentoDb _tratamentoDb;
        private readonly IHorariosDosagemDb _horarioDosagemDb;
        private readonly IHorarioDosagemCreateMapper _horarioDosagemCreateMapper;
        public MedicamentoService(
            IMedicamentoCreateMapper medicineCreateMapper,
            ITratamentoCreateMapper tratamentoCreateMapper,
            IMedicamentosRepository medicineRepository,
            ITratamentoRepository tratamentoRepository,
            IHorarioDosagemCreateMapper horarioDosagemCreateMapper,
            IHorarioDosagemRepository horarioDosagemRepository,
            IHorariosDosagemDb horarioDosagemDb,
            IMedicamentoDb medicineDb,
            ITratamentoDb tratamentoDb)
        {
            _medicamentoCreateMapper = medicineCreateMapper;
            _medicineRepository = medicineRepository;
            _medicamentoDb = medicineDb;
            _tratamentoCreateMapper = tratamentoCreateMapper;
            _tratamentoRepository = tratamentoRepository;
            _horarioDosagemCreateMapper = horarioDosagemCreateMapper;
            _horarioDosagemRepository = horarioDosagemRepository;
            _horarioDosagemDb = horarioDosagemDb;
            _tratamentoDb = tratamentoDb;
        }

        /// <summary>
        /// Cria um novo medicamento e tratamento.
        /// </summary>
        public async Task<int> CriandoMedicamentosETratamento(MedicamentoETratamentoCreateRequestModel request)
        {
            Tratamento tratamentos;
            Medicamentos medicamentos;
            HorariosDosagem horarioDosagem;
            HorariosDosagemCreateRequestModel requestHorario;
            HorariosDosagemResponseModel horarioExistente;
            List<string> horariosDosagem;
            MedicamentoCreateValidation validation = new MedicamentoCreateValidation();
            Dictionary<string, string> errors;

            _medicamentoCreateMapper.SetBaseMapping(request);
            if (!validation.IsValid(request))
            {
                errors = validation.GetErrors();
                throw new CustomValidationException(errors);
            }

            try
            {
                horariosDosagem = CalcularHorariosDoses(request.horario_inicial_tratamento, request.intervalo_tratamento_horas);
                medicamentos = _medicamentoCreateMapper.BuscarMedicamentos();
                _medicineRepository.Create(medicamentos);
                tratamentos = _tratamentoCreateMapper.BuscarTratemento(request, medicamentos.id);

                _tratamentoRepository.Create(tratamentos);


                // Criar horários de dosagem e salvar no banco de dados
                foreach (string horario in horariosDosagem)
                {
                    HorariosDosagemCreateRequestModel horarioDosagemRequest = new HorariosDosagemCreateRequestModel
                    {
                        tratamento_id = tratamentos.id,
                        horario_dosagem = horario
                    };

                    // Verifica se já existe um horário de dosagem com os mesmos valores na tabela
                    horarioExistente = await _horarioDosagemDb.BuscarHorarioDosagemExistente(tratamentos.id, horario);
                    if (horarioExistente != null)
                    {
                        // O horário de dosagem já existe, pode optar por atualizar ou pular a inserção
                        continue; // Neste caso, optamos por pular a inserção se o horário já existir
                    }

                    horarioDosagem = _horarioDosagemCreateMapper.BuscarHorariosDosagem(horarioDosagemRequest);
                    _horarioDosagemRepository.Create(horarioDosagem);
                }

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

        /// <summary>
        /// Alterando medicamentos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="CustomValidationException"></exception>
        public async Task<bool> AlterandoMedicamentosETratamento (MedicamentoETratamentoUpdateRequestModel request)
        {
            MedicamentoETratamentoUpdateValidation validation;
            HorariosDosagemResponseModel horarioExistente;
            HorariosDosagem horarioDosagem;
            List<string> horariosDosagem;
            Dictionary<string, string> errors;
            bool successMedicamento;
            bool successTratamento;
            validation = new MedicamentoETratamentoUpdateValidation();
            if (!validation.IsValid(request))
            {
                errors = validation.GetErrors();
                throw new CustomValidationException(errors);
            }

            try
            {
                await _horarioDosagemDb.DeletandoHorarioDosagem(request.tratamento_id);
                horariosDosagem = CalcularHorariosDoses(request.horario_inicial_tratamento, request.intervalo_tratamento_horas);
                successMedicamento = await _medicamentoDb.AlterandoMedicamento(request.nome, request.data_vencimento_medicamento, request.quantidade_embalagem, request.dosagem, request.medicamento_id);
                if (successMedicamento)
                {
                    successTratamento = await _tratamentoDb.AlterandoTratamento(request.medicamento_id, request.nome, request.quantidade_medicamento_dosagem, request.horario_inicial_tratamento, request.intervalo_tratamento_horas, request.intervalo_tratamento_dias,
                    request.recomendacoes_alimentacao, request.observacao, request.tratamento_id);
                }
                if (successMedicamento)
                {
                    foreach (string horario in horariosDosagem)
                    {
                        HorariosDosagemCreateRequestModel horarioDosagemRequest = new HorariosDosagemCreateRequestModel
                        {
                            tratamento_id = request.tratamento_id,
                            horario_dosagem = horario
                        };

                        // Verifica se já existe um horário de dosagem com os mesmos valores na tabela
                        horarioExistente = await _horarioDosagemDb.BuscarHorarioDosagemExistente(request.tratamento_id, horario);
                        if (horarioExistente != null)
                        {
                            // O horário de dosagem já existe, pode optar por atualizar ou pular a inserção
                            continue; // Neste caso, optamos por pular a inserção se o horário já existir
                        }

                        horarioDosagem = _horarioDosagemCreateMapper.BuscarHorariosDosagem(horarioDosagemRequest);
                        _horarioDosagemRepository.Create(horarioDosagem);
                    }

                    return true;
                }
                else
                {
                    return false;
                }
              
            }
            catch (DbUpdateException exception)
            {
                errors = validation.GetPersistenceErrors(exception);
                throw new CustomValidationException(errors);
            }
        }
        
        /// <summary>
        /// Calculando as horas que o medicamento deve ser tomado a partir a hora inicial o intervalo de horas
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="intervaloEmHoras"></param>
        /// <returns></returns>
        private List<string> CalcularHorariosDoses(string startTime, int? intervaloEmHoras)
        {
            List<string> dosageTimes = new List<string>();
            DateTime startDateTime = DateTime.Parse(startTime);

            // Adiciona o horário inicial como a primeira dose
            dosageTimes.Add(startDateTime.ToString("HH:mm"));

            // Calcula os horários das doses seguintes com base no intervalo de horas
            for (int i = 1; i < 24 / intervaloEmHoras; i++)
            {
                DateTime nextDoseTime = startDateTime.AddHours((double)(intervaloEmHoras * i));
                dosageTimes.Add(nextDoseTime.ToString("HH:mm"));
            }

            return dosageTimes;
        }

        /// <summary>
        /// Obtém todos os medicamentos.
        /// </summary>
        public async Task<List<MedicamentoResponseModel>> BuscarTodosMedicamentos()
        {
            List<MedicamentoResponseModel> medicamentoLista;
            medicamentoLista = await _medicamentoDb.BuscarTodosMedicamentos();

            if (medicamentoLista == null || medicamentoLista.Count == 0)
            {
                throw new RecordNotFoundException();
            }
            return medicamentoLista;
        }

        /// <summary>
        /// Obtém medicamento por nome.
        /// </summary>
        public async Task<List<MedicamentoResponseModel>> BuscarMedicamentosPorNome(string name)
        {
            List<MedicamentoResponseModel> medicamentoLista;
            medicamentoLista = await _medicamentoDb.BuscarMedicamentosPorNome(name);

            if (medicamentoLista == null || medicamentoLista.Count == 0)
            {
                throw new RecordNotFoundException();
            }
            return medicamentoLista;
        }

        /// <summary>
        /// Obtém medicamento por data de vencimento.
        /// </summary>
        public async Task<List<MedicamentoResponseModel>> BuscarMedicamentosPorDataVencimento()
        {
            List<MedicamentoResponseModel> medicamentoLista;
            medicamentoLista = await _medicamentoDb.BuscarMedicamentosPorDataVencimento();

            if (medicamentoLista == null || medicamentoLista.Count == 0)
            {
                throw new RecordNotFoundException();
            }
            return medicamentoLista;
        }

        /// <summary>
        /// Deletando medicamentos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="RecordNotFoundException"></exception>
        public async Task<bool> DeletandoMedicamento(int id)
        {
            bool success;
            success = await _medicamentoDb.DeletandoMedicamento(id);

            if (!success)
            {
                throw new RecordNotFoundException();
            }
            return success;
        }
    }
}
