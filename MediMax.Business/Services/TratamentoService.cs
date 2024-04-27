using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediMax.Business.Exceptions;
using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Services.Interfaces;
using MediMax.Data.Dao;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Repositories.Interfaces;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services
{
    /// <summary>
    /// Serviço para operações relacionadas a tratamentos.
    /// </summary>
    public class TratamentoService : ITratamentoService
    {
        private readonly IMedicamentoCreateMapper _medicamentoCreateMapper;
        private readonly ITratamentoCreateMapper _tratamentoCreateMapper;
        private readonly IMedicamentosRepository _medicamentoRepository;
        private readonly ITratamentoRepository _tratamentoRepository;
        private readonly ITratamentoDb _tratamentoDb;

        /// <summary>
        /// Inicializa uma nova instância do serviço de tratamento.
        /// </summary>
        public TratamentoService(
            IMedicamentoCreateMapper medicamentoCreateMapper,
            ITratamentoCreateMapper tratamentoCreateMapper,
            IMedicamentosRepository medicamentoRepository,
            ITratamentoRepository tratamentoRepository,
            ITratamentoDb tratamentoDb)
        {
            _medicamentoCreateMapper = medicamentoCreateMapper ?? throw new ArgumentNullException(nameof(medicamentoCreateMapper));
            _medicamentoRepository = medicamentoRepository ?? throw new ArgumentNullException(nameof(medicamentoRepository));
            _tratamentoCreateMapper = tratamentoCreateMapper ?? throw new ArgumentNullException(nameof(tratamentoCreateMapper));
            _tratamentoRepository = tratamentoRepository ?? throw new ArgumentNullException(nameof(tratamentoRepository));
            _tratamentoDb = tratamentoDb ?? throw new ArgumentNullException(nameof(tratamentoDb));
        }

        /// <inheritdoc/>
        public async Task<List<TratamentoResponseModel>> BuscarTratamentoPorNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new ArgumentException("O nome do tratamento não pode ser nulo ou vazio.", nameof(nome));
            }

            List<TratamentoResponseModel> tratamentoLista;
            try
            {
                tratamentoLista = await _tratamentoDb.BuscarTratamentoPorNome(nome);
            }
            catch (RecordNotFoundException)
            {
                throw new RecordNotFoundException($"Nenhum tratamento encontrado com o nome '{nome}'.");
            }

            return tratamentoLista;
        }

        /// <inheritdoc/>
        public async Task<List<TratamentoResponseModel>> BuscarTratamentoPorIntervalo(string startTime, string finishTime)
        {
            if (string.IsNullOrEmpty(startTime) || string.IsNullOrEmpty(finishTime))
            {
                throw new ArgumentException("Os horários de início e término do intervalo não podem ser nulos ou vazios.");
            }

            List<TratamentoResponseModel> treatmentList;
            try
            {
                treatmentList = await _tratamentoDb.BuscarTratamentoPorIntervalo(startTime, finishTime);

                // Calcula os horários das doses seguintes para cada tratamento
                foreach (var treatment in treatmentList)
                {
                    if (treatment.StartTime != null && treatment.TreatmentInterval.HasValue)
                    {
                        treatment.DosageTime = CalcularHorariosDoses(treatment.StartTime, treatment.TreatmentInterval.Value);
                    }
                }
            }
            catch (RecordNotFoundException)
            {
                throw new RecordNotFoundException("Nenhum tratamento encontrado no intervalo especificado.");
            }

            return treatmentList;
        }

        private List<string> CalcularHorariosDoses(string startTime, int intervaloEmHoras)
        {
            List<string> dosageTimes = new List<string>();
            DateTime startDateTime = DateTime.Parse(startTime);

            // Adiciona o horário inicial como a primeira dose
            dosageTimes.Add(startDateTime.ToString("HH:mm"));

            // Calcula os horários das doses seguintes com base no intervalo de horas
            for (int i = 1; i < 24 / intervaloEmHoras; i++)
            {
                DateTime nextDoseTime = startDateTime.AddHours(intervaloEmHoras * i);
                dosageTimes.Add(nextDoseTime.ToString("HH:mm"));
            }

            return dosageTimes;
        }

        /// <summary>
        /// Deletando um tratemento
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="RecordNotFoundException"></exception>
        public async Task<bool> DeletandoTratamento(int id)
        {
            bool success;
            success = await _tratamentoDb.DeletandoTratamento(id);

            if (!success)
            {
                throw new RecordNotFoundException();
            }
            return success;
        }
    }
}
