using System;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace TryFasterClient
{
    /// <summary>
    /// Логика взаимодействия для DeliveryControl.xaml
    /// </summary>
    public partial class DeliveryControl : UserControl
    {
        string[] dates = new string[] { DateTime.Now.ToString("dd.MM.yyyy"), DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy") }; // массив с датами поставки
        string[] delivTypes = new string[] { "Товары", "Транспорт" };
        string[] ProductTypes = new string[] { "Экипировка", "Запчасти", "Другое" };

        public DeliveryControl()
        {
            InitializeComponent();
        }

        private void comboboxLoad(string selectText, string tableName, int row, ComboBox cb) // загрузка данных в комбобокс из базы данных
        {
            DBConAct.sql.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(selectText, DBConAct.sql);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, tableName);
            for (int i = 0; i < dataSet.Tables[tableName].Rows.Count; i++)
            {
                cb.Items.Add(dataSet.Tables[tableName].Rows[i][row].ToString());
            }
            DBConAct.sql.Close();
            cb.SelectedIndex = 0;
        }

        private void TbTransportPower_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TbTransportPtNum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void CbSelectTransport_Unchecked(object sender, RoutedEventArgs e)
        {
            if (cbSelectTransport.IsChecked == true)
            {
                cbTransportName.Visibility = Visibility.Visible;
                tbTransportName.Visibility = Visibility.Collapsed;
            }
            else
            {
                cbTransportName.Visibility = Visibility.Collapsed;
                tbTransportName.Visibility = Visibility.Visible;
            }
        }

        private void CbSelectProduct_Unchecked(object sender, RoutedEventArgs e)
        {
            if (cbSelectProduct.IsChecked == true)
            {
                cbProductName.Visibility = Visibility.Visible;
                tbProductName.Visibility = Visibility.Collapsed;
            }
            else
            {
                cbProductName.Visibility = Visibility.Collapsed;
                tbProductName.Visibility = Visibility.Visible;
            }
        }

        private void BtnAddDelivery_Click(object sender, RoutedEventArgs e)
        {
            string invoice_Num;  // номер накланой
            int repeatProd = 0; // проверка на повторение продукта                                     
            int prodCountSql = 0; // количество продукта из базы
            int prodCountTb = 0; // количество продуктов из текстбокса            
            int delivId; // id поставки из таблицы store            
            int idProduct = 0; // id store
            string prodType = "";
            string transTPNum = "";
            string transPower = "";
            string prodName = ""; // название продукта
            string transName = "";
            string timeDeliv = tpTimeDelivery.Text; // Время поставки
            if (timeDeliv.Length == 4) timeDeliv = timeDeliv.Insert(0, "0");
            try
            {
                if ((cbDeliveryType.SelectedIndex == 0 && dgProductList.Items.Count != 0) || (cbDeliveryType.SelectedIndex == 1 && dgTransportList.Items.Count != 0))
                {
                    DBConAct.cmd.CommandText = "select count(*) from delivery";
                    invoice_Num = (Convert.ToInt32(DBConAct.execScalar()) + 1).ToString();
                    while (invoice_Num.Length != 10) invoice_Num = invoice_Num.Insert(0, "0");
                    DBConAct.Delivery_Insert(invoice_Num, cbDateDelivery.SelectedValue.ToString(), timeDeliv, cbDeliveryType.SelectedValue.ToString());
                    DBConAct.cmd.CommandText = "select id_delivery from delivery where invoice_num = '" + invoice_Num + "'";
                    delivId = Convert.ToInt32(DBConAct.execScalar());
                    if (cbDeliveryType.SelectedIndex == 0)
                    {
                        for (int y = 0; dgProductList.Items.Count > y; y++)
                        {
                            for (int i = 0; 3 > i; i++)
                            {
                                var ci = new DataGridCellInfo(dgProductList.Items[y], dgProductList.Columns[i]); // возвращает ячейку
                                var content = ci.Column.GetCellContent(ci.Item) as TextBlock; // возвращает значение из ячейки
                                if (i == 0)
                                {
                                    DBConAct.cmd.CommandText = "select count(*) from product where product_name = '" + content.Text + "'";
                                    repeatProd = Convert.ToInt32(DBConAct.execScalar());
                                    prodName = content.Text;
                                }
                                if (i == 1) prodCountTb = Convert.ToInt32(content.Text);
                                if (i == 2) prodType = content.Text;
                            }

                            if (repeatProd != 0)
                            {
                                DBConAct.cmd.CommandText = "select product_Type from product where product_name = '" + prodName + "'";
                                prodType = DBConAct.execScalar();
                                DBConAct.cmd.CommandText = "select id_product from product where product_name = '" + prodName + "'";
                                idProduct = Convert.ToInt32(DBConAct.execScalar());
                                DBConAct.cmd.CommandText = "select Product_Count from product where product_name = '" + prodName + "'";
                                prodCountSql = Convert.ToInt32(DBConAct.execScalar());
                                DBConAct.Product_Update(idProduct, prodCountSql + prodCountTb, prodName, prodType, delivId);
                            }
                            else
                            {
                                DBConAct.Product_Insert(prodName, prodCountTb, prodType, delivId);
                            }
                        }
                        MessageBox.Show("Поставка успешно оформлена!", "Поставка оформлена!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        for (int y = 0; dgTransportList.Items.Count > y; y++)
                        {
                            for (int i = 0; 3 > i; i++)
                            {
                                var ci = new DataGridCellInfo(dgTransportList.Items[y], dgTransportList.Columns[i]); // возвращает ячейку
                                var content = ci.Column.GetCellContent(ci.Item) as TextBlock; // возвращает значение из ячейки
                                if (i == 0) transName = content.Text;
                                if (i == 1) transTPNum = content.Text;                                
                                if (i == 2) transPower = content.Text;
                            }
                            DBConAct.Transport_Insert(transName, transTPNum, "Идеально", transPower, delivId);
                        }
                        MessageBox.Show("Поставка успешно оформлена!", "Поставка оформлена!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else MessageBox.Show("Заполните таблицу товаров!", "Пустая таблица", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                DBConAct.sql.Close();
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ToDeliveryList_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddToTable_Click(object sender, RoutedEventArgs e)
        {
            if (cbDeliveryType.SelectedIndex == 0)
            {
                string prodName; // название продукта 
                string prodCount; // количество продуктов
                string prodType; // цена продукта

                if (cbSelectProduct.IsChecked == true) prodName = cbProductName.SelectedValue.ToString();
                else prodName = tbProductName.Text;
                prodCount = udProductCount.Text.ToString();
                prodType = cbProductType.SelectedValue.ToString();

                if (prodName != "")
                {
                    var data = new Product { ProductName = prodName, ProductCount = prodCount, ProductType = prodType }; // добавление в datagrid с помощью отдельного класса
                    dgProductList.Items.Add(data);
                }
                else MessageBox.Show("Заполните поля ввода!", "Пустое поле", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                string transName; // название продукта 
                string transTpNum; // количество продуктов
                string transPower; // цена продукта

                if (cbSelectTransport.IsChecked == true) transName = cbTransportName.SelectedValue.ToString();
                else transName = tbTransportName.Text;
                transTpNum = tbTransportPtNum.Text;
                transPower = tbTransportPower.Text + " л.с.";
                if (!char.IsDigit(transPower[2])) transPower = transPower.Remove(2, 1).Insert(2, "");

                if (transName != "" && transTpNum != "" && transPower != "")
                    {
                        var data = new Transport { TransportName = transName, TransportPtNum = transTpNum, TransportPower = transPower }; // добавление в datagrid с помощью отдельного класса
                        dgTransportList.Items.Add(data);
                    }
                    else MessageBox.Show("Заполните поля ввода!", "Пустое поле", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (cbDeliveryType.SelectedIndex == 0)
            {
                var selectedItem = dgProductList.SelectedItem; // выделенная строка
                if (selectedItem != null)
                {
                    dgProductList.Items.Remove(selectedItem);
                }
            }
            else
            {
                var selectedItem = dgTransportList.SelectedItem; // выделенная строка
                if (selectedItem != null)
                {
                    dgTransportList.Items.Remove(selectedItem);
                }
            }
        }

        private void BtnClearTable_Click(object sender, RoutedEventArgs e)
        {
            if (cbDeliveryType.SelectedIndex == 0) dgProductList.Items.Clear();
            else dgTransportList.Items.Clear();
        }

        private void CbDeliveryType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbDeliveryType.SelectedIndex == 1)
            {
                spProducts.Visibility = Visibility.Collapsed;
                spTransports.Visibility = Visibility.Visible;
                svProducts.Visibility = Visibility.Collapsed;
                svTransport.Visibility = Visibility.Visible;
            }
            else
            {
                spProducts.Visibility = Visibility.Visible;
                spTransports.Visibility = Visibility.Collapsed;
                svProducts.Visibility = Visibility.Visible;
                svTransport.Visibility = Visibility.Collapsed;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            comboboxLoad("select * from product", "product", 1, cbProductName);
            comboboxLoad("select * from transport", "transport", 1, cbTransportName);            
            tpTimeDelivery.Value = DateTime.Now;
            tpTimeDelivery.Maximum = DateTime.Now;
            cbDateDelivery.ItemsSource = dates;
            cbDateDelivery.SelectedIndex = 0;
            cbDeliveryType.ItemsSource = delivTypes;
            cbDeliveryType.SelectedIndex = 0;
            cbProductType.ItemsSource = ProductTypes;
            cbProductType.SelectedIndex = 0;
        }
        public class Product // класс для добавления данных в datagrid
        {
            public string ProductName { get; set; }
            public string ProductCount { get; set; }
            public string ProductType { get; set; }
        }

        public class Transport // класс для добавления данных в datagrid
        {
            public string TransportName { get; set; }
            public string TransportPtNum { get; set; }
            public string TransportPower { get; set; }
        }
    }
}
