﻿using iTextSharp.text;
using iTextSharp.text.pdf;

namespace MediMax.Data.ApplicationModels
{
    public class PdfGenerator<TModel>
    {
        private readonly string _logoPath = "C:/Users/Julia Fideles/OneDrive/Documentos/Documentos/Nexus/Nexus/Icones Coloridos/favicon-32x32.png";

        public PdfGenerator ( )
        {
        }

        public MemoryStream GeneratePdf ( List<TModel> data, string fileName, Dictionary<string, string> columnNames, string title )
        {
            // Criar um MemoryStream para armazenar o PDF
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Criação do documento PDF
                using (Document document = new Document())
                {
                    PdfWriter.GetInstance(document, memoryStream);
                    document.Open();

                    // Adicionando cabeçalho com logo e nome do projeto
                    PdfPTable headerTable = new PdfPTable(2);
                    headerTable.WidthPercentage = 100;
                    headerTable.DefaultCell.Border = Rectangle.NO_BORDER;

                    // Logo
                    PdfPCell logoCell = new PdfPCell(Image.GetInstance(_logoPath));
                    logoCell.Border = Rectangle.NO_BORDER;
                    headerTable.AddCell(logoCell);

                    // Nome do projeto
                    PdfPCell projectNameCell = new PdfPCell(new Phrase(fileName));
                    projectNameCell.Border = Rectangle.NO_BORDER;
                    projectNameCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.BLACK);
                    projectNameCell.Phrase.Font = boldFont;
                    headerTable.AddCell(projectNameCell);

                    document.Add(headerTable);

                    // Adicionando título personalizado
                    Font customTitleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLACK);
                    Paragraph customTitle = new Paragraph(title, customTitleFont);
                    customTitle.Alignment = Element.ALIGN_CENTER;
                    document.Add(customTitle);


                    // Adicionando espaço antes da tabela
                    document.Add(new Paragraph("\n"));

                    // Adicionando dados em uma tabela
                    PdfPTable table = new PdfPTable(columnNames.Count);
                    table.WidthPercentage = 100;

                    // Adicionando cabeçalhos estilizados
                    foreach (var columnName in columnNames.Values)
                    {
                        PdfPCell headerCell = new PdfPCell(new Phrase(columnName, new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD, BaseColor.WHITE)));
                        headerCell.BackgroundColor = new BaseColor(0, 21, 36); // #001524
                        headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        headerCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        table.AddCell(headerCell);
                    }
                    // Definir as cores para as células
                    BaseColor cellBackgroundColor1 = new BaseColor(51, 101, 138); // #001524
                    BaseColor cellBackgroundColor2 = new BaseColor(0, 122, 204); // #007ACC

                    // Variável para alternar entre as cores
                    bool isEvenRow = false;

                    // Adicionando linhas de dados
                    foreach (var item in data)
                    {
                        // Alternar entre as cores das células
                        BaseColor currentCellBackgroundColor = isEvenRow ? cellBackgroundColor1 : cellBackgroundColor2;

                        foreach (var columnName in columnNames.Keys)
                        {
                            // Usa reflexão para acessar as propriedades do item
                            var propertyInfo = item.GetType().GetProperty(columnName);

                            if (propertyInfo != null)
                            {
                                var value = propertyInfo.GetValue(item, null);

                                // Se for a coluna de "Dosage_Times", converta a lista de horários para string
                                if (columnName == "Dosage_Times" && value is List<string> dosageTimes)
                                {
                                    string dosageTimesString = string.Join(", ", dosageTimes);

                                    // Cria a célula com os horários de dosagem
                                    PdfPCell dosageTimeCell = new PdfPCell(new Phrase(dosageTimesString, new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.WHITE)));
                                    dosageTimeCell.BackgroundColor = currentCellBackgroundColor;
                                    dosageTimeCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    dosageTimeCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                    table.AddCell(dosageTimeCell);
                                }
                                else
                                {
                                    // Trata as outras colunas
                                    string cellValue = value?.ToString() ?? "";
                                    PdfPCell cell = new PdfPCell(new Phrase(cellValue, new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.WHITE)));
                                    cell.BackgroundColor = currentCellBackgroundColor;
                                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                    table.AddCell(cell);
                                }
                            }
                        }

                        // Alternar entre as cores das linhas
                        isEvenRow = !isEvenRow;
                    }

                    document.Add(table);
                }
                return memoryStream;
            }
        }

        private string GetPropertyValue ( TModel item, string propertyName )
        {
            var property = typeof(TModel).GetProperty(propertyName);
            if (property != null)
            {
                return property.GetValue(item)?.ToString() ?? "";
            }
            return "";
        }
    }
}
