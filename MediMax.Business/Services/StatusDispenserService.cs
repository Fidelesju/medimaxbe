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
    public class StatusDispenserService : IStatusDispenserService
    {
        private readonly IStatusDispenserCreateMapper _statusDispenserCreateMapper;
        private readonly IStatusDispenserRepository _statusDispenserRepository;
        private readonly IStatusDispenserDb _statusDispenserDb;
        private readonly ITratamentoDb _tratamentoDb;
        private readonly IMedicamentoDb _medicamentoDb;
       
        public StatusDispenserService(
            IStatusDispenserCreateMapper statusDispenserCreateMapper,
            IStatusDispenserDb statusDispenserDb,
            ITratamentoDb tratamentoDb,
            IMedicamentoDb medicamentoDb,
            IStatusDispenserRepository statusDispenserRepository
          )
        {
            _statusDispenserCreateMapper = statusDispenserCreateMapper;
            _statusDispenserDb = statusDispenserDb;
            _tratamentoDb = tratamentoDb;
            _medicamentoDb = medicamentoDb;
            _statusDispenserRepository = statusDispenserRepository;
           
           
        }

        /// <summary>
        /// Criando ou atualizando status do tratamento.
        /// </summary>
        public async Task<int> CriandoOuAtualizandoStatusDispenser(StatusDispenserCreateRequestModel request)
        {
            StatusDispenser statusDispenser;
            StatusDispenserCreateValidation validation;
            StatusDispenserResponseModel existeAbastencimentoCadastrado;
            DateTime dataAtual = DateTime.Today;
            DateTime dataFinalPrevistaTratamento;

            Dictionary<string, string> errors;

            validation = new StatusDispenserCreateValidation();
            if (!validation.IsValid(request))
            {
                errors = validation.GetErrors();
                throw new CustomValidationException(errors);
            }

            try
            {
                existeAbastencimentoCadastrado = await _statusDispenserDb.BuscandoSeExisteAbastacimentoCadastrado(request.tratamento_id);
                request.frenquecia_dosagem_diaria = CalcularFrequenciaDosagemDiaria(request.intervalo_tratamento_horas);
                request.quantidade_total_medicamento_dosagem_dia = CalcularQuantidadeTotalMedicamentoDosagemDia(request.quantidade_medicamento_por_dosagem, request.frenquecia_dosagem_diaria);
                request.quantidade_medicamento_semanal = CalcularQuantidadeTotalMedicamentoDosagemSemanal(request.frenquecia_dosagem_diaria);
                request.quantidade_total_medicamentos_tratamento = CalcularQuantidadeTotalMedicamentosTratamento(request.frenquecia_dosagem_diaria, request.intervalo_tratamento_dias);
                request.quantidade_total_caixa_tratamento =  CalcularQuantidadeTotalDeCaixasTratamento(request.quantidade_total_medicamento_caixa, request.frenquecia_dosagem_diaria);

                if (existeAbastencimentoCadastrado == null )
                {
                    request.quantidade_atual_medicamento_caixa_tratamento = request.quantidade_total_medicamento_caixa - request.quantidade_medicamento_semanal;
                    request.quantidade_medicamento_faltante_para_fim_tratamento = request.quantidade_atual_medicamento_caixa_tratamento;
                    request.quantidade_dias_faltante_para_fim_tratamento = request.intervalo_tratamento_dias;
                    request.data_criacao = dataAtual.ToString("dd/MM/yyyy");
                    request.data_atualizacao_semanal = dataAtual.ToString("dd/MM/yyyy");
                    request.data_inicio_tratamento = dataAtual.ToString("dd/MM/yyyy");
                    dataFinalPrevistaTratamento = dataAtual.AddDays(request.intervalo_tratamento_dias);
                    request.data_final_previsto_tratamento = dataFinalPrevistaTratamento.ToString("dd/MM/yyyy");
                    request.data_final_marcado_tratamento = "Ainda não finalizado";
                    request.status_tratamento = 1;
                    _statusDispenserCreateMapper.SetBaseMapping(request);
                    statusDispenser = _statusDispenserCreateMapper.BuscarStatusDispenser();
                    _statusDispenserRepository.Create(statusDispenser);
                }
                else
                {
                    request.quantidade_atual_medicamento_caixa_tratamento = CalcularQuantidadeAtualMedicamentoCaixaTratamento(existeAbastencimentoCadastrado.QuantidadeAtualMedicamentoCaixaTratamento, request.quantidade_medicamento_semanal);
                    request.quantidade_dias_faltante_para_fim_tratamento = CalcularQuantidadeDiasFaltanteParaFimTratamento(dataAtual.ToString("dd/MM/yyyy"), existeAbastencimentoCadastrado.DataFinalPrevistoTratamento);

                    //TODO Adicionar notificação que a caixa está vazia e que precisa adicionar novo
                    if (request.quantidade_atual_medicamento_caixa_tratamento < 0 && request.quantidade_dias_faltante_para_fim_tratamento > 7)
                        request.quantidade_atual_medicamento_caixa_tratamento = request.quantidade_total_medicamento_caixa;
                    else if (request.quantidade_atual_medicamento_caixa_tratamento < 0 && request.quantidade_dias_faltante_para_fim_tratamento < 7)
                        request.quantidade_atual_medicamento_caixa_tratamento = 0;

                    request.quantidade_medicamento_faltante_para_fim_tratamento = CalcularQuantidadeMedicamentoFaltanteParaFimTratamento(existeAbastencimentoCadastrado.QuantidadeMedicamentoFaltanteParaFimTratamento, request.quantidade_medicamento_semanal);

                    if (request.quantidade_medicamento_faltante_para_fim_tratamento <= existeAbastencimentoCadastrado.QuantidadeMedicamentoSemanal)
                        request.quantidade_medicamento_faltante_para_fim_tratamento = 0;

                    request.data_criacao = existeAbastencimentoCadastrado.DataCriacao;
                    request.data_atualizacao_semanal = dataAtual.ToString("dd/MM/yyyy");
                    request.data_inicio_tratamento = existeAbastencimentoCadastrado.DataInicioTratamento;
                    request.data_final_previsto_tratamento = existeAbastencimentoCadastrado.DataFinalPrevistoTratamento;

                    if(request.quantidade_dias_faltante_para_fim_tratamento <= 7)
                    {
                        request.data_final_marcado_tratamento = dataAtual.ToString("dd/MM/yyyy");
                        request.status_tratamento = 5;
                        _statusDispenserCreateMapper.SetBaseMapping(request);
                        statusDispenser = _statusDispenserCreateMapper.BuscarStatusDispenser();
                        _statusDispenserRepository.Create(statusDispenser);
                        _tratamentoDb.DeletandoTratamento(request.tratamento_id);
                    }
                    else
                    {
                        request.data_final_marcado_tratamento = "Ainda não finalizado";
                        _statusDispenserCreateMapper.SetBaseMapping(request);
                        statusDispenser = _statusDispenserCreateMapper.BuscarStatusDispenser();
                        _statusDispenserRepository.Create(statusDispenser);
                    }
                }

                return statusDispenser.id;
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

        public async Task<int> CalculadoraQuantidadeCaixasTratamento( CalculadoraCaixasRequestModel request)
        {
            int quantidadeTotalCaixasTratamento = 0;
            int frenquenciaDias = 0;
            int quantidadePorDia = 0;
            int quantidadeTotal = 0;
            int restoDivisao = 0;
            frenquenciaDias = CalcularFrequenciaDosagemDiaria(request.intervalo_tratamento_horas);
            quantidadePorDia = frenquenciaDias* request.quantidade_medicamento_por_dosagem;
            quantidadeTotal = quantidadePorDia* request.intervalo_tratamento_dias;
            quantidadeTotalCaixasTratamento = quantidadeTotal / request.quantidade_total_medicamento_caixa;
            restoDivisao = quantidadeTotal % request.quantidade_total_medicamento_caixa;

            if (restoDivisao != 0)
                quantidadeTotalCaixasTratamento++;
            return quantidadeTotalCaixasTratamento;
        }

        public int CalcularQuantidadeDiasFaltanteParaFimTratamento ( string dataInicioTratamento, string dataFinalPrevistaTratamento )
        {
            // Converter as strings de data para objetos DateTime
            DateTime inicioTratamento;
            DateTime finalPrevista;

            if (!DateTime.TryParseExact(dataInicioTratamento, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out inicioTratamento))
            {
                // Tratar erro de conversão
                return 0;
            }

            if (!DateTime.TryParseExact(dataFinalPrevistaTratamento, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out finalPrevista))
            {
                // Tratar erro de conversão
                return 0;
            }

            // Calcular a diferença em dias
            TimeSpan diferenca = finalPrevista - inicioTratamento;

            // Retornar o número de dias como um valor inteiro
            return (int)diferenca.TotalDays;
        }

        public int CalcularQuantidadeTotalMedicamentoDosagemDia(int quantidadeMedicamentoDosagem, int frequenciaDosagemDiaria)
        {
            return quantidadeMedicamentoDosagem * frequenciaDosagemDiaria;
        }
        
        public int CalcularQuantidadeTotalMedicamentoDosagemSemanal(int quantidadeTotalMedicamentoDosagemDia)
        {
            return quantidadeTotalMedicamentoDosagemDia * 7;
        }

        public int CalcularFrequenciaDosagemDiaria(int intervaloTratamentoEmHoras)
        {
            int intervalo = 24 / intervaloTratamentoEmHoras;
            return intervalo;
        }

        public int CalcularQuantidadeTotalMedicamentosTratamento(int quantidadeTotalMedicamentoDosagemDia, int intervaloTratamentoDias)
        {
            return quantidadeTotalMedicamentoDosagemDia * intervaloTratamentoDias;
        }

        public int CalcularQuantidadeTotalDeCaixasTratamento(int quantidadeTotalMedicamentoCaixa, int quantidadeTotalMedicamentosTratamento)
        {
            int quantidadeMedicamentoCaixaTratamento = quantidadeTotalMedicamentosTratamento/ quantidadeTotalMedicamentoCaixa;
            if (quantidadeTotalMedicamentoCaixa % quantidadeTotalMedicamentosTratamento != 0)
            {
                quantidadeMedicamentoCaixaTratamento++;
            }
            return quantidadeMedicamentoCaixaTratamento;
        }

        public int CalcularQuantidadeAtualMedicamentoCaixaTratamento(int quantidadeAtuallMedicamentoCaixaTratamento, int quantidadeMedicamentoSemanal)
        {
            return quantidadeAtuallMedicamentoCaixaTratamento - quantidadeMedicamentoSemanal;
        }

        public int CalcularQuantidadeMedicamentoFaltanteParaFimTratamento(int quantidadeMedicamentoFaltanteParaFimTratamento, int quantidadeMedicamentoSemanal)
        {
            return quantidadeMedicamentoFaltanteParaFimTratamento - quantidadeMedicamentoSemanal;
        }

        public async Task<StatusDispenserListaResponseModel> BuscandoStatusDispenser ( int treatmentId, int userId )
        {

            StatusDispenserListaResponseModel statusDispenserResponse;
            TratamentoResponseModel tratamentoResponse;
            MedicamentoResponseModel medicamentoResponse;
            List<string> horarioDosagens = new List<string>();
            try
            {
                tratamentoResponse = await _tratamentoDb.BuscarTratamentoPorIdParaStatus(treatmentId, userId);
                medicamentoResponse = await _medicamentoDb.BuscarMedicamentosPorTratamento(treatmentId, userId);

                if (tratamentoResponse != null)
                    horarioDosagens = CalcularHorariosDoses(tratamentoResponse.StartTime, tratamentoResponse.TreatmentInterval.Value);
                    statusDispenserResponse = new StatusDispenserListaResponseModel
                    {
                        TratamentoId = tratamentoResponse.Id,
                        MedicamentoId = medicamentoResponse.Id,
                        QuantidadeTotalMedicamentoCaixa = (int)medicamentoResponse.PackageQuantity,
                        QuantidadeDiasFaltantesFimTratamento = (int)tratamentoResponse.TreatmentDurationDays,
                        IntervaloTratamentoHoras = (int)tratamentoResponse.TreatmentInterval,
                        QuantidadeMedicamentoPorDosagem = (int)tratamentoResponse.MedicineQuantity,
                        DosageTime = horarioDosagens
                    };
            }
            catch (RecordNotFoundException)
            {
                throw new RecordNotFoundException($"Nenhum tratamento encontrado com o id '{treatmentId}'.");
            }

            return statusDispenserResponse;
        }
        
        private List<string> CalcularHorariosDoses ( string startTime, int intervaloEmHoras )
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
    }
}
