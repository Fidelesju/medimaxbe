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
    public class DispenserStatusService : IDispenserStatusService
    {
        private readonly IDispenserStatusCreateMapper _DispenserStatusCreateMapper;
        private readonly IDispenserStatusRepository _DispenserStatusRepository;
        private readonly IDispenserStatusDb _DispenserStatusDb;
        private readonly ITreatmentDb _TreatmentDb;
        private readonly IMedicationDb _medicationDb;
       
        public DispenserStatusService(
            IDispenserStatusCreateMapper DispenserStatusCreateMapper,
            IDispenserStatusDb DispenserStatusDb,
            ITreatmentDb TreatmentDb,
            IMedicationDb medicamentoDb,
            IDispenserStatusRepository DispenserStatusRepository
          )
        {
            _DispenserStatusCreateMapper = DispenserStatusCreateMapper;
            _DispenserStatusDb = DispenserStatusDb;
            _TreatmentDb = TreatmentDb;
            _medicationDb = medicamentoDb;
            _DispenserStatusRepository = DispenserStatusRepository;
           
        }

        /// <summary>
        /// Criando ou atualizando status do Treatment.
        /// </summary>
        public async Task<int> CriandoOuAtualizandoDispenserStatus( int treatmentId, int userId, int medicationId )
        {
            DispenserStatusCreateRequestModel request = new DispenserStatusCreateRequestModel();
            StatusDispenser DispenserStatus;
            DispenserStatusCreateValidation validation;
            DispenserStatusResponseModel existeAbastencimentoCadastrado;
            DateTime dataAtual = DateTime.Today;
            DateTime dataFinalPrevistaTreatment;
            TreatmentResponseModel treatment;
            MedicationResponseModel medication;

            
            //Busca o tratamento pelo id
            treatment = await _TreatmentDb.GetTreatmentById(treatmentId,userId);
            medication = await _medicationDb.GetMedicationById(medicationId, userId);

            if(treatment == null && medication == null)
            {
                return 0;
                
            }
            else
            {
                request.medicamento_id = medicationId;
                request.Treatment_id = treatmentId;
                request.intervalo_Treatment_horas = treatment.Treatment_Interval_Hours;
                request.intervalo_Treatment_dias = treatment.Treatment_Interval_Days;
                request.quantidade_total_medication_caixa = medication.package_quantity;
                request.user_id = medication.user_id;
                request.quantidade_medication_por_dosagem = treatment.Medication_Quantity;

                //Verifica se existe algum abastecimento cadastrado para esse tratamento
                existeAbastencimentoCadastrado = await _DispenserStatusDb.BuscandoSeExisteAbastacimentoCadastrado(treatmentId);

                //Calcula a frequencia de que o medicamento vai ser tomado diarimanete a partir do intervalo de dias do tratamento
                request.frenquecia_dosagem_diaria = CalcularFrequenciaDosagemDiaria(treatment.Treatment_Interval_Days);

                //Calcula a quantidade de medicamento 
                request.quantidade_total_medication_dosagem_dia = CalcularQuantidadeTotalMedicamentoDosagemDia(treatment.Medication_Quantity, request.frenquecia_dosagem_diaria);
                
                //Calcula a quantidade total de medicamentos por dosagem 
                request.quantidade_medication_semanal = CalcularQuantidadeTotalMedicamentoDosagemSemanal(request.frenquecia_dosagem_diaria);

                //Calcula a quantidade total de medicamentos do tratamento
                request.quantidade_total_medications_Treatment = CalcularQuantidadeTotalMedicamentosTreatment(request.frenquecia_dosagem_diaria, treatment.Treatment_Interval_Days);

                //Calcula a quantidade total de caixas do tratamento
                request.quantidade_total_caixa_Treatment =  CalcularQuantidadeTotalDeCaixasTreatment(medication.package_quantity, request.frenquecia_dosagem_diaria);

                if (existeAbastencimentoCadastrado == null )
                {
                        
                        request.quantidade_atual_medication_caixa_Treatment = request.quantidade_total_medication_caixa - request.quantidade_medication_semanal;
                        request.quantidade_medication_faltante_para_fim_Treatment = request.quantidade_atual_medication_caixa_Treatment;
                        request.quantidade_dias_faltante_para_fim_Treatment = request.intervalo_Treatment_dias;
                        request.data_criacao = dataAtual.ToString("dd/MM/yyyy");
                        request.data_atualizacao_semanal = dataAtual.ToString("dd/MM/yyyy");
                        request.data_inicio_Treatment = dataAtual.ToString("dd/MM/yyyy");
                        dataFinalPrevistaTreatment = dataAtual.AddDays(request.intervalo_Treatment_dias);
                        request.data_final_previsto_Treatment = dataFinalPrevistaTreatment.ToString("dd/MM/yyyy");
                        request.data_final_marcado_Treatment = "Ainda não finalizado";
                        request.status_Treatment = 1;
                        _DispenserStatusCreateMapper.SetBaseMapping(request);
                        DispenserStatus = _DispenserStatusCreateMapper.BuscarDispenserStatus();
                        _DispenserStatusRepository.Create(DispenserStatus);
                    }

                else
                {
                    request.quantidade_atual_medication_caixa_Treatment = CalcularQuantidadeAtualMedicamentoCaixaTreatment(existeAbastencimentoCadastrado.QuantidadeAtualMedicamentoCaixaTreatment, request.quantidade_medication_semanal);
                    request.quantidade_dias_faltante_para_fim_Treatment = CalcularQuantidadeDiasFaltanteParaFimTreatment(dataAtual.ToString("dd/MM/yyyy"), existeAbastencimentoCadastrado.DataFinalPrevistoTreatment);

                    //TODO Adicionar notificação que a caixa está vazia e que precisa adicionar novo
                    if (request.quantidade_atual_medication_caixa_Treatment < 0 && request.quantidade_dias_faltante_para_fim_Treatment > 7)
                        request.quantidade_atual_medication_caixa_Treatment = request.quantidade_total_medication_caixa;
                    else if (request.quantidade_atual_medication_caixa_Treatment < 0 && request.quantidade_dias_faltante_para_fim_Treatment < 7)
                        request.quantidade_atual_medication_caixa_Treatment = 0;

                    request.quantidade_medication_faltante_para_fim_Treatment = CalcularQuantidadeMedicamentoFaltanteParaFimTreatment(existeAbastencimentoCadastrado.QuantidadeMedicamentoFaltanteParaFimTreatment, request.quantidade_medication_semanal);

                    if (request.quantidade_medication_faltante_para_fim_Treatment <= existeAbastencimentoCadastrado.QuantidadeMedicamentoSemanal)
                        request.quantidade_medication_faltante_para_fim_Treatment = 0;

                    request.data_criacao = existeAbastencimentoCadastrado.DataCriacao;
                    request.data_atualizacao_semanal = dataAtual.ToString("dd/MM/yyyy");
                    request.data_inicio_Treatment = existeAbastencimentoCadastrado.DataInicioTreatment;
                    request.data_final_previsto_Treatment = existeAbastencimentoCadastrado.DataFinalPrevistoTreatment;

                    if(request.quantidade_dias_faltante_para_fim_Treatment <= 7)
                    {
                        request.data_final_marcado_Treatment = dataAtual.ToString("dd/MM/yyyy");
                        request.status_Treatment = 5;
                        _DispenserStatusCreateMapper.SetBaseMapping(request);
                        DispenserStatus = _DispenserStatusCreateMapper.BuscarDispenserStatus();
                        _DispenserStatusRepository.Create(DispenserStatus);
                        //_TreatmentDb.DeleteTreatment(request.Treatment_id);
                    }
                    else
                    {
                        request.data_final_marcado_Treatment = "Ainda não finalizado";
                        _DispenserStatusCreateMapper.SetBaseMapping(request);
                        DispenserStatus = _DispenserStatusCreateMapper.BuscarDispenserStatus();
                        _DispenserStatusRepository.Create(DispenserStatus);
                    }
                }
            }

                return DispenserStatus.Id;
        }

        public async Task<int> CalculadoraQuantidadeCaixasTreatment( CalculadoraCaixasRequestModel request)
        {
            int quantidadeTotalCaixasTreatment = 0;
            int frenquenciaDias = 0;
            int quantidadePorDia = 0;
            int quantidadeTotal = 0;
            int restoDivisao = 0;
            frenquenciaDias = CalcularFrequenciaDosagemDiaria(request.intervalo_tratamento_horas);
            quantidadePorDia = frenquenciaDias* request.quantidade_medicamento_por_dosagem;
            quantidadeTotal = quantidadePorDia* request.intervalo_tratamento_dias;
            quantidadeTotalCaixasTreatment = quantidadeTotal / request.quantidade_total_medicamento_caixa;
            restoDivisao = quantidadeTotal % request.quantidade_total_medicamento_caixa;

            if (restoDivisao != 0)
                quantidadeTotalCaixasTreatment++;
            return quantidadeTotalCaixasTreatment;
        }

        public int CalcularQuantidadeDiasFaltanteParaFimTreatment ( string dataInicioTreatment, string dataFinalPrevistaTreatment )
        {
            // Converter as strings de data para objetos DateTime
            DateTime inicioTreatment;
            DateTime finalPrevista;

            if (!DateTime.TryParseExact(dataInicioTreatment, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out inicioTreatment))
            {
                // Tratar erro de conversão
                return 0;
            }

            if (!DateTime.TryParseExact(dataFinalPrevistaTreatment, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out finalPrevista))
            {
                // Tratar erro de conversão
                return 0;
            }

            // Calcular a diferença em dias
            TimeSpan diferenca = finalPrevista - inicioTreatment;

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

        public int CalcularFrequenciaDosagemDiaria(int intervaloTreatmentEmHoras)
        {
            int intervalo = 24 / intervaloTreatmentEmHoras;
            return intervalo;
        }

        public int CalcularQuantidadeTotalMedicamentosTreatment(int quantidadeTotalMedicamentoDosagemDia, int intervaloTreatmentDias)
        {
            return quantidadeTotalMedicamentoDosagemDia * intervaloTreatmentDias;
        }

        public int CalcularQuantidadeTotalDeCaixasTreatment(int quantidadeTotalMedicamentoCaixa, int quantidadeTotalMedicamentosTreatment)
        {
            int quantidadeMedicamentoCaixaTreatment = quantidadeTotalMedicamentosTreatment/ quantidadeTotalMedicamentoCaixa;
            if (quantidadeTotalMedicamentoCaixa % quantidadeTotalMedicamentosTreatment != 0)
            {
                quantidadeMedicamentoCaixaTreatment++;
            }
            return quantidadeMedicamentoCaixaTreatment;
        }

        public int CalcularQuantidadeAtualMedicamentoCaixaTreatment(int quantidadeAtuallMedicamentoCaixaTreatment, int quantidadeMedicamentoSemanal)
        {
            return quantidadeAtuallMedicamentoCaixaTreatment - quantidadeMedicamentoSemanal;
        }

        public int CalcularQuantidadeMedicamentoFaltanteParaFimTreatment(int quantidadeMedicamentoFaltanteParaFimTreatment, int quantidadeMedicamentoSemanal)
        {
            return quantidadeMedicamentoFaltanteParaFimTreatment - quantidadeMedicamentoSemanal;
        }

        public async Task<DispenserStatusListaResponseModel> BuscandoDispenserStatus ( int treatmentId, int userId )
        {

            DispenserStatusListaResponseModel DispenserStatusResponse;
            TreatmentResponseModel TreatmentResponse;
            MedicationResponseModel medicamentoResponse;
            List<string> horarioDosagens = new List<string>();
            try
            {
                TreatmentResponse = await _TreatmentDb.BuscarTreatmentPorIdParaStatus(treatmentId, userId);
                medicamentoResponse = await _medicationDb.GetMedicationByTreatmentId(treatmentId, userId);

                if (TreatmentResponse != null)
                    horarioDosagens = CalcularHorariosDoses(TreatmentResponse.Start_Time, TreatmentResponse.Treatment_Interval_Hours);
                    DispenserStatusResponse = new DispenserStatusListaResponseModel
                    {
                        TreatmentId = TreatmentResponse.Id,
                        MedicamentoId = medicamentoResponse.id,
                        QuantidadeTotalMedicamentoCaixa = (int)medicamentoResponse.package_quantity,
                        QuantidadeDiasFaltantesFimTreatment = (int)TreatmentResponse.Treatment_Interval_Days,
                        IntervaloTreatmentHoras = (int)TreatmentResponse.Treatment_Interval_Hours,
                        QuantidadeMedicamentoPorDosagem = (int)TreatmentResponse.Medication_Quantity,
                        DosageTime = horarioDosagens
                    };
            }
            catch (RecordNotFoundException)
            {
                throw new RecordNotFoundException($"Nenhum Treatment encontrado com o id '{treatmentId}'.");
            }

            return DispenserStatusResponse;
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
