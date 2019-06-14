using System;
using excel = Microsoft.Office.Interop.Excel;
using System.Windows;

namespace TryFasterClient
{
    class DataExcel
    {
        public static void DeliveryDocument(string Invoice_Num, string Delivery_Date, string Delivery_Time, string Product_name, string Product_Count)
        {
            string name = Registr.DirPath + DateTime.Now.ToString("_hh_mm_ss_dd_MM_yyyy") + ".xlsx";
            excel.Application application = new excel.Application();
            excel.Workbook workbook = application.Workbooks.Add();
            excel.Worksheet worksheet = (excel.Worksheet)workbook.ActiveSheet;
            try
            {
                excel.Range _excelCells = (excel.Range)worksheet.get_Range("A1", "B2").Cells;
                // Производим объединение
                _excelCells.Merge(Type.Missing);
                worksheet.Name = "Поставка";
                worksheet.Columns[1].ColumnWidth = 21;
                worksheet.Columns[2].ColumnWidth = 21;
                worksheet.Cells[1, 1] = Registr.OrganizationName;
                worksheet.Cells[1, 1].HorizontalAlignment = excel.XlHAlign.xlHAlignCenter;
                worksheet.Cells[1, 1].VerticalAlignment = excel.XlHAlign.xlHAlignCenter;
                worksheet.get_Range("A1", "B7").Borders.LineStyle = excel.XlLineStyle.xlContinuous;
                worksheet.get_Range("B9").Borders[excel.XlBordersIndex.xlEdgeBottom].LineStyle = excel.XlLineStyle.xlContinuous;
                worksheet.get_Range("B10").Borders[excel.XlBordersIndex.xlEdgeBottom].LineStyle = excel.XlLineStyle.xlContinuous;
                worksheet.Cells[3, 1] = "Номер докладной";
                worksheet.Cells[4, 1] = "Дата поставки";
                worksheet.Cells[5, 1] = "Время поставки";
                worksheet.Cells[6, 1] = "Наименование товара";
                worksheet.Cells[7, 1] = "Количество товара";

                worksheet.Cells[3, 2].HorizontalAlignment = excel.XlHAlign.xlHAlignCenter;
                worksheet.Cells[4, 2].HorizontalAlignment = excel.XlHAlign.xlHAlignCenter;
                worksheet.Cells[5, 2].HorizontalAlignment = excel.XlHAlign.xlHAlignCenter;
                worksheet.Cells[6, 2].HorizontalAlignment = excel.XlHAlign.xlHAlignCenter;
                worksheet.Cells[7, 2].HorizontalAlignment = excel.XlHAlign.xlHAlignCenter;

                worksheet.Cells[3, 2] = Invoice_Num;
                worksheet.Cells[4, 2] = Delivery_Date;
                worksheet.Cells[5, 2] = Delivery_Time;
                worksheet.Cells[6, 2] = Product_name;
                worksheet.Cells[7, 2] = Product_Count;

                worksheet.Cells[9, 1] = "Подпись кладовщика";
                worksheet.Cells[10, 1] = "Подпись курьера";
                MessageBox.Show(name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                workbook.SaveAs(name, application.DefaultSaveFormat);
                workbook.Close();
                application.Quit();
            }
        }

        public static void SmetaDocument(string Invoice_Num, string Delivery_Date, string Delivery_Time, string Product_Type, string Product_Count)
        {
            string name = Registr.DirPath + DateTime.Now.ToString("_hh_mm_ss_dd_MM_yyyy") + ".xlsx";
            excel.Application application = new excel.Application();
            excel.Workbook workbook = application.Workbooks.Add();
            excel.Worksheet worksheet = (excel.Worksheet)workbook.ActiveSheet;
            try
            {
                excel.Range _excelCells = (excel.Range)worksheet.get_Range("A1", "B2").Cells;
                // Производим объединение
                _excelCells.Merge(Type.Missing);
                worksheet.Name = "Смета об учёте сырья";
                worksheet.Columns[1].ColumnWidth = 21;
                worksheet.Columns[2].ColumnWidth = 21;
                worksheet.Cells[1, 1] = Registr.OrganizationName;
                worksheet.Cells[1, 1].HorizontalAlignment = excel.XlHAlign.xlHAlignCenter;
                worksheet.Cells[1, 1].VerticalAlignment = excel.XlHAlign.xlHAlignCenter;
                worksheet.get_Range("A1", "B7").Borders.LineStyle = excel.XlLineStyle.xlContinuous;
                worksheet.get_Range("B9").Borders[excel.XlBordersIndex.xlEdgeBottom].LineStyle = excel.XlLineStyle.xlContinuous;
                worksheet.Cells[3, 1] = "Номер сметы";
                worksheet.Cells[4, 1] = "Дата";
                worksheet.Cells[5, 1] = "Время";
                worksheet.Cells[6, 1] = "Вид товара";
                worksheet.Cells[7, 1] = "Количество товара";

                worksheet.Cells[3, 2].HorizontalAlignment = excel.XlHAlign.xlHAlignCenter;
                worksheet.Cells[4, 2].HorizontalAlignment = excel.XlHAlign.xlHAlignCenter;
                worksheet.Cells[5, 2].HorizontalAlignment = excel.XlHAlign.xlHAlignCenter;
                worksheet.Cells[6, 2].HorizontalAlignment = excel.XlHAlign.xlHAlignCenter;
                worksheet.Cells[7, 2].HorizontalAlignment = excel.XlHAlign.xlHAlignCenter;

                worksheet.Cells[3, 2] = Invoice_Num;
                worksheet.Cells[4, 2] = Delivery_Date;
                worksheet.Cells[5, 2] = Delivery_Time;
                worksheet.Cells[6, 2] = Product_Type;
                worksheet.Cells[7, 2] = Product_Count;

                worksheet.Cells[9, 1] = "Подпись кладовщика";
                MessageBox.Show(name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                workbook.SaveAs(name, application.DefaultSaveFormat);
                workbook.Close();
                application.Quit();
            }
        }
    }
}
