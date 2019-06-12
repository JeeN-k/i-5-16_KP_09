using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace TryFasterClient
{
    /// <summary>
    /// Логика взаимодействия для AccountControl.xaml
    /// </summary>
    public partial class AccountControl : UserControl
    {
        string mobNum; // номер телефона для поиска строки
        string UserLog; // логин пользователя для поиска строки
        public AccountControl()
        {
            InitializeComponent();
        }

        private void TbLoginClient_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsDigit(e.Text, 0)) return;
            e.Handled = true;
            Regex regex = new Regex("[^A-z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TbSurnClient_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsLetter(e.Text, 0)) return;
            e.Handled = true;
        }

        private void BtnAcceptChange_Click(object sender, RoutedEventArgs e)
        {
            Int32 logchk; // проверка на уникальность логина
            string passUser = ""; // пароль пользователя
            int idUser; // id user
            int idClient; // id client

            if (tbLoginClient.Text == "" || tbSurnClient.Text == "" || tbNameClient.Text == "" || tbMidnClient.Text == "" || tbMobNum.Text == "") // если одно из поле пустое
            {
                MessageBox.Show("Поля не заполнены!\nЗаполните все поля!", "Заполните поля", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (tbLoginClient.Text == lblUserName.Content.ToString() && tbSurnClient.Text == lblSurname.Content.ToString()
                    && tbNameClient.Text == lblName.Content.ToString() && tbMidnClient.Text == lblMidn.Content.ToString() && tbMobNum.Text == lblMobNum.Content.ToString())
                    BtnCancelChange_Click(sender, e);
                else
                {
                    DBConAct.cmd.CommandText = "select count(*) from [dbo].[appuser]" +
                                "where " +
                                "(User_Login ='" + tbLoginClient.Text + "')";
                    logchk = Convert.ToInt32(DBConAct.execScalar());
                    if (logchk == 1 && tbLoginClient.Text != UserLog)
                    {
                        MessageBox.Show("Данный логин уже зарегистрирован!", "Измените логин", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        Int32 mobChck; // проверка на уникальность номер телефона
                        DBConAct.cmd.CommandText = "select count (*) from client where Mob_Num_Client = '" + tbMobNum.Text + "'";
                        mobChck = Convert.ToInt32(DBConAct.execScalar());
                        if (mobChck == 1 && tbMobNum.Text != mobNum)
                            MessageBox.Show("Данный номер телефона уже зарегистрирован!", "Измените номер телефона", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else
                        {
                            try
                            {
                                DBConAct.cmd.CommandText = "select Id_appuser from [appuser] where [User_login] = '" + UserLog + "'";
                                idUser = Convert.ToInt32(DBConAct.execScalar());
                                DBConAct.cmd.CommandText = "select [User_pass] from [appuser] where id_appuser = " + idUser.ToString();
                                passUser = DBConAct.execScalar();
                                DBConAct.cmd.CommandText = "select id_client from client where mob_num_client = '" + mobNum + "'";
                                idClient = Convert.ToInt32(DBConAct.execScalar());

                                if (UserLog != tbLoginClient.Text)
                                    DBConAct.AppUser_Update(idUser, tbLoginClient.Text, passUser, 7); // добавляет зарегестрированные данные в бд                                 
                                DBConAct.Client_Update(idClient, tbSurnClient.Text, tbNameClient.Text, tbMobNum.Text, tbMidnClient.Text, idUser);
                                MessageBox.Show("Данные изменены!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                                AuthorizControl.UserLogin = tbLoginClient.Text;
                                BtnCancelChange_Click(sender, e);
                                UcAccount_Loaded(sender, e);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }

                    }

                }
            }
        }

        private void BtnCancelChange_Click(object sender, RoutedEventArgs e)
        {
            lblUserName.Visibility = Visibility.Visible;
            lblSurname.Visibility = Visibility.Visible;
            lblName.Visibility = Visibility.Visible;
            lblMobNum.Visibility = Visibility.Visible;
            lblMidn.Visibility = Visibility.Visible;
            btnChangeData.Visibility = Visibility.Visible;

            tbLoginClient.Visibility = Visibility.Collapsed;
            tbMidnClient.Visibility = Visibility.Collapsed;
            tbMobNum.Visibility = Visibility.Collapsed;
            tbNameClient.Visibility = Visibility.Collapsed;
            tbSurnClient.Visibility = Visibility.Collapsed;
            btnCancelChange.Visibility = Visibility.Collapsed;
            btnAcceptChange.Visibility = Visibility.Collapsed;
        }

        private void BtnChangeData_Click(object sender, RoutedEventArgs e)
        {
            lblUserName.Visibility = Visibility.Collapsed;
            lblSurname.Visibility = Visibility.Collapsed;
            lblName.Visibility = Visibility.Collapsed;
            lblMobNum.Visibility = Visibility.Collapsed;
            lblMidn.Visibility = Visibility.Collapsed;
            btnChangeData.Visibility = Visibility.Collapsed;

            tbLoginClient.Visibility = Visibility.Visible;
            tbMidnClient.Visibility = Visibility.Visible;
            tbMobNum.Visibility = Visibility.Visible;
            tbNameClient.Visibility = Visibility.Visible;
            tbSurnClient.Visibility = Visibility.Visible;
            btnCancelChange.Visibility = Visibility.Visible;
            btnAcceptChange.Visibility = Visibility.Visible;

            tbLoginClient.Text = lblUserName.Content.ToString();
            tbSurnClient.Text = lblSurname.Content.ToString();
            tbMidnClient.Text = lblMidn.Content.ToString();
            tbMobNum.Text = lblMobNum.Content.ToString();
            tbNameClient.Text = lblName.Content.ToString();

            UserLog = lblUserName.Content.ToString();
            mobNum = lblMobNum.Content.ToString();
        }

        private void BtnChangePass_Click(object sender, RoutedEventArgs e)
        {
            Int32 passChck;
            int idUser;
            int roleid;
            try
            {
                if (tbOldPass.Text == "" || tbNewPass.Text == "" || tbCheckNewPass.Text == "") // если одно из поле пустое
                {
                    MessageBox.Show("Поля не заполнены!\nЗаполните все поля!", "Заполните поля", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    DBConAct.cmd.CommandText = "select count(*) from [dbo].[appuser]" +
                                "where " +
                                "(User_Login ='" + AuthorizControl.UserLogin + "') and (User_Pass = '" + Crypt.GetHash(tbOldPass.Password) + "')";
                    passChck = Convert.ToInt32(DBConAct.execScalar());
                    DBConAct.cmd.CommandText = "select Id_appuser from [appuser] where [User_login] = '" + AuthorizControl.UserLogin + "'";
                    idUser = Convert.ToInt32(DBConAct.execScalar());
                    DBConAct.cmd.CommandText = "select Role_Id from [appuser] where [User_login] = '" + AuthorizControl.UserLogin + "'";
                    roleid = Convert.ToInt32(DBConAct.execScalar());
                    if (passChck == 1)
                    {
                        if (tbNewPass.Password == tbCheckNewPass.Password)
                        {
                            DBConAct.AppUser_Update(idUser, AuthorizControl.UserLogin, tbNewPass.Password, roleid);
                            MessageBox.Show("Пароль усмешно изменен!", "Пароль изменен!", MessageBoxButton.OK, MessageBoxImage.Information);
                            UcAccount_Loaded(sender, e);
                        }
                        else MessageBox.Show("Пароли не совпадают!", "Повторите попытку", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else MessageBox.Show("Текущий пароль введен неправильно!", "Повторите ввод", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка!");
            }
        }

        private void BtnToMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UcAccount_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                lblUserName.Content = AuthorizControl.UserLogin;
                DBConAct.cmd.CommandText = "select Surn_Client from client join [appuser] on [appuser_id] = id_appuser where [user_login] = '" + AuthorizControl.UserLogin + "'";
                lblSurname.Content = DBConAct.execScalar();
                DBConAct.cmd.CommandText = "select name_Client from client join [appuser] on [appuser_id] = id_appuser where [user_login] = '" + AuthorizControl.UserLogin + "'";
                lblName.Content = DBConAct.execScalar();
                DBConAct.cmd.CommandText = "select Email_Client from client join [appuser] on [appuser_id] = id_appuser where [user_login] = '" + AuthorizControl.UserLogin + "'";
                lblMidn.Content = DBConAct.execScalar();
                DBConAct.cmd.CommandText = "select Mob_Num_Client from client join [appuser] on [appuser_id] = id_appuser where [user_login] = '" + AuthorizControl.UserLogin + "'";
                lblMobNum.Content = DBConAct.execScalar();
                tbOldPass.Text = "";
                tbNewPass.Text = "";
                tbCheckNewPass.Text = "";
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }
    }
}
