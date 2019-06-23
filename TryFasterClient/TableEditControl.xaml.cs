using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
namespace TryFasterClient
{
    /// <summary>
    /// Логика взаимодействия для TableEditControl.xaml
    /// </summary>
    public partial class TableEditControl : UserControl
    {
        Grid MainGrid = Application.Current.MainWindow.Content as Grid;
        string[] AllTables = new string[] {"Клиенты", "Бронирование", "Чеки", "Сотрудники", "Поставки", "Должности",
            "Товары на складе", "Транспорт", "Ремонт", "Заезд", "Заезды клиентов", "Транспорт заезда", "Пользователи"}; // Массив названий таблиц, используется для заполнения ComboBox с табицами
        string[] tables;//массив таблиц
        Object[] textBoxes = new Object[9];
        public static string[] SelectedValues = new string[20];//выбранное значение
        Int32 ID_table; // Переменная, используется для хранения перчивного ключа таблицы

        public TableEditControl()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DBConAct.sql.Open();

                SqlCommand cmd = new SqlCommand("select Client, booking, payment_check, employee, delivery, position, product, transport, repair, " +
                    "race, race_client, transport_Race, appuser from role where ID_Role =" + AuthorizControl.Role.ToString(), DBConAct.sql);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());


                for (int i = 0; i < 13; i++)
                {
                    if ((bool)dt.Rows[0][i] == false)
                        AllTables[i] = null;
                }

                tables = (from elem in AllTables where elem != null select elem).ToArray();
                DBConAct.sql.Close();

                LabelsAdd();
                CbTables.ItemsSource = tables;
                CbTables.SelectedIndex = 0;
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

