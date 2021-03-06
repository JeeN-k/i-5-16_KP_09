﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Threading;

namespace TryFasterClient
{
    /// <summary>
    /// Логика взаимодействия для AuthorizControl.xaml
    /// </summary>
    public partial class AuthorizControl : UserControl
    {

        public static int Role; // код Роли
        public static string RoleName; // Название роли
        public static string UserLogin; // Имя пользователя

        public AuthorizControl()
        {
            InitializeComponent();

            Registr.Registry_Get();//получение значений из реестра
            if (Registr.SE == "1")//если запоминание пароля работает
            {
                TbLogin.Text = Registr.UI;//заполнение логина и пароля
                TbPassword.Password = Registr.PW;
                cbAutoEnter.IsChecked = true;
            }
            else
                cbAutoEnter.IsChecked = false;
        }


        private void RegBut_MouseDown(object sender, MouseButtonEventArgs e) // К регистрации
        {
            LinkControl.Link(new RegistrationControl());
        }

        private void Auth() // Авторизация
        {
            try
            {
                this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate ()
                {
                    DBConAct.cmd.CommandType = System.Data.CommandType.Text;
                    Int32 logchk;
                    // Проверка наличия учетной записи с указанным логином
                    DBConAct.cmd.CommandText = "SELECT Count(*) FROM [AppUser] WHERE User_Login= '" + TbLogin.Text + "' COLLATE Cyrillic_General_CS_AS";
                    logchk = Convert.ToInt32(DBConAct.execScalar());
                    // Если учетная запись существует
                    if (logchk > 0)
                    {
                        // Проверяем в соответствии с учетной записью заданный пароль
                        DBConAct.cmd.CommandText = "SELECT Count(*) FROM [AppUser] WHERE User_Login= '" + TbLogin.Text + "' and User_Pass= '" + Crypt.GetHash(TbPassword.Password) + "' COLLATE Cyrillic_General_CS_AS";
                        int PasswordCount = Convert.ToInt32(DBConAct.execScalar());
                        // Если пароль соответствует
                        if (PasswordCount > 0)
                        {
                            DBConAct.cmd.CommandText = "SELECT role_ID from [appuser] where User_Login = '" + TbLogin.Text + "'";
                            Role = Convert.ToInt32(DBConAct.execScalar());
                            DBConAct.cmd.CommandText = "select Role_Name from role where id_role = " + Role;
                            RoleName = DBConAct.execScalar();
                            UserLogin = TbLogin.Text;
                            string se = (bool)cbAutoEnter.IsChecked ? "1" : "0";
                            Registr.Registry_Set(TbLogin.Text, TbPassword.Password, se);
                            LinkControl.Link(new MenuControl());
                            foreach (Window window in Application.Current.Windows)//поиск окна
                            {
                                if (window.GetType() == typeof(MainWindow))//проверка класса окна
                                {
                                    (window as MainWindow).WPNav.Visibility = Visibility.Visible;
                                    (window as MainWindow).TBlUserInfo.Visibility = Visibility.Visible;
                                    (window as MainWindow).TBlUserInfo.Text = UserLogin + " (" + RoleName + ")";
                                }
                            }

                        }
                        else // Если пароль не подходит
                        {
                            lblErr.Visibility = Visibility.Visible;
                        }

                    }
                    else lblErr.Visibility = Visibility.Visible;
                }));
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
            finally
            {
                DBConAct.sql.Close();
            }
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e) // Запуск потока авторизации
        {
            Thread trd = new Thread(new ThreadStart(Auth));            
            trd.Start();
        }

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e) // Нажатие на ентер приводит в действие кнопку
        {
            if (e.Key == Key.Enter)
            {
                BtnEnter_Click(sender, e);
                e.Handled = true;
            }
            else
                e.Handled = false;
        }


        private void BtnSetConnect_MouseDown(object sender, MouseButtonEventArgs e) // На страницу подключения
        {
            LinkControl.Link(new ConnectControl());
        }
    }
}
