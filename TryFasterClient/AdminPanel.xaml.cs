using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace TryFasterClient
{
    /// <summary>
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : UserControl
    {

        string[] tables = new string[] {"Клиенты", "Бронирование", "Чеки", "Сотрудники", "Поставки", "Должности",
            "Товары на складе", "Транспорт", "Ремонт", "Заезд", "Заезды клиентов", "Пользователи", "Транспорт заезда"};

        private static SqlCommand scmd = new SqlCommand("", DBConAct.sql);
        string idrole = "1";
        string[] roles = new string[30];
        string[] usRoles = new string[30];
        string[] newRoles = new string[30];
        string[] newUsRoles = new string[30];
        string[] users = new string[150];
        string[] newUsers = new string[150];
        string curLogin;
        private static CheckBox[] cbs = new CheckBox[13];

        public AdminPanel()
        {
            InitializeComponent();
            ObjectsAdd();
        }

        public void ObjectsAdd() //Процедура создания объектов динамически
        {
            try
            {
                for (int i = 0; i < tables.Length; i++)
                {
                    TextBlock newLabel = new TextBlock();
                    newLabel.Text = tables[i];
                    newLabel.FontSize = 12;
                    newLabel.Visibility = Visibility.Visible;
                    newLabel.HorizontalAlignment = HorizontalAlignment.Right;
                    newLabel.VerticalAlignment = VerticalAlignment.Center;
                    Grid.SetRow(newLabel, i);
                    this.GridLabel.Children.Add(newLabel);

                    CheckBox newCB = new CheckBox();
                    newCB.Visibility = Visibility.Visible;
                    newCB.Width = 15;
                    newCB.Height = 15;
                    Thickness margin = newCB.Margin;
                    margin.Left = 10;
                    newCB.Margin = margin;
                    newCB.HorizontalAlignment = HorizontalAlignment.Left;
                    newCB.VerticalAlignment = VerticalAlignment.Center;
                    newCB.Name = "cb" + i.ToString();
                    cbs[i] = newCB;
                    Grid.SetRow(newCB, i);
                    this.GridChckBoxes.Children.Add(newCB);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void getCbItems()
        {
            try
            {
                DBConAct.sql.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from Role", DBConAct.sql);
                DataSet dataSet = new DataSet();

                adapter.Fill(dataSet, "Role");
                for (int i = 0; i < dataSet.Tables["Role"].Rows.Count; i++)
                {
                    roles[i] = dataSet.Tables["Role"].Rows[i][1].ToString();
                    usRoles[i] = dataSet.Tables["Role"].Rows[i][1].ToString();
                }
                roles[roles.Length - 1] = "<Новая роль>";
                newRoles = (from elem in roles where elem != null select elem).ToArray();
                newUsRoles = (from elem in usRoles where elem != null select elem).ToArray();

                adapter.SelectCommand.CommandText = "select * from AppUser";
                adapter.Fill(dataSet, "AppUser");
                for (int i = 0; i < dataSet.Tables["AppUser"].Rows.Count; i++)
                {
                    users[i] = dataSet.Tables["AppUser"].Rows[i][1].ToString();
                }

                newUsers = (from elem in users where elem != null select elem).ToArray();

                cbUsers.ItemsSource = newUsers;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                DBConAct.sql.Close();
            }
        }

        private void UcAdmin_Loaded(object sender, RoutedEventArgs e)
        {
            getCbItems();
            cbRole.ItemsSource = newUsRoles;
            cbRoles.ItemsSource = newRoles;
            cbUsers.SelectedIndex = 0;
            cbRoles.SelectedIndex = 0;
            chckBoxFill();
        }

        private void chckBoxFill()
        {
            try
            {
                DBConAct.sql.Open();
                DataTable dt = new DataTable();
                dt.Load(scmd.ExecuteReader());
                for (int a = 0; a < 13; a++)
                {
                    cbs[a].IsChecked = (bool)dt.Rows[0][a];//указание значени чекбокса
                }
                DBConAct.sql.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CbRoles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbRoles.SelectedValue != "<Новая роль>")
            {
                DBConAct.cmd.CommandText = "select id_role from role where role_name = '" + cbRoles.SelectedValue + "'";
                idrole = DBConAct.execScalar();
                scmd.CommandText = "select client, Booking, Payment_Check, employee, delivery, position, " +
                   "Product, Transport, Repair, race, Race_Client, AppUser, Transport_race from role where id_role = " + idrole;
                chckBoxFill();
            }
            else
            {
                cbRoles.Visibility = Visibility.Collapsed;
                tbRoles.Visibility = Visibility.Visible;
                btnRoleAdd.Visibility = Visibility.Visible;
                btnRoleUpdate.Visibility = Visibility.Collapsed;
                btnRoleCancel.Visibility = Visibility.Visible;
                for (int a = 0; a < 13; a++)
                {
                    cbs[a].IsChecked = false;//указание значени чекбокса
                }
            }
        }

        private void BtnRoleUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DBConAct.Role_Update(Convert.ToInt32(idrole), cbRoles.SelectedValue.ToString(), (bool)cbs[1].IsChecked, (bool)cbs[0].IsChecked,
                    (bool)cbs[4].IsChecked, (bool)cbs[3].IsChecked, (bool)cbs[2].IsChecked, (bool)cbs[5].IsChecked, (bool)cbs[6].IsChecked, (bool)cbs[9].IsChecked,
                    (bool)cbs[10].IsChecked, (bool)cbs[8].IsChecked, (bool)cbs[7].IsChecked, (bool)cbs[11].IsChecked, (bool)cbs[12].IsChecked);
                MessageBox.Show("Роль обновлена!", "Обновление роли", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnRoleAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbRoles.Text != "")
                {
                    DBConAct.Role_Insert(tbRoles.Text, (bool)cbs[1].IsChecked, (bool)cbs[0].IsChecked,
                        (bool)cbs[4].IsChecked, (bool)cbs[3].IsChecked, (bool)cbs[2].IsChecked, (bool)cbs[5].IsChecked, (bool)cbs[6].IsChecked, (bool)cbs[9].IsChecked,
                        (bool)cbs[10].IsChecked, (bool)cbs[8].IsChecked, (bool)cbs[7].IsChecked, (bool)cbs[11].IsChecked, (bool)cbs[12].IsChecked);
                    MessageBox.Show("Роль успешно добавлена", "Роль добавлена", MessageBoxButton.OK, MessageBoxImage.Information);
                    UcAdmin_Loaded(sender, e);
                }
                else MessageBox.Show("Заполните имя роли!", "Название роли не заполнено", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка!");
            }
        }

        private void BtnRoleCancel_Click(object sender, RoutedEventArgs e)
        {
            cbRoles.Visibility = Visibility.Visible;
            tbRoles.Visibility = Visibility.Collapsed;
            btnRoleAdd.Visibility = Visibility.Collapsed;
            btnRoleUpdate.Visibility = Visibility.Visible;
            btnRoleCancel.Visibility = Visibility.Collapsed;
            UcAdmin_Loaded(sender, e);
        }

        private void CbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            getTextToLbl();
        }

        private void getTextToLbl()
        {
            try
            {
                curLogin = cbUsers.SelectedValue.ToString();
                lblLog.Text = curLogin;
                DBConAct.cmd.CommandText = "select user_pass from appuser where [user_login] = '" + curLogin + "'";
                lblPass.Text = DBConAct.execScalar();
                DBConAct.cmd.CommandText = "select role_name from appuser join [role] on [role_id] = id_role where [user_login] = '" + curLogin + "'";
                lblRole.Text = DBConAct.execScalar();
            }
            catch (SqlException se)
            {
                MessageBox.Show(se.ToString());
            }
        }

        private void BtnUserUpd_Click(object sender, RoutedEventArgs e)
        {
            btnAccept.Visibility = Visibility.Visible;
            btnUserCancel.Visibility = Visibility.Visible;
            btnUserUpd.Visibility = Visibility.Collapsed;
            tbLog.Visibility = Visibility.Visible;
            cbRole.Visibility = Visibility.Visible;
            lblLog.Visibility = Visibility.Collapsed;
            lblRole.Visibility = Visibility.Collapsed;
            cbUsers.IsEnabled = false;

            if (lblRole.Text != "Клиент")
            {
                tbPass.Visibility = Visibility.Visible;
                lblPass.Visibility = Visibility.Collapsed;
            }
            cbRole.SelectedValue = lblRole.Text;

            tbLog.Text = lblLog.Text;
            tbPass.Text = "";
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lblRole.Text == "Клиент")
                {
                    if (tbLog.Text != "")
                    {
                        DBConAct.cmd.CommandText = "select id_role from role where role_name = '" + lblRole.Text + "'";
                        string idrole = DBConAct.execScalar();
                        DBConAct.cmd.CommandText = "update appuser set User_Login = '" + tbLog.Text + "', Role_id = " + idrole + " where User_Login = '" + lblLog.Text + "'";
                        DBConAct.sql.Open();
                        DBConAct.cmd.ExecuteScalar();
                        DBConAct.sql.Close();
                        UcAdmin_Loaded(sender, e);
                        BtnUserCancel_Click(sender, e);
                        cbUsers.SelectedIndex = 0;
                        curLogin = tbLog.Text;

                        MessageBox.Show("Данные пользователя успешно обновлены!", "Данные обновлены", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else MessageBox.Show("Заполните все поля!", "Поля не заполнены", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    if (tbLog.Text != "")
                    {
                        DBConAct.cmd.CommandText = "select id_appuser from appuser where user_login = '" + lblLog.Text + "'";
                        int iduser = Convert.ToInt32(DBConAct.execScalar());
                        DBConAct.cmd.CommandText = "select id_role from role where role_name = '" + cbRole.SelectedValue.ToString() + "'";
                        int idrole = Convert.ToInt32(DBConAct.execScalar());
                        DBConAct.AppUser_Update(iduser, tbLog.Text, tbPass.Text, idrole);
                        UcAdmin_Loaded(sender, e);
                        BtnUserCancel_Click(sender, e);
                        cbUsers.SelectedIndex = 0;
                        curLogin = tbLog.Text;

                        MessageBox.Show("Данные пользователя успешно обновлены!", "Данные обновлены", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else MessageBox.Show("Заполните все поля!", "Поля не заполнены", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка");
            }
            catch (NullReferenceException nre)
            {

            }

            finally
            {
                DBConAct.sql.Close();
            }
        }

        private void BtnUserCancel_Click(object sender, RoutedEventArgs e)
        {
            btnAccept.Visibility = Visibility.Collapsed;
            btnUserCancel.Visibility = Visibility.Collapsed;
            btnUserUpd.Visibility = Visibility.Visible;
            tbLog.Visibility = Visibility.Collapsed;
            tbPass.Visibility = Visibility.Collapsed;
            cbRole.Visibility = Visibility.Collapsed;
            lblLog.Visibility = Visibility.Visible;
            lblPass.Visibility = Visibility.Visible;
            lblRole.Visibility = Visibility.Visible;

            cbUsers.IsEnabled = true;
        }

        private void BtnRoleDelete_Click(object sender, RoutedEventArgs e)
        {
            int roleid;
            try
            {
                DBConAct.cmd.CommandText = "select id_role from role where role_name = '" + cbRoles.SelectedValue.ToString() + "'";
                roleid = Convert.ToInt32(DBConAct.execScalar());
                DBConAct.Role_Delete(roleid);
                MessageBox.Show("Выбрнная роль удалена!", "Роль удалена", MessageBoxButton.OK, MessageBoxImage.Information);
                BtnRoleCancel_Click(sender, e);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnBackToMenu_Click(object sender, RoutedEventArgs e)
        {
            LinkControl.Link(new MenuControl());
        }
    }
}