        public void LabelsAdd() //Процедура создания объектов динамически
        {
            try
            {
                int CountCb = 0;
                for (int i = 0; i < DynamicObject.count; i++)
                {
                    TextBlock newLabel = new TextBlock();
                    newLabel.Text = DynamicObject.Parameters[i];
                    newLabel.FontSize = 12;
                    newLabel.Visibility = Visibility.Visible;                    
                    newLabel.VerticalAlignment = VerticalAlignment.Center;
                    Grid.SetRow(newLabel, i);
                    this.GridLabel.Children.Add(newLabel);

                    if (DynamicObject.CheckBoxes[i] == 0)
                    {
                        Xceed.Wpf.Toolkit.MaskedTextBox newTextBox = new Xceed.Wpf.Toolkit.MaskedTextBox();
                        newTextBox.FontSize = 12;
                        newTextBox.VerticalContentAlignment = VerticalAlignment.Center;
                        newTextBox.Margin = new Thickness(5, 5, 5, 5);
                        newTextBox.Visibility = Visibility.Visible;
                        newTextBox.Name = "tb" + i.ToString();
                        newTextBox.Mask = DynamicObject.Masked[i];
                        Grid.SetRow(newTextBox, i);
                        textBoxes[i] = newTextBox;
                        this.GridTextBox.Children.Add(newTextBox);

                    }
                    else
                    {
                        if (DynamicObject.CheckBoxes[i] == 1)
                        {
                            ComboBox newComboBox = new ComboBox();
                            newComboBox.FontSize = 12;
                            newComboBox.Margin = new Thickness(5, 5, 5, 5);
                            newComboBox.Visibility = Visibility.Visible;

                            DataTable datatbl1 = new DataTable();
                            SqlCommand command1 = new SqlCommand(DynamicObject.qrCombobox[CountCb], DBConAct.sql);

                            DBConAct.sql.Close();
                            DBConAct.sql.Open();
                            datatbl1.Load(command1.ExecuteReader());

                            for (int k = 0; k < datatbl1.Rows.Count; k++)
                            {
                                newComboBox.Items.Add(datatbl1.Rows[k][0].ToString());
                            }

                            Grid.SetRow(newComboBox, i);
                            textBoxes[i] = newComboBox;
                            this.GridTextBox.Children.Add(newComboBox);
                            if (CountCb <= DynamicObject.qrCombobox.Length) CountCb++;
                            DBConAct.sql.Close();
                        }

                        if (DynamicObject.CheckBoxes[i] == 2)
                        {

                            CheckBox newCheckBox = new CheckBox();
                            newCheckBox.Margin = new Thickness(5, 5, 5, 5);
                            newCheckBox.Visibility = Visibility.Visible;
                            newCheckBox.VerticalAlignment = VerticalAlignment.Center;
                            Grid.SetRow(newCheckBox, i);
                            textBoxes[i] = newCheckBox;
                            this.GridTextBox.Children.Add(newCheckBox);

                        }

                        if (DynamicObject.CheckBoxes[i] == 3)
                        {
                            Xceed.Wpf.Toolkit.TimePicker newTp = new Xceed.Wpf.Toolkit.TimePicker();
                            newTp.FontSize = 12;
                            newTp.Margin = new Thickness(5, 5, 5, 5);
                            newTp.Visibility = Visibility.Visible;
                            newTp.Value = DateTime.Now;
                            Grid.SetRow(newTp, i);
                            textBoxes[i] = newTp;
                            this.GridTextBox.Children.Add(newTp);
                        }

                        if (DynamicObject.CheckBoxes[i] == 4)
                        {
                            Xceed.Wpf.Toolkit.DateTimePicker newDtp = new Xceed.Wpf.Toolkit.DateTimePicker();
                            newDtp.FontSize = 12;
                            newDtp.Margin = new Thickness(5, 5, 5, 5);
                            newDtp.Visibility = Visibility.Visible;
                            newDtp.TimePickerVisibility = Visibility.Hidden;
                            newDtp.Format = Xceed.Wpf.Toolkit.DateTimeFormat.ShortDate;
                            newDtp.Value = DateTime.Now;
                            Grid.SetRow(newDtp, i);
                            textBoxes[i] = newDtp;
                            this.GridTextBox.Children.Add(newDtp);
                        }
                        DynamicObject.Parameters[i] = "";
                    }
                }
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

        private void LoadDBGrid() // Процедура загрузки datagrid
        {
            DBConAct.sql.Open();
            DynamicObject.SelectedTable(CbTables.SelectedItem.ToString());
            LabelsAdd();

            DBConAct.sql.Open();
            DataTable datatbl = new DataTable();
            SqlCommand command = new SqlCommand(DynamicObject.qrTable, DBConAct.sql);

            datatbl.Load(command.ExecuteReader());
            DG_Tables.ItemsSource = datatbl.DefaultView;
            DBConAct.sql.Close();
        }

        private void Word_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Excel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PDF_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LinkControl.Link(new MenuControl());
        }

        private void DG_Tables_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                ID_table = Convert.ToInt32(((DataRowView)DG_Tables.SelectedItems[0]).Row[0].ToString());
                for (int k = 1; k < DG_Tables.Columns.Count; k++)
                {
                    SelectedValues[k - 1] = ((DataRowView)DG_Tables.SelectedItems[0]).Row[k].ToString();

                    //MessageBox.Show(((DataRowView)DG_Tables.SelectedItems[0]).Row[1].ToString());
                }

                for (int z = 0; z <= DynamicObject.count; z++)
                {
                    if (DynamicObject.CheckBoxes[z] == 0)
                        (textBoxes[z] as TextBox).Text = SelectedValues[z];
                    else if (DynamicObject.CheckBoxes[z] == 1)
                        (textBoxes[z] as ComboBox).SelectedValue = SelectedValues[z];
                    else if (DynamicObject.CheckBoxes[z] == 2)
                    {
                        if (SelectedValues[z] == "Да")
                            (textBoxes[z] as CheckBox).IsChecked = true;
                        else
                            (textBoxes[z] as CheckBox).IsChecked = false;
                    }
                    else if (DynamicObject.CheckBoxes[z] == 3)
                        (textBoxes[z] as Xceed.Wpf.Toolkit.TimePicker).Text = SelectedValues[z];
                    else if (DynamicObject.CheckBoxes[z] == 4)
                        (textBoxes[z] as Xceed.Wpf.Toolkit.DateTimePicker).Text = SelectedValues[z];
                }

            }
            catch { }
        }

        private void CbTables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                this.GridLabel.Children.Clear();
                this.GridTextBox.Children.Clear();
                DynamicObject.qrTable = "";
                //DG_Tables.ItemsSource = null;
                LoadDBGrid();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            finally { DBConAct.sql.Close(); }
            //DG_Tables.Columns[0].Visibility = Visibility.Hidden;
        }

        private void BtnAdd_Click_1(object sender, RoutedEventArgs e)
        {
            Int32[] FKIDS = new Int32[9];
            string[] timeForm = new string[9];
            try
            {
                for (int i = 0; i < DynamicObject.count; i++)
                {
                    if ((DynamicObject.CheckBoxes[i] == 0 && (textBoxes[i] as TextBox).Text == null)
                        || (DynamicObject.CheckBoxes[i] == 1 && (textBoxes[i] as ComboBox).SelectedValue == null))
                    {
                        MessageBox.Show("Заполните данные во все поля!", "Поля не заполнены", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    else
                    {
                        if (DynamicObject.CheckBoxes[i] == 1)
                        {
                            SqlCommand command = new SqlCommand(DynamicObject.GetFKID[i] + "'" + (textBoxes[i] as ComboBox).SelectedValue + "'", DBConAct.sql);
                            DataTable datatbl = new DataTable();
                            DBConAct.sql.Open();
                            datatbl.Load(command.ExecuteReader());
                            DBConAct.sql.Close();
                            FKIDS[i] = Convert.ToInt32(datatbl.Rows[0].ItemArray[0].ToString());
                        }
                    }
                    if (DynamicObject.CheckBoxes[i] == 3)
                    {
                        timeForm[i] = (textBoxes[i] as Xceed.Wpf.Toolkit.TimePicker).Text;
                        if (timeForm[i].Length == 4) timeForm[i] = timeForm[i].Insert(0, "0");
                        else timeForm[i] = (textBoxes[i] as Xceed.Wpf.Toolkit.TimePicker).Text;
                    }
                }
                int rowCount = 0;
                rowCount = DG_Tables.Items.Count;
                switch (CbTables.SelectedValue)
                {
                    case "Клиенты":
                        DBConAct.Client_Insert((textBoxes[0] as TextBox).Text, (textBoxes[1] as TextBox).Text, (textBoxes[2] as TextBox).Text, (textBoxes[3] as TextBox).Text, FKIDS[4]);
                        break;
                    case "Бронирование":
                        DBConAct.Booking_Insert((textBoxes[1] as Xceed.Wpf.Toolkit.DateTimePicker).Text, timeForm[0],
                            Convert.ToInt32((textBoxes[2] as TextBox).Text), FKIDS[3]);
                        break;
                    case "Чеки":
                        DBConAct.PaymentCheck_Insert((textBoxes[2] as TextBox).Text, (textBoxes[0] as Xceed.Wpf.Toolkit.DateTimePicker).Text,
                            timeForm[1], float.Parse((textBoxes[3] as TextBox).Text), FKIDS[4]);
                        break;
                    case "Сотрудники":
                        DBConAct.Employee_Insert((textBoxes[0] as TextBox).Text, (textBoxes[1] as TextBox).Text, (textBoxes[2] as TextBox).Text,
                            (textBoxes[3] as TextBox).Text, (textBoxes[4] as TextBox).Text, (textBoxes[5] as TextBox).Text, FKIDS[6], FKIDS[7]);
                        break;
                    case "Поставки":
                        DBConAct.Delivery_Insert((textBoxes[0] as TextBox).Text, (textBoxes[1] as TextBox).Text, (textBoxes[2] as TextBox).Text, (textBoxes[3] as TextBox).Text);
                        break;
                    case "Должности":
                        DBConAct.Position_Insert((textBoxes[0] as TextBox).Text, float.Parse((textBoxes[1] as TextBox).Text));
                        break;
                    case "Товары на складе":
                        DBConAct.Product_Insert((textBoxes[0] as TextBox).Text, Convert.ToInt32((textBoxes[1] as TextBox).Text), (textBoxes[2] as TextBox).Text, FKIDS[3]);
                        break;
                    case "Транспорт":
                        DBConAct.Transport_Insert((textBoxes[0] as TextBox).Text, (textBoxes[1] as TextBox).Text, (textBoxes[2] as TextBox).Text,
                            (textBoxes[3] as TextBox).Text, FKIDS[4]);
                        break;
                    case "Ремонт":
                        string yn;
                        if ((textBoxes[2] as CheckBox).IsChecked == true) yn = "Да";
                        else yn = "Нет";
                        DBConAct.Repair_Insert((textBoxes[1] as Xceed.Wpf.Toolkit.DateTimePicker).Text, (textBoxes[0] as TextBox).Text, yn, FKIDS[3]);
                        break;
                    case "Заезд":
                        DBConAct.Race_Insert(timeForm[1], (textBoxes[0] as Xceed.Wpf.Toolkit.DateTimePicker).Text, timeForm[2],
                            timeForm[3], FKIDS[4]);
                        break;
                    case "Заезды клиентов":
                        DBConAct.Race_Client_Insert(FKIDS[0], FKIDS[1]);
                        break;
                    case "Транспорт заезда":
                        DBConAct.Transport_Race_Insert(FKIDS[0], FKIDS[1]);
                        break;
                    case "Пользователи":
                        DBConAct.AppUser_Insert((textBoxes[0] as TextBox).Text, (textBoxes[1] as TextBox).Text, FKIDS[2]);
                        break;
                }
                LoadDBGrid();
                if (DG_Tables.Items.Count != rowCount)
                {
                    MessageBox.Show("Данные успешно внесены в таблицу " + CbTables.SelectedValue.ToString() + "!", "Данные добавлены", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else MessageBox.Show("Ошибка внесения данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "Ошибка добавления!"); }
        }

        private void BtnUpd_Click(object sender, RoutedEventArgs e)
        {
            Int32[] FKIDS = new Int32[9];
            string[] timeForm = new string[9];
            try
            {
                for (int i = 0; i < DynamicObject.count; i++)
                {
                    if ((DynamicObject.CheckBoxes[i] == 0 && (textBoxes[i] as TextBox).Text == null)
                        || (DynamicObject.CheckBoxes[i] == 1 && (textBoxes[i] as ComboBox).SelectedValue == null))
                    {
                        MessageBox.Show("Заполните данные во все поля!");
                        return;
                    }
                    else
                    {
                        if (DynamicObject.CheckBoxes[i] == 1)
                        {
                            SqlCommand command = new SqlCommand(DynamicObject.GetFKID[i] + "'" + (textBoxes[i] as ComboBox).SelectedValue + "'", DBConAct.sql);
                            DataTable datatbl = new DataTable();
                            DBConAct.sql.Open();
                            datatbl.Load(command.ExecuteReader());
                            DBConAct.sql.Close();
                            FKIDS[i] = Convert.ToInt32(datatbl.Rows[0].ItemArray[0].ToString());
                        }
                    }
                    if (DynamicObject.CheckBoxes[i] == 3)
                    {
                        timeForm[i] = (textBoxes[i] as Xceed.Wpf.Toolkit.TimePicker).Text;
                        if (timeForm[i].Length == 4) timeForm[i] = timeForm[i].Insert(0, "0");
                        else timeForm[i] = (textBoxes[i] as Xceed.Wpf.Toolkit.TimePicker).Text;
                    }
                }
                switch (CbTables.SelectedValue)
                {
                    case "Клиенты":
                        DBConAct.Client_Update(ID_table, (textBoxes[0] as TextBox).Text, (textBoxes[1] as TextBox).Text, (textBoxes[2] as TextBox).Text, (textBoxes[3] as TextBox).Text, FKIDS[4]);
                        break;
                    case "Бронирование":
                        DBConAct.Booking_Update(ID_table, (textBoxes[1] as Xceed.Wpf.Toolkit.DateTimePicker).Text, timeForm[0],
                            Convert.ToInt32((textBoxes[2] as TextBox).Text), FKIDS[3]);
                        break;
                    case "Чеки":
                        DBConAct.PaymentCheck_Update(ID_table, (textBoxes[2] as TextBox).Text, (textBoxes[0] as Xceed.Wpf.Toolkit.DateTimePicker).Text,
                            timeForm[1], float.Parse((textBoxes[3] as TextBox).Text), FKIDS[4]);
                        break;
                    case "Сотрудники":
                        DBConAct.Employee_Update(ID_table, (textBoxes[0] as TextBox).Text, (textBoxes[1] as TextBox).Text, (textBoxes[2] as TextBox).Text,
                            (textBoxes[3] as TextBox).Text, (textBoxes[4] as TextBox).Text, (textBoxes[5] as TextBox).Text, FKIDS[6], FKIDS[7]);
                        break;
                    case "Поставки":
                        DBConAct.Delivery_Update(ID_table, (textBoxes[0] as TextBox).Text, (textBoxes[1] as Xceed.Wpf.Toolkit.DateTimePicker).Text,
                            timeForm[2], (textBoxes[3] as TextBox).Text);
                        break;
                    case "Должности":
                        DBConAct.Position_Update(ID_table, (textBoxes[0] as TextBox).Text, float.Parse((textBoxes[1] as TextBox).Text));
                        break;
                    case "Товары на складе":
                        DBConAct.Product_Update(ID_table, Convert.ToInt32((textBoxes[1] as TextBox).Text), (textBoxes[0] as TextBox).Text, (textBoxes[2] as TextBox).Text, FKIDS[3]);
                        break;
                    case "Транспорт":
                        DBConAct.Transport_Update(ID_table, (textBoxes[0] as TextBox).Text, (textBoxes[1] as TextBox).Text, (textBoxes[2] as TextBox).Text,
                            (textBoxes[3] as TextBox).Text, FKIDS[4]);
                        break;
                    case "Ремонт":
                        string yn;
                        if ((textBoxes[2] as CheckBox).IsChecked == true) yn = "Да";
                        else yn = "Нет";
                        DBConAct.Repair_Update(ID_table, (textBoxes[0] as TextBox).Text, (textBoxes[1] as Xceed.Wpf.Toolkit.DateTimePicker).Text, yn, FKIDS[3]);
                        break;
                    case "Заезд":
                        DBConAct.Race_Update(ID_table, (textBoxes[0] as Xceed.Wpf.Toolkit.DateTimePicker).Text, timeForm[1], timeForm[2],
                            timeForm[3], FKIDS[4]);
                        break;
                    case "Заезды клиентов":
                        DBConAct.Race_Client_Update(ID_table, FKIDS[0], FKIDS[1]);
                        break;
                    case "Транспорт заезда":
                        DBConAct.Transport_Race_Update(ID_table, FKIDS[0], FKIDS[1]);
                        break;
                    case "Пользователи":
                        DBConAct.AppUser_Update(ID_table, (textBoxes[0] as TextBox).Text, (textBoxes[1] as TextBox).Text, FKIDS[2]);
                        break;
                }
                LoadDBGrid();
                MessageBox.Show("Данные таблицы " + CbTables.SelectedValue.ToString() + " успешно обновлены!", "Данные обновлены", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "Ошибка добавления!"); }
        }

        private void BtnDlt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (CbTables.SelectedValue.ToString())
                {
                    case "Клиенты":
                        DBConAct.Client_Delete(ID_table);
                        break;
                    case "Бронирование":
                        DBConAct.Booking_Delete(ID_table);
                        break;
                    case "Чеки":
                        DBConAct.PaymentCheck_Delete(ID_table);
                        break;
                    case "Поставки":
                        DBConAct.Delivery_Delete(ID_table);
                        break;
                    case "Сотрудники":
                        DBConAct.Employee_Delete(ID_table);
                        break;
                    case "Должности":
                        DBConAct.Position_Delete(ID_table);
                        break;
                    case "Товары на складе":
                        DBConAct.Product_Delete(ID_table);
                        break;
                    case "Транспорт":
                        DBConAct.Transport_Delete(ID_table);
                        break;
                    case "Ремонт":
                        DBConAct.Repair_Delete(ID_table);
                        break;
                    case "Заезд":
                        DBConAct.Race_Delete(ID_table);
                        break;
                    case "Заезды клиентов":
                        DBConAct.Race_Client_Delete(ID_table);
                        break;
                    case "Транспорт заезда":
                        DBConAct.Transport_Race_Delete(ID_table);
                        break;
                    case "Пользователи":
                        DBConAct.AppUser_Delete(ID_table);
                        break;
                }
                MessageBox.Show("Данные из таблицы " + CbTables.SelectedValue.ToString() + " удалены!", "Данные удалены", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadDBGrid();
            }
            catch { MessageBox.Show("Ошибка удаления!"); }
        }
    }
}
