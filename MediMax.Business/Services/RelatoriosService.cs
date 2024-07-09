﻿using MediMax.Business.Mappers.Interfaces;
using MediMax.Business.Services.Interfaces;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Dao;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Repositories.Interfaces;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using ServiceStack.NativeTypes.Php;

namespace MediMax.Business.Services
{
    public class RelatoriosService : IRelatoriosService
    {
        private readonly IGerenciamentoTratamentoCreateMapper _gerenciamentoTratamentoCreateMapper;
        private readonly IGerenciamentoTratamentoRepository _gerenciamentoTratamentoRepository;
        private readonly ITratamentoDb _treatmentDb;
        private readonly IHistoricoDb _historicoDb;
        private readonly IMedicamentoDb _medicamentoDb;
        private readonly IAlimentacaoDb _alimentacaoDb;

        public RelatoriosService(
            IGerenciamentoTratamentoCreateMapper gerenciamentoTratamentoCreateMapper,
            IGerenciamentoTratamentoRepository gerenciamentoTratamentoRepository,
            ITratamentoDb treatmentDb,
            IHistoricoDb historicoDb,
            IAlimentacaoDb alimentacaoDb,
            IMedicamentoDb medicamentoDb )
        {
            _gerenciamentoTratamentoCreateMapper = gerenciamentoTratamentoCreateMapper;
            _gerenciamentoTratamentoRepository = gerenciamentoTratamentoRepository;
            _treatmentDb = treatmentDb;
            _historicoDb = historicoDb;
            _medicamentoDb = medicamentoDb;
            _alimentacaoDb = alimentacaoDb;
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

        public async Task<byte[]> GeradorPdf ( RelatorioRequestModel request )
        {

            /*
            1 - Medicamentos Cadastrados 
            2 - Medicamentos Excluidos
            3 - Tratamentos Cadastrados
            4 - Tratamentos Excluidos
            5 - Gerenciamento Tratamento Todos
            6 - Gerenciamento Tratamento Tomado
            7 - Gerenciamento Tratamento Não Tomado
            8 - Gerenciamento Tratamento Ultimos 7 dias
            9 - Gerenciamento Tratamento Ultimos 15 dias
            10 - Gerenciamento Tratamento Ultimos 30 dias
            11 - Gerenciamento Tratamento Ultimos 60 dias
            12 - Gerenciamento Tratamento Ultimos Ultimo Ano
            13 - Gerenciamento Tratamento Ultimos Ano Especifico
            14 - Gerenciamento Tratamento Ultimos Data Especifica
            15 - Refeições 
             */

            // Caminho onde o PDF será salvo
            string filePath = "C:/Users/Julia Fideles/Downloads/";

            List<HistoricoResponseModel> historicoGeral;
            List<HistoricoResponseModel> historicoTomado;
            List<HistoricoResponseModel> historicoNaoTomado;
            List<HistoricoResponseModel> historico7Dias;
            List<HistoricoResponseModel> historico15Dias;
            List<HistoricoResponseModel> historico30Dias;
            List<HistoricoResponseModel> historico60Dias;
            List<HistoricoResponseModel> historicoUltimoAno;
            List<AlimentacaoResponseModel> alimentacao;
            List<MedicamentoResponseModel> medicamentoAtivos;
            List<MedicamentoResponseModel> medicamentoInativos;
            List<TratamentoResponseModel> tratamentoAtivos;
            List<TratamentoResponseModel> tratamentoInativos;
            List<HistoricoResponseModel> historicoAnoEspecifico;
            List<HistoricoResponseModel> historicoDataEspecifica;

            MemoryStream memoryStream = null;

            RelatorioResponseModel response = new RelatorioResponseModel();
            switch (request.type)
            {
                case 0:
                    medicamentoAtivos = await _medicamentoDb.BuscarTodosMedicamentos(request.userId);
                    var pdfGenerator1 = new PdfGenerator<MedicamentoResponseModel>();
                    string fileName1 = filePath + "Medicamentos_Ativos.pdf";
                    Dictionary<string, string> columnNames1 = new Dictionary<string, string>
                    {
                        { "Name", "Medicamento" },
                        { "PackageQuantity", "Quantidade de Medicamentos da Caixa" },
                        { "Dosage", "Dosagem" },
                        { "ExpirationDate", "Data de Vencimento" }
                    };
                    memoryStream = pdfGenerator1.GeneratePdf(medicamentoAtivos, fileName1, columnNames1, "Relatório de Medicamentos Ativos");

                break;
                case 1:
                    medicamentoInativos = await _medicamentoDb.BuscarMedicamentosInativos(request.userId);
                    var pdfGenerator2 = new PdfGenerator<MedicamentoResponseModel>();
                    string fileName2 = filePath + "Medicamentos_Inativos.pdf";
                    Dictionary<string, string> columnNames2 = new Dictionary<string, string>
                        {
                            { "Name", "Medicamento" },
                            { "PackageQuantity", "Quantidade de Medicamentos da Caixa" },
                            { "Dosage", "Dosagem" },
                            { "ExpirationDate", "Data de Vencimento" }
                        };
                memoryStream = pdfGenerator2.GeneratePdf(medicamentoInativos, fileName2, columnNames2, "Relatório de Medicamentos Inativos");
                    break;
                case 2:
                    tratamentoAtivos = await _treatmentDb.BuscarTodosTratamentoAtivos(request.userId);
                    foreach (var treatment in tratamentoAtivos)
                    {
                        if (treatment.StartTime != null && treatment.TreatmentInterval.HasValue)
                        {
                            treatment.DosageTime = CalcularHorariosDoses(treatment.StartTime, treatment.TreatmentInterval.Value);
                        }
                    }
                    var pdfGenerator3 = new PdfGenerator<TratamentoResponseModel>();
                    string fileName3 = filePath + "Tratamentos_Ativos.pdf";
                    Dictionary<string, string> columnNames3 = new Dictionary<string, string>
                    {
                        { "Name", "Medicamento" },
                        { "MedicineQuantity", "Quantidade de Medicamentos" },
                        { "StartTime", "Horário de Inicio do Tratamento" },
                        { "TreatmentInterval", "Intervalo de Horas do Tratamento" },
                        { "TreatmentDurationDays", "Intervalo de Dias do Tratamento" },
                        { "DietaryRecommendations", "Recomendações de Alimentação" },
                        { "Observation", "Observação" },
                        { "DosageTime", "Horário Dosagem" }
                    };
                memoryStream = pdfGenerator3.GeneratePdf(tratamentoAtivos, fileName3, columnNames3, "Relatório de Tratamentos Ativos");
                    break;
                case 3:
                    tratamentoInativos = await _treatmentDb.BuscarTodosTratamentoInativos(request.userId);
                    foreach (var treatment in tratamentoInativos)
                    {
                        if (treatment.StartTime != null && treatment.TreatmentInterval.HasValue)
                        {
                            treatment.DosageTime = CalcularHorariosDoses(treatment.StartTime, treatment.TreatmentInterval.Value);
                        }
                    }
                    var pdfGenerator4 = new PdfGenerator<TratamentoResponseModel>();
                    string fileName4 = filePath + "Tratamentos_Inativos.pdf";
                    Dictionary<string, string> columnNames4 = new Dictionary<string, string>
                    {
                        { "Name", "Medicamento" },
                        { "MedicineQuantity", "Quantidade de Medicamentos" },
                        { "StartTime", "Horário de Inicio do Tratamento" },
                        { "TreatmentInterval", "Intervalo de Horas do Tratamento" },
                        { "TreatmentDurationDays", "Intervalo de Dias do Tratamento" },
                        { "DietaryRecommendations", "Recomendações de Alimentação" },
                        { "Observation", "Observação" },
                        { "DosageTime", "Horário Dosagem" }
                    };
                memoryStream = pdfGenerator4.GeneratePdf(tratamentoInativos, fileName4, columnNames4, "Relatório de Tratamento Inativos");
                    break;
                case 4:
                    historicoGeral = await _historicoDb.BuscarHistoricoGeral(request.userId);
                    var pdfGenerator5 = new PdfGenerator<HistoricoResponseModel>();
                    string fileName5 = filePath + "Historico_Geral.pdf";
                    Dictionary<string, string> columnNames5 = new Dictionary<string, string>
                    {
                        { "DateMedicationIntake", "Data de Ingestão" },
                        { "CorrectTreatmentSchedule", "Horário Correto" },
                        { "MedicationIntakeSchedule", "Horário de Ingestão" },
                        { "MedicineName", "Nome do Medicamento" },
                        { "WasTakenDescription", "Foi Tomado" }
                    };
                memoryStream = pdfGenerator5.GeneratePdf(historicoGeral, fileName5, columnNames5, "Relatório de Geral");
                    break;
                case 5:
                    historicoTomado = await _historicoDb.BuscarHistoricoTomado(request.userId);
                    var pdfGenerator6 = new PdfGenerator<HistoricoResponseModel>();
                    string fileName6 = filePath + "Historico_Medicamentos_Tomados.pdf";
                    Dictionary<string, string> columnNames6 = new Dictionary<string, string>
                        {
                            { "DateMedicationIntake", "Data de Ingestão" },
                            { "CorrectTreatmentSchedule", "Horário Correto" },
                            { "MedicationIntakeSchedule", "Horário de Ingestão" },
                            { "MedicineName", "Nome do Medicamento" }
                        };
                memoryStream = pdfGenerator6.GeneratePdf(historicoTomado, fileName6, columnNames6, "Relatório de Tratamentos Inativos");
                    break;
                case 6:
                    historicoNaoTomado = await _historicoDb.BuscarHistoricoNaoTomado(request.userId);
                    var pdfGenerator7 = new PdfGenerator<HistoricoResponseModel>();
                    string fileName7 = filePath + "Historico_Medicamentos_Nao_Tomados.pdf";
                    Dictionary<string, string> columnNames7 = new Dictionary<string, string>
                            {
                                { "DateMedicationIntake", "Data de Ingestão" },
                                { "CorrectTreatmentSchedule", "Horário Correto" },
                                { "MedicationIntakeSchedule", "Horário de Ingestão" },
                                { "MedicineName", "Nome do Medicamento" }
                            };
                    pdfGenerator7.GeneratePdf(historicoNaoTomado, fileName7, columnNames7, "Relatório de Medicamentos Não Tomados");
                    break;
                case 7:
                    historico7Dias = await _historicoDb.BuscarHistorico7Dias(request.userId);
                    var pdfGenerator8 = new PdfGenerator<HistoricoResponseModel>();
                    string fileName8 = filePath + "Historico_Ultimos_7_Dias.pdf";
                    Dictionary<string, string> columnNames8 = new Dictionary<string, string>
                    {
                        { "DateMedicationIntake", "Data de Ingestão" },
                        { "CorrectTreatmentSchedule", "Horário Correto" },
                        { "MedicationIntakeSchedule", "Horário de Ingestão" },
                        { "MedicineName", "Nome do Medicamento" },
                        { "WasTakenDescription", "Foi Tomado" }
                    };
                memoryStream = pdfGenerator8.GeneratePdf(historico7Dias, fileName8, columnNames8, "Relatório de Ultimos 7 dias");
                    break;
                case 8:
                    historico15Dias = await _historicoDb.BuscarHistorico15Dias(request.userId);
                    var pdfGenerator9 = new PdfGenerator<HistoricoResponseModel>();
                    string fileName9 = filePath + "Historico_Ultimos_15_Dias.pdf";
                    Dictionary<string, string> columnNames9 = new Dictionary<string, string>
                        {
                            { "DateMedicationIntake", "Data de Ingestão" },
                            { "CorrectTreatmentSchedule", "Horário Correto" },
                            { "MedicationIntakeSchedule", "Horário de Ingestão" },
                            { "MedicineName", "Nome do Medicamento" },
                            { "WasTakenDescription", "Foi Tomado" }
                        };
                memoryStream = pdfGenerator9.GeneratePdf(historico15Dias, fileName9, columnNames9, "Relatório de Ultimos 15 dias");
                    break;
                case 9:
                    historico30Dias = await _historicoDb.BuscarHistorico30Dias(request.userId);
                    var pdfGenerator10 = new PdfGenerator<HistoricoResponseModel>();
                    string fileName10 = filePath + "Historico_Ultimos_30_Dias.pdf";
                    Dictionary<string, string> columnNames10 = new Dictionary<string, string>
                            {
                                { "DateMedicationIntake", "Data de Ingestão" },
                                { "CorrectTreatmentSchedule", "Horário Correto" },
                                { "MedicationIntakeSchedule", "Horário de Ingestão" },
                                { "MedicineName", "Nome do Medicamento" },
                                { "WasTakenDescription", "Foi Tomado" }
                            };
                memoryStream = pdfGenerator10.GeneratePdf(historico30Dias, fileName10, columnNames10, "Relatório de Ultimos 30 dias");
                    break;
                case 10:
                    historico60Dias = await _historicoDb.BuscarHistorico60Dias(request.userId);
                    var pdfGenerator11 = new PdfGenerator<HistoricoResponseModel>();
                    string fileName11 = filePath + "Historico_Ultimos_60_Dias.pdf";
                    Dictionary<string, string> columnNames11 = new Dictionary<string, string>
                                {
                                    { "DateMedicationIntake", "Data de Ingestão" },
                                    { "CorrectTreatmentSchedule", "Horário Correto" },
                                    { "MedicationIntakeSchedule", "Horário de Ingestão" },
                                    { "MedicineName", "Nome do Medicamento" },
                                    { "WasTakenDescription", "Foi Tomado" }
                                };
                memoryStream = pdfGenerator11.GeneratePdf(historico60Dias, fileName11, columnNames11, "Relatório de Ultimos 60 dias");
                    break;
                case 11:
                    historicoUltimoAno = await _historicoDb.BuscarHistoricoUltimoAno(request.userId);
                    var pdfGenerator12 = new PdfGenerator<HistoricoResponseModel>();
                    string fileName12 = filePath + "Historico_Ultimo_Ano.pdf";
                    Dictionary<string, string> columnNames12 = new Dictionary<string, string>
                    {
                        { "DateMedicationIntake", "Data de Ingestão" },
                        { "CorrectTreatmentSchedule", "Horário Correto" },
                        { "MedicationIntakeSchedule", "Horário de Ingestão" },
                        { "MedicineName", "Nome do Medicamento" },
                        { "WasTakenDescription", "Foi Tomado" }
                    };

                memoryStream = pdfGenerator12.GeneratePdf(historicoUltimoAno, fileName12, columnNames12, "Relatório de Ultimo Ano");
                    break;
                case 12:
                    historicoAnoEspecifico = await _historicoDb.BuscarHistoricoAnoEspecifico(request.year, request.userId);
                    var pdfGenerator13= new PdfGenerator<HistoricoResponseModel>();
                    string fileName13= filePath + "Historico_Ano_" +request.year + ".pdf";
                    Dictionary<string, string> columnNames13= new Dictionary<string, string>
                    {
                        { "DateMedicationIntake", "Data de Ingestão" },
                        { "CorrectTreatmentSchedule", "Horário Correto" },
                        { "MedicationIntakeSchedule", "Horário de Ingestão" },
                        { "MedicineName", "Nome do Medicamento" },
                        { "WasTakenDescription", "Foi Tomado" }
                    };
                memoryStream = pdfGenerator13.GeneratePdf(historicoAnoEspecifico, fileName13, columnNames13, "Relatório " + request.year);
                    break;
                case 13:
                    historicoDataEspecifica = await _historicoDb.BuscarHistoricoDataEspecifica(request.date, request.userId);
                    var pdfGenerator14 = new PdfGenerator<HistoricoResponseModel>();
                    string fileName14 = filePath + "Historico.pdf";
                    Dictionary<string, string> columnNames14 = new Dictionary<string, string>
                    {
                        { "DateMedicationIntake", "Data de Ingestão" },
                        { "CorrectTreatmentSchedule", "Horário Correto" },
                        { "MedicationIntakeSchedule", "Horário de Ingestão" },
                        { "MedicineName", "Nome do Medicamento" },
                        { "WasTakenDescription", "Foi Tomado" }
                    };
                memoryStream = pdfGenerator14.GeneratePdf(historicoDataEspecifica, fileName14, columnNames14, "Relatório " + request.date);
                    break;
                case 14:
                    alimentacao = await _alimentacaoDb.BuscarTodasAlimentacao(request.userId);
                    var pdfGenerator15 = new PdfGenerator<AlimentacaoResponseModel>();
                    string fileName15 = filePath + "Historico_Refeições.pdf";
                    Dictionary<string, string> columnNames15 = new Dictionary<string, string>
                    {
                        { "tipo_refeicao", "Tipo de Refeição" },
                        { "horario", "Horário" },
                        { "alimento", "Refeição" },
                        { "quantidade", "Quantidade" },
                        { "unidade_medida", "Unidade de Medida" }
                    };
                memoryStream = pdfGenerator15.GeneratePdf(alimentacao, fileName15, columnNames15, "Relatório de Refeições");
                    break;
            }

            if (memoryStream != null)
            {
                return memoryStream.ToArray();
            }
            else
            {
                return null;
            }
        }
    }
}
