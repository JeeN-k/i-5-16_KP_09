using System;
using excel = Microsoft.Office.Interop.Excel;
using System.Windows;

namespace TryFasterClient
{
    class DataExcel
    {
        public static void DeliveryProductDocument(string Invoice_Num, string Delivery_Date, string Delivery_Time, string Delivery_Type, string[] Product_name,
            string[] Product_Type, string[] Product_Count) // Вывод документа поставки продуктов
        {
            string name = Registr.DirPath + "Поставка_Товары_" + DateTime.Now.ToString("_hh_mm_ss_dd_MM_yyyy") + ".xlsx";
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
                worksheet.Columns[3].ColumnWidth = 24;
                worksheet.Cells[1, 1] = "TryFaster";
                worksheet.Cells[1, 1].HorizontalAlignment = excel.XlHAlign.xlHAlignCenter;
                worksheet.Cells[1, 1].VerticalAlignment = excel.XlHAlign.xlHAlignCenter;
                worksheet.get_Range("A1", "B7").Borders.LineStyle = excel.XlLineStyle.xlContinuous;
                worksheet.get_Range("A1", "B7").Borders[excel.XlBordersIndex.xlEdgeRight].Weight = excel.XlBorderWeight.xlMedium;

                worksheet.get_Range("B" + (Product_name.Length + 7 + 1).ToString()).Borders[excel.XlBordersIndex.xlEdgeBottom].LineStyle = excel.XlLineStyle.xlContinuous;
                worksheet.get_Range("B" + (Product_name.Length + 7 + 2).ToString()).Borders[excel.XlBordersIndex.xlEdgeBottom].LineStyle = excel.XlLineStyle.xlContinuous;
                worksheet.Cells[7 + Product_name.Length + 1, 1] = "Подпись кладовщика";
                worksheet.Cells[7 + Product_name.Length + 2, 1] = "Подпись курьера";

                worksheet.get_Range("A7", "C7").Borders[excel.XlBordersIndex.xlEdgeBottom].Weight = excel.XlBorderWeight.xlMedium;
                worksheet.get_Range("A7", "C7").Borders[excel.XlBordersIndex.xlEdgeLeft].Weight = excel.XlBorderWeight.xlMedium;
                worksheet.get_Range("A7", "C7").Borders[excel.XlBordersIndex.xlEdgeTop].Weight = excel.XlBorderWeight.xlMedium;
                worksheet.get_Range("A7", "C7").Borders[excel.XlBordersIndex.xlEdgeRight].Weight = excel.XlBorderWeight.xlMedium;
                worksheet.get_Range("A8", "C" + (Product_name.Length + 7).ToString()).Borders.LineStyle = excel.XlLineStyle.xlContinuous;
                worksheet.get_Range("A8", "C" + (Product_name.Length + 7).ToString()).Borders[excel.XlBordersIndex.xlEdgeBottom].Weight = excel.XlBorderWeight.xlMedium;
                worksheet.get_Range("A8", "C" + (Product_name.Length + 7).ToString()).Borders[excel.XlBordersIndex.xlEdgeLeft].Weight = excel.XlBorderWeight.xlMedium;
                worksheet.get_Range("A8", "C" + (Product_name.Length + 7).ToString()).Borders[excel.XlBordersIndex.xlEdgeTop].Weight = excel.XlBorderWeight.xlMedium;
                worksheet.get_Range("A8", "C" + (Product_name.Length + 7).ToString()).Borders[excel.XlBordersIndex.xlEdgeRight].Weight = excel.XlBorderWeight.xlMedium;

                worksheet.get_Range("A1", "B2").Borders.Weight = excel.XlBorderWeight.xlMedium;
                worksheet.Cells[3, 1] = "Номер накладной";
                worksheet.Cells[4, 1] = "Дата поставки";
                worksheet.Cells[5, 1] = "Время поставки";
                worksheet.Cells[6, 1] = "Тип поставки";
                worksheet.Cells[7, 1] = "Наименование товара";
                worksheet.Cells[7, 2] = "Тип товара";
                worksheet.Cells[7, 3] = "Количество товара";

                worksheet.Cells[3, 2].HorizontalAlignment = excel.XlHAlign.xlHAlignCenter;
                worksheet.Cells[4, 2].HorizontalAlignment = excel.XlHAlign.xlHAlignCenter;
                worksheet.Cells[5, 2].HorizontalAlignment = excel.XlHAlign.xlHAlignCenter;
                worksheet.Cells[6, 2].HorizontalAlignment = excel.XlHAlign.xlHAlignCenter;
                worksheet.Cells[7, 2].HorizontalAlignment = excel.XlHAlign.xlHAlignCenter;

                worksheet.Cells[3, 2] = Invoice_Num;
                worksheet.Cells[4, 2] = Delivery_Date;
                worksheet.Cells[5, 2] = Delivery_Time;
                worksheet.Cells[6, 2] = Delivery_Type;
                int i = 0;
                foreach (string products in Product_name)
                {
                    worksheet.Cells[i + 8, 1] = products;
                    worksheet.Cells[i + 8, 2] = Product_Type[i];
                    worksheet.Cells[i + 8, 3] = Product_Count[i];
                    i++;
                }


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

        public static void DeliveryTransporttDocument(string Invoice_Num, string Delivery_Date, string Delivery_Time, string Delivery_Type, string[] Transport_Name,
           string[] TransportTpNum, string[] TransportPower) // Вывод документа поставки транспорта
        {
            string name = Registr.DirPath + "Поставка_Транспорт_" + DateTime.Now.ToString("_hh_mm_ss_dd_MM_yyyy") + ".xlsx";
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
                worksheet.Columns[3].ColumnWidth = 24;
                worksheet.Cells[1, 1] = "TryFaster";
                worksheet.Cells[1, 1].HorizontalAlignment = excel.XlHAlign.xlHAlignCenter;
                worksheet.Cells[1, 1].VerticalAlignment = excel.XlHAlign.xlHAlignCenter;
                worksheet.get_Range("A1", "B7").Borders.LineStyle = excel.XlLineStyle.xlContinuous;
                worksheet.get_Range("A1", "B7").Borders[excel.XlBordersIndex.xlEdgeRight].Weight = excel.XlBorderWeight.xlMedium;

                worksheet.get_Range("B" + (Transport_Name.Length + 7 + 1).ToString()).Borders[excel.XlBordersIndex.xlEdgeBottom].LineStyle = excel.XlLineStyle.xlContinuous;
                worksheet.get_Range("B" + (Transport_Name.Length + 7 + 2).ToString()).Borders[excel.XlBordersIndex.xlEdgeBottom].LineStyle = excel.XlLineStyle.xlContinuous;
                worksheet.Cells[7 + Transport_Name.Length + 1, 1] = "Подпись кладовщика";
                worksheet.Cells[7 + Transport_Name.Length + 2, 1] = "Подпись курьера";

                worksheet.get_Range("A7", "C7").Borders[excel.XlBordersIndex.xlEdgeBottom].Weight = excel.XlBorderWeight.xlMedium;
                worksheet.get_Range("A7", "C7").Borders[excel.XlBordersIndex.xlEdgeLeft].Weight = excel.XlBorderWeight.xlMedium;
                worksheet.get_Range("A7", "C7").Borders[excel.XlBordersIndex.xlEdgeTop].Weight = excel.XlBorderWeight.xlMedium;
                worksheet.get_Range("A7", "C7").Borders[excel.XlBordersIndex.xlEdgeRight].Weight = excel.XlBorderWeight.xlMedium;
                worksheet.get_Range("A8", "C" + (Transport_Name.Length + 7).ToString()).Borders.LineStyle = excel.XlLineStyle.xlContinuous;
                worksheet.get_Range("A8", "C" + (Transport_Name.Length + 7).ToString()).Borders[excel.XlBordersIndex.xlEdgeBottom].Weight = excel.XlBorderWeight.xlMedium;
                worksheet.get_Range("A8", "C" + (Transport_Name.Length + 7).ToString()).Borders[excel.XlBordersIndex.xlEdgeLeft].Weight = excel.XlBorderWeight.xlMedium;
                worksheet.get_Range("A8", "C" + (Transport_Name.Length + 7).ToString()).Borders[excel.XlBordersIndex.xlEdgeTop].Weight = excel.XlBorderWeight.xlMedium;
                worksheet.get_Range("A8", "C" + (Transport_Name.Length + 7).ToString()).Borders[excel.XlBordersIndex.xlEdgeRight].Weight = excel.XlBorderWeight.xlMedium;

                worksheet.get_Range("A1", "B2").Borders.Weight = excel.XlBorderWeight.xlMedium;
                worksheet.Cells[3, 1] = "Номер накладной";
                worksheet.Cells[4, 1] = "Дата поставки";
                worksheet.Cells[5, 1] = "Время поставки";
                worksheet.Cells[6, 1] = "Тип поставки";
                worksheet.Cells[7, 1] = "Наименование транспорта";
                worksheet.Cells[7, 2] = "Номер ПТС";
                worksheet.Cells[7, 3] = "Мощность";

                worksheet.Cells[3, 2].HorizontalAlignment = excel.XlHAlign.xlHAlignCenter;
                worksheet.Cells[4, 2].HorizontalAlignment = excel.XlHAlign.xlHAlignCenter;
                worksheet.Cells[5, 2].HorizontalAlignment = excel.XlHAlign.xlHAlignCenter;
                worksheet.Cells[6, 2].HorizontalAlignment = excel.XlHAlign.xlHAlignCenter;
                worksheet.Cells[7, 2].HorizontalAlignment = excel.XlHAlign.xlHAlignCenter;

                worksheet.Cells[3, 2] = Invoice_Num;
                worksheet.Cells[4, 2] = Delivery_Date;
                worksheet.Cells[5, 2] = Delivery_Time;
                worksheet.Cells[6, 2] = Delivery_Type;
                int i = 0;
                foreach (string transports in Transport_Name)
                {
                    worksheet.Cells[i + 8, 1] = transports;
                    worksheet.Cells[i + 8, 2] = TransportTpNum[i];
                    worksheet.Cells[i + 8, 3] = TransportPower[i];
                    i++;
                }


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
