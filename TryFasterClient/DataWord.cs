﻿using System;
using word = Microsoft.Office.Interop.Word;
using System.Windows;

namespace TryFasterClient
{
    class DataWord
    {
        public static void BookDocument(string BookingDate, string BookingTime, string BookingCount, string BookingClient) // Вывод документа бронирования
        {
            word.Application application = new word.Application();
            word.Document document = application.Documents.Add(Visible: true);
            word.Range range = document.Range(0, 0);
            string file_name = Registr.DirPath + "Бронь_" + DateTime.Now.ToString("_hh_mm_ss_dd_MM_yyyy") + ".docx";
            try
            {
                document.Sections.PageSetup.LeftMargin
                    = application.CentimetersToPoints(Convert.ToSingle(Registr.DocLM));
                document.Sections.PageSetup.RightMargin
                    = application.CentimetersToPoints(Convert.ToSingle(Registr.DocRM));
                document.Sections.PageSetup.TopMargin
                    = application.CentimetersToPoints(Convert.ToSingle(Registr.DocTM));
                document.Sections.PageSetup.BottomMargin
                    = application.CentimetersToPoints(Convert.ToSingle(Registr.DocBM));
                range.Text = "Картинг 'TryFaster'";
                range.ParagraphFormat.Alignment
                    = word.WdParagraphAlignment.wdAlignParagraphCenter;
                range.ParagraphFormat.SpaceAfter = 1;
                range.ParagraphFormat.SpaceBefore = 1;
                range.ParagraphFormat.LineSpacingRule = word.WdLineSpacing.wdLineSpaceSingle;
                range.Font.Name = "Times New Roman";
                range.Font.Size = 12;
                document.Paragraphs.Add();
                document.Paragraphs.Add();
                word.Paragraph Name_Doc = document.Paragraphs.Add();
                Name_Doc.Format.Alignment = word.WdParagraphAlignment.wdAlignParagraphCenter;
                Name_Doc.Range.Font.Name = "Times New Roman";
                Name_Doc.Range.Font.Size = 16;
                Name_Doc.Range.Text = "Бронирование";
                document.Paragraphs.Add();
                document.Paragraphs.Add();
                document.Paragraphs.Add();
                word.Paragraph pTable = document.Paragraphs.Add();
                word.Table Book = document.Tables.Add(pTable.Range, 4,
                    2);
                Book.Borders.InsideLineStyle = word.WdLineStyle.wdLineStyleSingle;
                Book.Borders.OutsideLineStyle = word.WdLineStyle.wdLineStyleSingle;

                Book.Range.ParagraphFormat.Alignment = word.WdParagraphAlignment.wdAlignParagraphCenter;
                Book.Range.Paragraphs.Alignment = word.WdParagraphAlignment.wdAlignParagraphCenter;
                Book.Rows.Alignment = word.WdRowAlignment.wdAlignRowCenter;

                Book.Cell(1, 1).Range.Text = "Дата";
                Book.Cell(2, 1).Range.Text = "Время";
                Book.Cell(3, 1).Range.Text = "Количество клиентов";
                Book.Cell(4, 1).Range.Text = "Забронировавший клиент";
                Book.Range.Font.Size = 11;
                Book.Range.Font.Name = "Times New Roman";
                Book.Columns[1].AutoFit();
                Book.Cell(1, 2).Range.Text = BookingDate;
                Book.Cell(2, 2).Range.Text = BookingTime;
                Book.Cell(3, 2).Range.Text = BookingCount;
                Book.Cell(4, 2).Range.Text = BookingClient;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                document.SaveAs2(file_name, word.WdSaveFormat.wdFormatDocumentDefault);
                document.Close();
                application.Quit();
            }
        }
    }
}
