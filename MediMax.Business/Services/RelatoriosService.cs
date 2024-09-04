using MediMax.Business.Services.Interfaces;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Repositories.Interfaces;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services
{
    public class RelatoriosService : IRelatoriosService
    {
        private readonly ITreatmentManagementRepository _TreatmentManagementRepository;
        private readonly ITreatmentDb _treatmentDb;
        private readonly ITreatmentManagementDbDb _historicoDb;
        private readonly IMedicationDb _medicationDb;
        private readonly INutritionDb _alimentacaoDb;

        public RelatoriosService(
            ITreatmentManagementRepository TreatmentManagementRepository,
            ITreatmentDb treatmentDb,
            ITreatmentManagementDbDb historicoDb,
            INutritionDb alimentacaoDb,
            IMedicationDb medicamentoDb )
        {
            _TreatmentManagementRepository = TreatmentManagementRepository;
            _treatmentDb = treatmentDb;
            _historicoDb = historicoDb;
            _medicationDb = medicamentoDb;
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

        public async Task<byte[]> PdfGenerator ( ReportRequestModel request )
        {

            /*
            1 - Medicamentos Cadastrados 
            2 - Medicamentos Excluidos
            3 - Treatments Cadastrados
            4 - Treatments Excluidos
            5 - Gerenciamento Treatment Todos
            6 - Gerenciamento Treatment Tomado
            7 - Gerenciamento Treatment Não Tomado
            8 - Gerenciamento Treatment Ultimos 7 dias
            9 - Gerenciamento Treatment Ultimos 15 dias
            10 - Gerenciamento Treatment Ultimos 30 dias
            11 - Gerenciamento Treatment Ultimos 60 dias
            12 - Gerenciamento Treatment Ultimos Ultimo Ano
            13 - Gerenciamento Treatment Ultimos Ano Especifico
            14 - Gerenciamento Treatment Ultimos Data Especifica
            15 - Refeições 
             */

            // Caminho onde o PDF será salvo
            string filePath = "C:/Users/Julia Fideles/Downloads/";

            List<TreatmentManagementResponseModel> historicoGeral;
            List<TreatmentManagementResponseModel> historicoTomado;
            List<TreatmentManagementResponseModel> historicoNaoTomado;
            List<TreatmentManagementResponseModel> historico7Dias;
            List<TreatmentManagementResponseModel> historico15Dias;
            List<TreatmentManagementResponseModel> historico30Dias;
            List<TreatmentManagementResponseModel> historico60Dias;
            List<TreatmentManagementResponseModel> historicoUltimoAno;
            List<NutritionResponseModel> alimentacao;
            List<NutritionGetResponseModel> alimentacaoGet;
            List<MedicationResponseModel> medicamentoAtivos;
            List<MedicationResponseModel> medicamentoInativos;
            List<TreatmentResponseModel> TreatmentAtivos;
            List<TreatmentResponseModel> TreatmentInativos;
            List<TreatmentManagementResponseModel> historicoAnoEspecifico;
            List<TreatmentManagementResponseModel> historicoDataEspecifica;
            List<TimeDosageResponseModel> horariosDosagem = null;

            MemoryStream memoryStream = null;

            RelatorioResponseModel response = new RelatorioResponseModel();
            switch (request.type)
            {
                case 0:
                    medicamentoAtivos = await _medicationDb.GetAllMedicine(request.userId);
                    var pdfGenerator1 = new PdfGenerator<MedicationResponseModel>();
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
                    medicamentoInativos = await _medicationDb.BuscarMedicamentosInativos(request.userId);
                    var pdfGenerator2 = new PdfGenerator<MedicationResponseModel>();
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
                    TreatmentAtivos = await _treatmentDb.GetTreatmentActives(request.userId);
                    foreach (var treatment in TreatmentAtivos)
                    {
                        if (treatment.Start_Time != null && treatment.Treatment_Interval_Hours != 0)
                        {
                            List<string> horariosDosagemString = CalcularHorariosDoses(treatment.Start_Time, treatment.Treatment_Interval_Hours);
                        }
                    }
                    var pdfGenerator3 = new PdfGenerator<TreatmentResponseModel>();
                    string fileName3 = filePath + "Treatments_Ativos.pdf";
                    Dictionary<string, string> columnNames3 = new Dictionary<string, string>
                    {
                        { "Name", "Medicamento" },
                        { "MedicineQuantity", "Quantidade de Medicamentos" },
                        { "StartTime", "Horário de Inicio do Treatment" },
                        { "TreatmentInterval", "Intervalo de Horas do Treatment" },
                        { "TreatmentDurationDays", "Intervalo de Dias do Treatment" },
                        { "DietaryRecommendations", "Recomendações de Alimentação" },
                        { "Observation", "Observação" },
                        { "DosageTime", "Horário Dosagem" }
                    };
                memoryStream = pdfGenerator3.GeneratePdf(TreatmentAtivos, fileName3, columnNames3, "Relatório de Treatments Ativos");
                    break;
                case 3:
                    TreatmentInativos = await _treatmentDb.GetTreatmentInactives(request.userId);
                    foreach (var treatment in TreatmentInativos)
                    {
                        if (treatment.Start_Time != null && treatment.Treatment_Interval_Hours != 0)
                        {
                            List<string> horariosDosagemString = CalcularHorariosDoses(treatment.Start_Time, treatment.Treatment_Interval_Hours);
                        }
                    }
                    var pdfGenerator4 = new PdfGenerator<TreatmentResponseModel>();
                    string fileName4 = filePath + "Treatments_Inativos.pdf";
                    Dictionary<string, string> columnNames4 = new Dictionary<string, string>
                    {
                        { "Name", "Medicamento" },
                        { "MedicineQuantity", "Quantidade de Medicamentos" },
                        { "StartTime", "Horário de Inicio do Treatment" },
                        { "TreatmentInterval", "Intervalo de Horas do Treatment" },
                        { "TreatmentDurationDays", "Intervalo de Dias do Treatment" },
                        { "DietaryRecommendations", "Recomendações de Alimentação" },
                        { "Observation", "Observação" },
                        { "DosageTime", "Horário Dosagem" }
                    };
                memoryStream = pdfGenerator4.GeneratePdf(TreatmentInativos, fileName4, columnNames4, "Relatório de Treatment Inativos");
                    break;
                case 4:
                    historicoGeral = await _historicoDb.GetAllHistoric(request.userId);
                    var pdfGenerator5 = new PdfGenerator<TreatmentManagementResponseModel>();
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
                    var pdfGenerator6 = new PdfGenerator<TreatmentManagementResponseModel>();
                    string fileName6 = filePath + "Historico_medications_Tomados.pdf";
                    Dictionary<string, string> columnNames6 = new Dictionary<string, string>
                        {
                            { "DateMedicationIntake", "Data de Ingestão" },
                            { "CorrectTreatmentSchedule", "Horário Correto" },
                            { "MedicationIntakeSchedule", "Horário de Ingestão" },
                            { "MedicineName", "Nome do Medicamento" }
                        };
                memoryStream = pdfGenerator6.GeneratePdf(historicoTomado, fileName6, columnNames6, "Relatório de Treatments Inativos");
                    break;
                case 6:
                    historicoNaoTomado = await _historicoDb.BuscarHistoricoNaoTomado(request.userId);
                    var pdfGenerator7 = new PdfGenerator<TreatmentManagementResponseModel>();
                    string fileName7 = filePath + "Historico_medications_Nao_Tomados.pdf";
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
                    var pdfGenerator8 = new PdfGenerator<TreatmentManagementResponseModel>();
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
                    var pdfGenerator9 = new PdfGenerator<TreatmentManagementResponseModel>();
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
                    var pdfGenerator10 = new PdfGenerator<TreatmentManagementResponseModel>();
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
                    var pdfGenerator11 = new PdfGenerator<TreatmentManagementResponseModel>();
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
                    var pdfGenerator12 = new PdfGenerator<TreatmentManagementResponseModel>();
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
                    var pdfGenerator13= new PdfGenerator<TreatmentManagementResponseModel>();
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
                    var pdfGenerator14 = new PdfGenerator<TreatmentManagementResponseModel>();
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
                    alimentacaoGet = await _alimentacaoDb.GetNutritionByUserId(request.userId);
                    var pdfGenerator15 = new PdfGenerator<NutritionGetResponseModel>();
                    string fileName15 = filePath + "Historico_Refeições.pdf";
                    Dictionary<string, string> columnNames15 = new Dictionary<string, string>
                    {
                        { "tipo_refeicao", "Tipo de Refeição" },
                        { "horario", "Horário" },
                        { "alimento", "Refeição" },
                        { "quantidade", "Quantidade" },
                        { "unidade_medida", "Unidade de Medida" }
                    };
                memoryStream = pdfGenerator15.GeneratePdf(alimentacaoGet, fileName15, columnNames15, "Relatório de Refeições");
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
