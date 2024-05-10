﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Collections.Generic;

namespace MediMax.Data.ApplicationModels
{
    public class PdfGenerator<TModel>
    {
        private readonly string _logoPath = "C:/Users/Julia Fideles/OneDrive/Documentos/Documentos/Nexus/Nexus/Icones Coloridos/favicon-32x32.png";

        public PdfGenerator ( )
        {
        }

        public void GeneratePdf ( List<TModel> data, string filePath, Dictionary<string, string> columnNames )
        {
            // Definindo as cores padrão
            BaseColor headerBackgroundColor = new BaseColor(0, 21, 36); // #F18805
            BaseColor headerTextColor = BaseColor.WHITE;
            BaseColor cellBackgroundColor = new BaseColor(0, 21, 36); // #001524
            BaseColor cellTextColor = BaseColor.BLACK;

            // Criação do documento PDF
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
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
            // Criar a célula com o nome do projeto
            PdfPCell projectNameCell = new PdfPCell(new Phrase("MediMax"));

            // Definir a borda da célula como nenhuma
            projectNameCell.Border = Rectangle.NO_BORDER;

            // Alinhar o texto à direita na célula
            projectNameCell.HorizontalAlignment = Element.ALIGN_RIGHT;

            // Configurar a fonte para negrito e maior
            Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.BLACK);
            projectNameCell.Phrase.Font = boldFont;

            // Adicionar a célula à tabela
            headerTable.AddCell(projectNameCell);

            document.Add(headerTable);

            // Adicionando título
            Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLACK);
            Paragraph title = new Paragraph($"Relatório PDF de {typeof(TModel).Name}", titleFont);
            title.Alignment = Element.ALIGN_CENTER;
            document.Add(title);

            // Adicionando espaço antes da tabela
            document.Add(new Paragraph("\n"));

            // Adicionando dados em uma tabela
            PdfPTable table = new PdfPTable(columnNames.Count);
            table.WidthPercentage = 100;

            // Adicionando cabeçalhos estilizados
            foreach (var columnName in columnNames.Values)
            {
                PdfPCell headerCell = new PdfPCell(new Phrase(columnName, new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD, headerTextColor)));
                headerCell.BackgroundColor = headerBackgroundColor;
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
                    if (columnName == "DosageTime")
                    {
                        // Verificar se a propriedade "DosageTime" existe em TModel
                        var dosageTimeProperty = typeof(TModel).GetProperty("DosageTime");
                        if (dosageTimeProperty != null)
                        {
                            // Obter o valor da propriedade "DosageTime"
                            var dosageTimes = dosageTimeProperty.GetValue(item) as List<string>;
                            if (dosageTimes != null)
                            {
                                // Converter a lista em uma única string separada por vírgulas
                                string dosageTimesString = string.Join(", ", dosageTimes);

                                // Criar uma célula com a string de dosagem
                                PdfPCell dosageTimeCell = new PdfPCell(new Phrase(dosageTimesString, new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.WHITE)));
                                dosageTimeCell.BackgroundColor = currentCellBackgroundColor;
                                dosageTimeCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                dosageTimeCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                                // Adicionar a célula à tabela
                                table.AddCell(dosageTimeCell);
                            }
                        }
                    }
                    else
                    {
                        // Obter o valor da propriedade correspondente ao nome da coluna
                        string cellValue = GetPropertyValue(item, columnName);

                        // Criar uma instância de Font com uma família de fontes específica
                        Font font = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL, BaseColor.WHITE);

                        // Criar uma instância de Phrase com o valor e a fonte especificada
                        Phrase phrase = new Phrase(cellValue, font);

                        // Criar a célula com a Phrase criada
                        PdfPCell cell = new PdfPCell(phrase);
                        cell.BackgroundColor = currentCellBackgroundColor;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;

                        // Adicionar a célula à tabela
                        table.AddCell(cell);
                    }
                }

              
                // Alternar entre as cores das linhas
                isEvenRow = !isEvenRow;
            }
            document.Add(table);
            document.Close();
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
